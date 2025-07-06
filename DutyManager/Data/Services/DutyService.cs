using DutyManager.Data.Models;
using DutyManager.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutyManager.Data.Services
{
    public class DutyService
    {
        private readonly IDutyRepository _repository;

        public DutyService(IDutyRepository repository)
        {
            _repository = repository;
        }

        public async Task<DutySchedule> GetTodayDutyAsync()
        {
            var today = DateTime.Today;
            var schedule = await _repository.GetDutyScheduleAsync(today);

            if (schedule == null || !schedule.Students.Any())
            {
                schedule = await GenerateScheduleAsync(today);
                await _repository.SaveDutyScheduleAsync(schedule);
            }

            return schedule;
        }

        private async Task<DutySchedule> GenerateScheduleAsync(DateTime date)
        {
            var students = await _repository.GetStudentsAsync();
            var config = await _repository.GetDutyConfigAsync();
            var activeStudents = students.Where(s => s.IsActive).ToList();

            if (!activeStudents.Any()) return new DutySchedule { Date = date };

            int startIndex = CalculateStartIndex(date, activeStudents.Count, config);
            var selectedStudents = activeStudents
                .Skip(startIndex)
                .Take(config.DutyPerDay)
                .ToList();

            return new DutySchedule
            {
                Date = date,
                Students = new ObservableCollection<Student>(selectedStudents)
            };
        }

        private int CalculateStartIndex(DateTime date, int studentCount, DutyConfig config)
        {
            return config.RotationType switch
            {
                RotationType.Daily => (int)(date - DateTime.MinValue).TotalDays % studentCount,
                RotationType.Weekly => (int)((date - DateTime.MinValue).TotalDays / 7) % studentCount,
                _ => 0
            };
        }
    }
}

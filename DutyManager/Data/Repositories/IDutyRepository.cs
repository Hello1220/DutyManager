using DutyManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutyManager.Data.Repositories
{
    public interface IDutyRepository
    {
        Task<List<Student>> GetStudentsAsync();
        Task SaveStudentsAsync(List<Student> students);

        Task<DutySchedule> GetDutyScheduleAsync(DateTime date);
        Task SaveDutyScheduleAsync(DutySchedule schedule);

        Task<DutyConfig> GetDutyConfigAsync();
        Task SaveDutyConfigAsync(DutyConfig config);
    }
}

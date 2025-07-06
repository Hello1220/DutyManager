using DutyManager.Data.Models;
using DutyManager.Data.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DutyManager.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly DutyService _dutyService;

        private ObservableCollection<Student> _todayDutyStudents = new();
        public ObservableCollection<Student> TodayDutyStudents
        {
            get => _todayDutyStudents;
            set => SetProperty(ref _todayDutyStudents, value);
        }

        public ICommand OpenSettingsCommand { get; }
        public ICommand RefreshCommand { get; }

        public MainViewModel(DutyService dutyService)
        {
            _dutyService = dutyService;
            OpenSettingsCommand = new RelayCommand(OpenSettings);
            RefreshCommand = new RelayCommand(async () => await LoadDataAsync());

            LoadDataAsync().ConfigureAwait(false);
        }

        private async Task LoadDataAsync()
        {
            var todayDuty = await _dutyService.GetTodayDutyAsync();
            TodayDutyStudents = new ObservableCollection<Student>(todayDuty.Students);
        }

        private void OpenSettings()
        {
            var settingsWindow = new SettingsWindow();
            settingsWindow.ShowDialog();
            LoadDataAsync().ConfigureAwait(false);
        }
    }
}

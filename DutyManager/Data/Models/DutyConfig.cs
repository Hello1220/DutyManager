using CommunityToolkit.Mvvm.ComponentModel;
using DutyManager.Data.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;// Data/Models/DutyConfig.cs
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class DutyConfig : ObservableObject
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [ObservableProperty]
    private RotationType _rotationType = RotationType.Daily;

    [ObservableProperty]
    private int _dutyPerDay = 2;
}

namespace DutyManager.Data.Models
{
    public enum RotationType { Daily, Weekly }

    public class DutyConfig : INotifyPropertyChanged
    {
        private RotationType _rotationType = RotationType.Daily;
        public RotationType RotationType
        {
            get => _rotationType;
            set => SetProperty(ref _rotationType, value);
        }

        private int _dutyPerDay = 2;
        public int DutyPerDay
        {
            get => _dutyPerDay;
            set => SetProperty(ref _dutyPerDay, value);
        }

        protected bool SetProperty<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

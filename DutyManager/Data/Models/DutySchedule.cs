using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

public class DutySchedule : ObservableObject, INotifyPropertyChanged, INotifyPropertyChanging
{
    private DateTime _date;

    public DateTime Date
    {
        get => _date;
        set => SetProperty(ref _date, value);
    }

    private ObservableCollection<Student> _students;

    public ObservableCollection<Student> Students
    {
        get => _students;
        set => SetProperty(ref _students, value);
    }
}
namespace DutyManager.Data.Models
{
    public class DutySchedule : ObservableObject // ObservableObject 来自 CommunityToolkit.Mvvm.ComponentModel
    {
        public DateTime Date { get; set; }

        private ObservableCollection<Student> _students = new();
        public ObservableCollection<Student> Students
        {
            get => _students;
            set => SetProperty(ref _students, value);
        }
    }
}

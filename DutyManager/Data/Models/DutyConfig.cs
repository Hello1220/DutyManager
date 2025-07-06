using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutyManager.Data.Models
{
    public enum RotationType { Daily, Weekly }

    public class DutyConfig : ObservableObject
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
    }
}

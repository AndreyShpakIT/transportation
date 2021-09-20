using db_course_project.Extentions;
using System.Collections.Generic;
using static db_course_project.ReportManager;

namespace db_course_project.ViewModels
{
    class ReportViewModel : ViewModelBase
    {
        private Period selectedPeriod;

        public List<Period> PeriodItems { get; set; }
        public Period SelectedPeriod
        {
            get => selectedPeriod;
            set => SetValue(ref selectedPeriod, value);
        }

        private ReportValue1 selectedRV1;

        public List<ReportValue1> RV1Items { get; set; }
        public ReportValue1 SelectedRV1
        {
            get => selectedRV1;
            set => SetValue(ref selectedRV1, value);
        }

        public ReportViewModel()
        {
            InitCommands();
            PeriodItems = new List<Period>
            {
                Period.Day,
                Period.Moth,
                Period.Year,
            };

            RV1Items = new List<ReportValue1>
            {
                ReportValue1.Car,
                ReportValue1.Client,
            };
        }

        public RelayCommand RequestAmoutReport { get; set; }
        public RelayCommand RequestAmoutReport2 { get; set; }
        public RelayCommand RequestAmoutReport3 { get; set; }

        private void InitCommands()
        {
            RequestAmoutReport = new RelayCommand((param) => ReportManager.CreateRequestAmoutInPeriod(selectedPeriod));
            RequestAmoutReport2 = new RelayCommand((param) => ReportManager.CreateRequestAmoutInClientOrCar(selectedRV1));
            RequestAmoutReport3 = new RelayCommand((param) => ReportManager.CreateReportForEmployeers());
        }


    }
}

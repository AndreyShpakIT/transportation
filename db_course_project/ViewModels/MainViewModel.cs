using db_course_project.Extentions;
using db_course_project.Views;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace db_course_project.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        public ObservableCollection<UserControl> Views { get; set; }

        private UserControl _currentView;
        public UserControl CurrentView
        {
            get => _currentView;
            set => SetValue(ref _currentView, value);
        }
        // Commands
        public RelayCommand TabClickCommand { get; set; }

        public MainViewModel()
        {
            Views = new ObservableCollection<UserControl>
            {
                new ReferencesView(),
                new RequestView()
            };

            TabClickCommand = new RelayCommand((parameter) =>
            {
                try
                {
                    int number = Convert.ToInt32(parameter);
                    if (number == 1)
                    {
                        Views[number] = new RequestView();
                    }
                    CurrentView = Views[number];
                }
                catch
                {
                    MessageBox.Show("Еще не реализовано!");
                }
            });
        }
    }
}

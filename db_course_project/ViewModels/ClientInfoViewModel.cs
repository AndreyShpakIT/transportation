using db_course_project.Database;
using db_course_project.Database.Views;
using db_course_project.Extentions;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace db_course_project.ViewModels
{
    class ClientInfoViewModel : ViewModelBase
    {
        // Private

        private ApplicationDbContext db;

        private string number;
        private string name;

        private ObservableCollection<ПредставлениеЗаявки> items;

        // Public

        public ObservableCollection<ПредставлениеЗаявки> Items
        {
            get => items;
            set => SetValue(ref items, value);
        }
        public string Number
        {
            get => number;
            set => SetValue(ref number, value);
        }

        public string Name
        {
            get => name;
            set => SetValue(ref name, value);
        }
        // Constructor

        public ClientInfoViewModel()
        {
            db = new ApplicationDbContext();
            db.Клиенты.Load();
            Items = db.ПредставлениеЗаявки.Local;

            SearchCommand = new RelayCommand(Search);
        }


        // Commands
        public RelayCommand SearchCommand { get; set; }
     
        // Methods
        public void Search(object param)
        {
            if (string.IsNullOrEmpty(number))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            Items.Clear();
            Items = new ObservableCollection<ПредставлениеЗаявки>(db.ПредставлениеЗаявки.Where(o => db.Клиенты.Where(c => c.Номер_телефона == number).FirstOrDefault().ФИО == o.ФИО));
        }
    }
}

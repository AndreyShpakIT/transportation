using db_course_project.Database;
using db_course_project.Extentions;
using System;
using System.Data.Entity;
using System.Windows;

namespace db_course_project.ViewModels
{
    public delegate void ClientCreation(Клиенты client);
    class CreateNewClientViewModel : ViewModelBase
    {
        public event ClientCreation ClientCreated;

        private ApplicationDbContext db;

        private string name;
        private string number;
        private string address;
        private DateTime date = DateTime.Now.AddYears(-10);

        public string Name
        {
            get => name;
            set => SetValue(ref name, value);
        }
        public string Number
        {
            get => number;
            set => SetValue(ref number, value);
        }
        public string Address
        {
            get => address;
            set => SetValue(ref address, value);
        }
        public DateTime Date
        {
            get => date;
            set => SetValue(ref date, value);
        }
        public DateTime DateStart { get; set; } = DateTime.Now.AddYears(-100);
        public DateTime DateEnd { get; set; } = DateTime.Now.AddYears(-10);

        public RelayCommand CreateCommand { get; set; }

        public CreateNewClientViewModel()
        {
            db = new ApplicationDbContext();
            try
            {
                db.Клиенты.Load();
                CreateCommand = new RelayCommand((param) =>
                {
                    Клиенты client = CreateClient();
                    if (!ValidateClinet(client))
                    {
                        MessageBox.Show("Заполните поля корректными данными!");
                        return;
                    }
                    db.Клиенты.Add(client);
                    db.SaveChanges();
                    ClientCreated?.Invoke(client);
                });
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public Клиенты CreateClient()
        {
            return new Клиенты
            {
                ФИО = Name,
                Адрес_проживания = Address,
                Дата_рождения = Date,
                Номер_телефона = Number
            };
        }
        public bool ValidateClinet(Клиенты client)
        {
            if (string.IsNullOrEmpty(client.Адрес_проживания) ||
                string.IsNullOrEmpty(client.ФИО) ||
                string.IsNullOrEmpty(client.Номер_телефона))
                return false;
            return true;
        }
    }
}

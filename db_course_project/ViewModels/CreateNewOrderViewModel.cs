using db_course_project.Database;
using db_course_project.Database.Views;
using db_course_project.Extentions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace db_course_project.ViewModels
{
    public delegate void OrderCreation(Заказы order);
    class CreateNewOrderViewModel : ViewModelBase
    {
        public event OrderCreation OrderCreated;

        // Private

        private ApplicationDbContext db;


        private Сотрудники selectedWorker;
        private string selectedCar;


        // Public
        public Сотрудники SelectedWorker
        {
            get => selectedWorker;
            set => SetValue(ref selectedWorker, value);
        }
        public string SelectedCar
        {
            get => selectedCar;
            set => SetValue(ref selectedCar, value);
        }
        public ObservableCollection<Сотрудники> ItemsWorkers { get; set; }
        public ObservableCollection<string> ItemsCars { get; set; }
        public int RequestId { get; set; }

        // Commands
        public RelayCommand CreateCommand { get; set; }

        // Constructors
        public CreateNewOrderViewModel(int reqId, DateTime date)
        {
            try
            {
                RequestId = reqId;
                db = new ApplicationDbContext();
                ItemsWorkers = new ObservableCollection<Сотрудники>(GetManagers());
                ItemsCars = GetCars(date);

                db.Клиенты.Load();
                CreateCommand = new RelayCommand(Create);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private ObservableCollection<string> GetCars(DateTime date)
        {
            return new ObservableCollection<string>(
                    db.Database.SqlQuery<string>("SELECT * FROM dbo.getFreeCarsId(@date)", new SqlParameter("@date", date))
                    .ToList());
        }
        private void Create(object param)
        {
            try
            {
                Заказы order = CreateOrderObject();
                if (!ValidateOrder(order))
                {
                    MessageBox.Show("Заполните поля корректными данными!");
                    return;
                }
                db.Заказы.Add(order);
                db.SaveChanges();
                OrderCreated?.Invoke(order);
            }
            catch (DbUpdateException e)
            {
                MessageBox.Show(e.InnerException.InnerException.Message);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        // Methods
        private Заказы CreateOrderObject()
        {
            return new Заказы
            {
                Код_автомобиля = selectedCar,
                Код_менеджера = selectedWorker?.Код_сотрудника ?? -1,
                Код_заявки = RequestId
            };
        }
        private bool ValidateOrder(Заказы order)
        {
            if (order.Код_автомобиля == null ||
                order.Код_менеджера == -1 ||
                order.Код_заявки == -1)
                return false;
            return true;
        }
        private IEnumerable<Сотрудники> GetManagers()
        {
            db.Сотрудники.Load();
            return db.Сотрудники.Local.Where(e => e.Код_должности == 4);
        }
    }
}
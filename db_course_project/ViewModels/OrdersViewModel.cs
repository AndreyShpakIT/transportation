using db_course_project.Database;
using db_course_project.Database.Views;
using db_course_project.Extentions;
using db_course_project.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace db_course_project.ViewModels
{
    class OrdersViewModel : ViewModelBase
    {
        // Private

        private ApplicationDbContext db;
        private ПредставлениеЗаявки selectedRequest;
        private ObservableCollection<ПредставлениеЗаявки> items;

        // Public

        public ObservableCollection<ПредставлениеЗаявки> Items
        {
            get => items;
            set => SetValue(ref items, value);
        }

        public ПредставлениеЗаявки SelectedRequest
        {
            get => selectedRequest;
            set => SetValue(ref selectedRequest, value);
        }

        public RelayCommand CreateCommand { get; set; }
        public RelayCommand RefreshCommand { get; set; }


        //Methods

        public OrdersViewModel()
        {
            db = new ApplicationDbContext();
            db.ПредставлениеЗаявки.Load();
            db.Заказы.Load();
            Items = new ObservableCollection<ПредставлениеЗаявки>(GetItems());


            CreateCommand = new RelayCommand(CreateOrder);
            RefreshCommand = new RelayCommand(Refresh);
        }

        private void CreateOrder(object param)
        {
            if (selectedRequest == null)
            {
                MessageBox.Show("Выберите заявку!");
                return;
            }
            CreateOrderWindow window = new CreateOrderWindow();
            CreateNewOrderViewModel dataContex = new CreateNewOrderViewModel(selectedRequest.Код_заявки, selectedRequest.Дата_перевозки);
            window.DataContext = dataContex;
            window.Show();

            dataContex.OrderCreated += (order) =>
            {
                window.Close();
                Refresh();
            };
        }
        private void Refresh(object param = null)
        {
            Items.Clear();
            Items = new ObservableCollection<ПредставлениеЗаявки>(GetItems());
        }
        private IEnumerable<ПредставлениеЗаявки> GetItems()
        {
            return db.ПредставлениеЗаявки.Where(v => !db.Заказы.Any(o => o.Код_заявки == v.Код_заявки));
        }
    }
}
using db_course_project.Database;
using db_course_project.Extentions;
using db_course_project.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace db_course_project.ViewModels
{
    class RequestViewModel : ViewModelBase
    {
        // Private
        private ApplicationDbContext db;
        private CreateClientWindow createClientWindow;
        
        private Клиенты selectedClient;
        private Услуги selectedService;
        private DateTime selectedDate = DateTime.Now.AddDays(3);
        private string address;
        private int km;
        private double sum;

        private ObservableCollection<Клиенты> clients;
        private ObservableCollection<Услуги> services;
        

        // Items
        public ObservableCollection<Клиенты> ClientItems
        {
            get => clients;
            set => SetValue(ref clients, value);
        }
        public ObservableCollection<Услуги> ServiceItems
        {
            get => services;
            set => SetValue(ref services, value);
        }

        // Selected
        public DateTime InitDate { get; set; } = DateTime.Now;
        public DateTime DateStart { get; set; } = DateTime.Now.AddDays(3);
        public DateTime DateEnd { get; set; } = DateTime.Now.AddDays(33);
        public DateTime SelectedDate
        {
            get => selectedDate;
            set => SetValue(ref selectedDate, value);
        }

        public Клиенты SelectedClient
        {
            get => selectedClient;
            set => SetValue(ref selectedClient, value);
        }
        public Услуги SelectedService
        {
            get => selectedService;
            set => SetValue(ref selectedService, value);
        }
        public string Address
        {
            get => address;
            set => SetValue(ref address, value);
        }
        public int Km
        {
            get => km;
            set => SetValue(ref km, value);
        }
        public double Sum
        {
            get => sum;
            set => SetValue(ref sum, value);
        }

        // Constructors
        public RequestViewModel()
        {
            db = new ApplicationDbContext();
            
            InitCommands();
            
            ClientItems = GetClients();
            ServiceItems = GetServices();
        }

        // Commands
        public RelayCommand CreateRequestCommand { get; set; }
        public RelayCommand NewClientCommand { get; set; }
        

        // Methods
        private void InitCommands()
        {
            CreateRequestCommand = new RelayCommand(CreateRequest);
            NewClientCommand = new RelayCommand((param) =>
            {
                createClientWindow = new CreateClientWindow();
                CreateNewClientViewModel dataContext = new CreateNewClientViewModel();
                dataContext.ClientCreated += (client) =>
                {
                    SelectedClient = client;
                    ClientItems.Add(client);
                    createClientWindow.Close();
                };
                createClientWindow.Show();
                createClientWindow.DataContext = dataContext;
            });
        }
        private ObservableCollection<Клиенты> GetClients()
        {
            db.Клиенты.Load();
            return db.Клиенты.Local;
        }
        private ObservableCollection<Услуги> GetServices()
        {
            db.Услуги.Load();
            return db.Услуги.Local;
        }
        private void CreateRequest(object param)
        {
            try
            {
                db.Заявки.Load();
                Заявки request = CreateRequestObject();
                if (!Validate(request))
                {
                    MessageBox.Show("Заполните поля корректными данными!");
                    return;
                }
                db.Заявки.Add(request);
                db.SaveChanges();
                Clear();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private Заявки CreateRequestObject()
        {
            return new Заявки
            {
                Дата_перевозки = SelectedDate,
                Дата_создания_заявки = InitDate,
                Код_клиента = SelectedClient?.Код_клиента ?? -1,
                Код_услуги = SelectedService?.Код_услуги ?? -1,
                Адрес_доставки = Address,
                Км = Km
            };
        }
        private bool Validate(Заявки req)
        {
            if (string.IsNullOrEmpty(req.Адрес_доставки) ||
                req.Км == 0 ||
                req.Код_клиента == -1 ||
                req.Код_услуги == -1)
                return false;
            return true;
        }
        private void Clear()
        {
            SelectedClient = null;
            SelectedDate = DateTime.Now.AddDays(3);
            SelectedService = null;
            Address = "";
            Km = 0;
        }
    }
}
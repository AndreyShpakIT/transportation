using db_course_project.Database;
using db_course_project.Extentions;
using db_course_project.Views;
using OfficeOpenXml;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
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
        private decimal km;
        private decimal sum;
        private decimal weight;

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
            set 
            {
                SetValue(ref selectedService, value);
                Sum = Calculate(km, weight, GetPrice());
            }
        }
        public string Address
        {
            get => address;
            set => SetValue(ref address, value);
        }
        public decimal Km
        {
            get => km;
            set
            {
                SetValue(ref km, value);
                Sum = Calculate(km, weight, GetPrice());
            }
        }
        public decimal Weight
        {
            get => weight;
            set
            {
                SetValue(ref weight, value);
                Sum = Calculate(km, weight, GetPrice());
            }
        }
        public decimal Sum
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
                if (!db.Database.SqlQuery<bool>("SELECT dbo.IsDateAvailable(@date)", new SqlParameter("@date", selectedDate)).FirstOrDefault())
                {
                    MessageBox.Show("На данную дату нет свободных мест.");
                    return;
                }
                db.Заявки.Load();
                Заявки request = CreateRequestObject();
                if (!Validate(request))
                {
                    MessageBox.Show("Заполните поля корректными данными!");
                    return;
                }
                if (!ReportManager.CreateNewReport(GenerateBill, "bill.xlsx"))
                {
                    if (MessageBox.Show("Не удалось создать отчет, записать данные в БД?", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    {
                        return;
                    } 
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
                Расстояние = Km,
                Масса = Weight,
                Сумма = Sum,
            };
        }
        private decimal GetPrice()
        {
            db.Тарифы.Load();
            if (selectedService == null)
            {
                return 0;
            }
            return db.Тарифы.Where(f => f.Код_тарифа == selectedService.Код_тарифа).First().Стоимость_тарифа;
        }
        private bool Validate(Заявки req)
        {
            if (string.IsNullOrEmpty(req.Адрес_доставки) ||
                req.Расстояние == 0 ||
                req.Код_клиента == -1 ||
                req.Код_услуги == -1 || 
                req.Сумма == -1 ||
                req.Масса == -1)
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
        private decimal Calculate(decimal km, decimal kg, decimal k)
        {
            return Math.Round((km <= 15 ? 1 : ((km / 10) + 1) * (kg / 10 + 1)) * k, 2); 
        }

        public void GenerateBill(ExcelWorksheet sheet)
        {
            sheet.Cells[1, 1].Value = "Счет на оплату от " + DateTime.Now;
            sheet.Cells[1, 1].Style.Font.Bold = true;
            sheet.Cells[1, 1].Style.Font.Size = 18;

            sheet.Cells[3, 1].Value = "Исполнитель: ";
            sheet.Cells[3, 2].Value = "ОАО \"Грузоперевозки\"";

            sheet.Cells[4, 2].Value = selectedClient.ФИО;
            sheet.Cells[4, 1].Value = "Заказчик: ";

            sheet.Cells[7, 1].Value = "Счет: ";
            sheet.Cells[7, 2].Value = "834576938156084592748413847";

            sheet.Cells[9, 1].Value = "Сумма: ";
            sheet.Cells[9, 2].Value = sum;
        }
    }
}
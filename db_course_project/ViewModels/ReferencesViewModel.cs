using db_course_project.Database;
using db_course_project.Extentions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Windows;

namespace db_course_project.ViewModels
{
    class ReferencesViewModel : ViewModelBase
    {
        public RelayCommand RefClickCommand { get; set; }
        public RelayCommand TestClickCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand SaveCommand { get; set; }

        public ApplicationDbContext db { get; set; }
        private IEnumerable<object> _items;
        public IEnumerable<object> Items
        {
            get => _items;
            set => SetValue(ref _items, value);
        }
        public ReferencesViewModel()
        {
            db = new ApplicationDbContext();
            LoadDb();
            RefClickCommand = new RelayCommand(SetDataSrouce);
            TestClickCommand = new RelayCommand((param) =>
            {
                foreach (IDbTable row in Items)
                {
                    Table table = Table.GetTable(row);
                    //row.IsSearchable("asf");
                }
            });
            SearchCommand = new RelayCommand((param) =>
            {
                Items = Search(param as string);
            });
            SaveCommand = new RelayCommand((param) =>
            {
                SaveDb();
            });
        }
        public void LoadDb()
        {
            db.Автомобили.Load();
            db.Должности.Load();
            db.Заказы.Load();
            db.Клиенты.Load();
            db.Сотрудники.Load();
            db.Тарифы.Load();
            db.Тип_автомобиля.Load();
            db.Услуги.Load();
        }
        
        public void SetDataSrouce(object tableName)
        {
            string name = Convert.ToString(tableName);
            var dbSet = GetPropValue(db, name);
            var local = GetPropValue(dbSet, "Local");

            Items = (local as IEnumerable<IDbTable>);
        }
        public void SaveDb()
        {
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
        public static object InvokeMethod(object src, string MethodName)
        {
            return src.GetType().GetMethod(MethodName).Invoke(src, null);
        }

        public IEnumerable<object> Search(string value)
        {
            ObservableCollection<object> items = new ObservableCollection<object>();
            foreach (IDbTable row in Items)
            {
                if (row.IsSearchable(value))
                {
                    items.Add(row);
                }
            }

            return items;
        }
    }
}

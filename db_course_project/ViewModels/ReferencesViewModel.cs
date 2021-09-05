using db_course_project.Database;
using db_course_project.Extentions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace db_course_project.ViewModels
{
    class ReferencesViewModel : ViewModelBase
    {
        public RelayCommand RefClickCommand { get; set; }
        public ApplicationDbContext db { get; set; }
        private ObservableCollection<object> _items;
        public ObservableCollection<object> Items
        {
            get => _items;
            set => SetValue(ref _items, value);
        }
        public ReferencesViewModel()
        {
            db = new ApplicationDbContext();
            LoadDb();
            RefClickCommand = new RelayCommand(SetDataSrouce);
        }
        public async void LoadDb()
        {
            await db.Автомобили.LoadAsync();
            await db.Должности.LoadAsync();
            await db.Заказы.LoadAsync();
            await db.Клиенты.LoadAsync();
            await db.Сотрудники.LoadAsync();
            await db.Тарифы.LoadAsync();
            await db.Тип_автомобиля.LoadAsync();
            await db.Услуги.LoadAsync();
        }
        
        public void SetDataSrouce(object tableName)
        {
            string name = Convert.ToString(tableName);
            var dbSet = GetPropValue(db, name);
            var local = GetPropValue(dbSet, "Local");

            var type = local.GetType(); 

            //object f = InvokeMethod(local, "CopyTo");
            //ObservableCollection<object> ff;
            
            ////ObservableCollection<Должности> list = f.;
            //Debug.WriteLine(local is ObservableCollection<Должности>);
        }
        //public void convert(string className, object items)
        //{
        //    Type type = Type.GetType(className);
        //    dynamic listType = typeof(ObservableCollection<>);
        //    var constructedListType = listType.MakeGenericType(type);

        //    var instance = Activator.CreateInstance(constructedListType);

        //}
        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
        public static object InvokeMethod(object src, string MethodName)
        {
            return src.GetType().GetMethod(MethodName).Invoke(src, null);
        }
    }
}

using db_course_project.Database;
using OfficeOpenXml;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows;

namespace db_course_project
{
    public delegate void Generation(ExcelWorksheet package);
    static class ReportManager
    {
        public static string Path = "Reports/";

        private static ApplicationDbContext db;

        static ReportManager()
        {
            db = new ApplicationDbContext();
        }

        public static bool SaveReport(ExcelPackage package, string path)
        {
            try
            {
                File.WriteAllBytes(path, package.GetAsByteArray());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }

            MessageBox.Show("Отчет успешно создан по следующему пути: " + path);
            return true;
        }

        public static bool CreateNewReport(Generation generate, string fileName)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage package = new ExcelPackage();

            ExcelWorksheet sheet = package.Workbook.Worksheets.Add("Лист1");

            generate(sheet);

            return SaveReport(package, Path + fileName);
        }
        
        public static bool CreateRequestAmoutInPeriod(Period period)
        {
            string start = "";
            string end = DateTime.Now.ToString("dd-MM-yyyy");
            switch(period)
            {
                case Period.Day:
                    start = DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy");
                    break;
                case Period.Moth:
                    start = DateTime.Now.AddDays(-30).ToString("dd-MM-yyyy");
                    break;
                case Period.Year:
                    start = DateTime.Now.AddDays(-365).ToString("dd-MM-yyyy");
                    break;
            }

            string sql = string.Format("SELECT К.ФИО As Field1, count(*) As Field2 FROM Заказы З " +
                "JOIN Заявки Зв " +
                "ON З.[Код заявки] = Зв.[Код заявки] AND Зв.[Дата создания заявки] BETWEEN '{0}' AND '{1}' " +
                "JOIN Клиенты К " +
                "ON Зв.[Код клиента] = К.[Код клиента] " +
                "GROUP BY К.ФИО;", start, end);

            var list = db.Database.SqlQuery<Amount>(sql).ToArray();

            return CreateNewReport(sheet =>
            {
                sheet.Cells[1, 1].Value = "Статистический отчет по количеству заказов за период времени с " + start + " по " + end;
                sheet.Cells[1, 1].Style.Font.Bold = true;
                sheet.Cells[1, 1].Style.Font.Size = 16;

                for (int i = 1; i < list.Length + 1; i++)
                {
                   
                    sheet.Cells[i + 2, 1].Value = list[i - 1].Field1 + ":";
                    sheet.Cells[i + 2, 2].Value = list[i - 1].Field2;
                }
            }, "requestAmout.xlsx");
        }

        public static bool CreateRequestAmoutInClientOrCar(ReportValue1 val)
        {
            string name = "";
            string sql = "";
            switch (val)
            {
                case ReportValue1.Car:
                    name = "автомобилей";
                    sql =
                        "SELECT З.[Код автомобиля] AS Field1, count(*) AS Field2 FROM Заказы З " +
                        "JOIN Заявки Зв " +
                        "ON З.[Код заявки] = Зв.[Код заявки] " +
                        "JOIN Клиенты К " +
                        "ON Зв.[Код клиента] = К.[Код клиента] " +
                        "GROUP BY З.[Код автомобиля]";
                    break;
                case ReportValue1.Client:
                    name = "клиентов";
                    sql =
                        "SELECT К.ФИО As Field1, count(*) As Field2 FROM Заказы З " +
                        "JOIN Заявки Зв " +
                        "ON З.[Код заявки] = Зв.[Код заявки] " +
                        "JOIN Клиенты К " +
                        "ON Зв.[Код клиента] = К.[Код клиента] " +
                        "GROUP BY К.ФИО;";
                    break;
            }

            var list = db.Database.SqlQuery<Amount>(sql).ToArray();

            return CreateNewReport(sheet =>
            {
                sheet.Cells[1, 1].Value = "Статистический отчет по количеству заказов в резрезе " + name;
                sheet.Cells[1, 1].Style.Font.Bold = true;
                sheet.Cells[1, 1].Style.Font.Size = 16;

                for (int i = 1; i < list.Length + 1; i++)
                {

                    sheet.Cells[i + 2, 1].Value = list[i - 1].Field1 + ":";
                    sheet.Cells[i + 2, 2].Value = list[i - 1].Field2;
                }
            }, "requestAmout2.xlsx");
        }

        public static bool CreateReportForEmployeers()
        {
            string sql =
                "SELECT С.ФИО AS 'Field1', С2.ФИО AS 'Field2', Зв.Сумма AS 'Field3'FROM Заказы З " +
                "JOIN Сотрудники С " +
                "ON З.[Код менеджера] = С.[Код сотрудника] " +
                "JOIN Автомобили А " +
                "ON А.[Номер автомобиля] = З.[Код автомобиля] " +
                "JOIN Сотрудники С2 " +
                "ON С2.[Код сотрудника] = А.[Код водителя] " +
                "JOIN Заявки Зв " +
                "ON Зв.[Код заявки] = З.[Код заявки];";

            var list = db.Database.SqlQuery<Amount2>(sql).ToArray();

            return CreateNewReport(sheet =>
            {
                sheet.Cells[1, 1].Value = "Аналитический отчет по работе сотрудников";
                sheet.Cells[1, 1].Style.Font.Bold = true;
                sheet.Cells[1, 1].Style.Font.Size = 16;

                sheet.Cells[3, 1].Value = "Менеджер";
                sheet.Cells[3, 1].Style.Font.Bold = true;

                sheet.Cells[3, 2].Value = "Водитель";
                sheet.Cells[3, 2].Style.Font.Bold = true;

                sheet.Cells[3, 3].Value = "Сумма";
                sheet.Cells[3, 3].Style.Font.Bold = true;

                for (int i = 1; i < list.Length + 1; i++)
                {
                    sheet.Cells[i + 3, 1].Value = list[i - 1].Field1;
                    sheet.Cells[i + 3, 2].Value = list[i - 1].Field2;
                    sheet.Cells[i + 3, 3].Value = list[i - 1].Field3;
                }
            }, "requestAmout3.xlsx");
        }

        public enum Period
        {
            Day, Moth, Year
        }
        public enum ReportValue1
        {
            Client, Car
        }
        class Amount
        {
            public string Field1 { get; set; }
            public int Field2 { get; set; }
        }
        class Amount2
        {
            public string Field1 { get; set; }
            public string Field2 { get; set; }
            public decimal Field3 { get; set; }

        }
    }

}

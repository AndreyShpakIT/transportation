using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace db_course_project.Database
{
    class Table
    {
        public Table()
        {
            Properties = new List<string>();
        }
        private Table(List<string> properties) : this()
        {
            Properties.AddRange(properties);
        }

        public List<string> Properties { get; set; }
        public static Table GetTable(IDbTable table)
        {
            List<string> properties = new List<string>();

            Type type = table.GetType();
            PropertyInfo[] props = type.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                object val = prop.GetValue(table);
                if (!val.GetType().IsPrimitive && !(val is string))
                {
                    continue;
                }
                string stringVal = Convert.ToString(val);
                properties.Add(stringVal);
            }

            return new Table(properties);
        }
    }
}

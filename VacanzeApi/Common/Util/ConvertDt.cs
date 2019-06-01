using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace vacanze_back.VacanzeApi.Common.Util
{
    public class ConvertDt
    {

        /// <summary>
        /// Convierte datatable en lista de objetos
        /// </summary>
        /// <typeparam name="T">objeto</typeparam>
        /// <param name="dt">datatable</param>
        /// <returns></returns>
        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    T item = GetItem<T>(row);
                    data.Add(item);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
            return data;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

    }
}

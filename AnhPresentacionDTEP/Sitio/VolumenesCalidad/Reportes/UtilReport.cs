using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AnhHydro.Sitio.VolumenesCalidad.Reportes
{
    public static class UtilReport
    {
        public static DataSet mToDataSet<T>(IList<T> list)
        {
            var elementType = typeof(T);
            var ds = new DataSet();
            var t = new DataTable();
            ds.Tables.Add(t);

            foreach (var propInfo in elementType.GetProperties())
            {
                var ColType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;
                t.Columns.Add(propInfo.Name, ColType);
            }

            foreach (T item in list)
            {
                var row = t.NewRow();
                foreach (var propInfo in elementType.GetProperties())
                {
                    row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
                }
                t.Rows.Add(row);
            }
            return ds;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;

namespace JARASOFT.CMDB.Services.Repositories.Infraestructure
{
    public static class DataUtils
    {
        public static string GetString(object data)
        {
            return data is DBNull ? null : (string)data;
        }

        public static int GetInt32(object data)
        {
            return GetInt32(data, false);
        }

        public static int GetInt32(object data, bool nullable)
        {
            if (!nullable)
                return (int)data;

            return data is DBNull ? -1 : (int)data;
        }

        public static long GetInt64(object data)
        {
            return GetInt64(data, false);
        }

        public static long GetInt64(object data, bool nullable)
        {
            if (!nullable)
                return (int)data;

            return data is DBNull ? -1 : (long)data;
        }

        public static DateTime GetDateTime(object data)
        {
            return GetDateTime(data, false);
        }

        public static DateTime GetDateTime(object data, bool nullable)
        {
            if (!nullable)
                return (DateTime)data;

            return data is DBNull ? DateTime.MinValue : (DateTime)data;
        }

        public static Guid GetGuid(object data)
        {
            return GetGuid(data, false);
        }

        public static Guid GetGuid(object data, bool nullable)
        {
            if (!nullable)
                return (Guid)data;

            return data is DBNull ? Guid.Empty : (Guid)data;
        }

        public static bool GetBoolean(object data)
        {
            return GetBoolean(data, false);
        }

        public static bool GetBoolean(object data, bool nullable)
        {
            if (!nullable)
                return (bool)data;

            return !(data is DBNull) && (bool)data;
        }

        public static decimal GetDecimal(object data)
        {
            return GetDecimal(data, false);
        }

        public static decimal GetDecimal(object data, bool nullable)
        {
            if (!nullable)
                return (decimal)data;

            return data is DBNull ? -1 : (decimal)data;
        }

        public static Nullable<T> GetNullableValue<T>(object data) where T : struct
        {
            if (data is DBNull)
                return null;

            return new Nullable<T>((T)data);
        }

        public static T ParseEnum<T>(object value)
        {
            return ParseEnum<T>(GetString(value));
        }

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static object ToEnum(this Enum enumInstance, Type enumType, object value)
        {
            var names = Enum.GetNames(enumType);
            if (Array.IndexOf(names, value) != -1)
            {
                return Enum.Parse(enumType, (string)value);
            }
            var match = names.FirstOrDefault(name => name.StartsWith((string)value, StringComparison.InvariantCultureIgnoreCase));
            if (match != null)
            {
                return Enum.Parse(enumType, match);
            }

            return Enum.ToObject(enumType, 1);
        }

        public static bool FieldExists(this IDataReader reader, string fieldName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(fieldName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        public static object GetOptional(this IDataReader reader, string fieldName)
        {
            if (reader.FieldExists(fieldName))
                return reader[fieldName];

            return null;
        }

        public static DataTable ConvertListIntToTable(List<int> list, string colName)
        {
            return ConvertVectorToTable<int>(list, colName);
        }

        public static DataTable ConvertVectorToTable<T>(ICollection<T> list, string colName)
        {
            DataTable table = new DataTable();
            table.Columns.Add(colName);
            if (list != null)
            {
                foreach (var item in list)
                {
                    table.Rows.Add(item);
                }
            }

            return table;
        }

        public static DataTable ConvertToDateFiltersTable(Dictionary<string, DateTime> filters)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Name");
            table.Columns.Add("ValueDateTime");
            if (filters != null && filters.Count > 0)
            {
                foreach (var item in filters)
                {
                    DataRow row = table.NewRow();
                    row["Name"] = item.Key;
                    row["ValueDateTime"] = item.Value;
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataTable GetTableFromType(string tableName, List<PropertyDescriptor> properties)
        {
            DataTable table = new DataTable(tableName);
            foreach (PropertyDescriptor property in properties)
            {
                if (!property.PropertyType.IsGenericType)
                {
                    table.Columns.Add(property.Name, property.PropertyType);
                }
                //else
                //{
                //    table.Columns.Add(property.Name, property.PropertyType.GenericParameterAttributes);
                //}
            }
            return table;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objects"></param>
        /// <returns></returns>
        public static DataTable GetTableFromObjects<T>(IEnumerable<T> objects)
        {
            Type type = typeof(T);
            PropertyDescriptorCollection meta = TypeDescriptor.GetProperties(type);

            List<PropertyDescriptor> properties = (from p in meta.Cast<PropertyDescriptor>()
                                                   where (p.Name.ToLower() != "state")
                                                   select p).ToList();

            DataTable table = GetTableFromType(type.Name, properties);

            foreach (var obj in objects)
            {
                DataRow row = table.NewRow();

                foreach (var item in properties)
                {
                    if (table.Columns.Contains(item.Name))
                    {
                        row[item.Name] = item.GetValue(obj);
                    }
                }

                table.Rows.Add(row);
            }

            return table;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static DataTable GetTableFromObjects<T>(ICollection<T> collection)
        {
            return GetTableFromObjects(collection.AsEnumerable());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DataTable GetTableFromObject<T>(T obj)
        {
            return GetTableFromObjects(Enumerable.Repeat(obj, 1));
        }

        public static DataTable GetTableFromObjects<T>(IEnumerable<T> objects, params string[] columnNames)
        {
            int count = 0;
            DataTable table = GetTableFromObjects<T>(objects);
            foreach (string columnName in columnNames)
            {
                table.Columns[count++].ColumnName = columnName;
            }
            return table;
        }

        public static bool IsDecimal(string n)
        {
            try
            {
                Decimal.Parse(n.Trim());
                return true;
            }
            catch { return false; }
        }

        public static bool IsInteger(string n)
        {
            try
            {
                Int32.Parse(n.Trim());
                return true;
            }
            catch { return false; }
        }

        public static bool IsLong(string n)
        {
            try
            {
                Int64.Parse(n.Trim());
                return true;
            }
            catch { return false; }
        }

        public static bool IsShort(string n)
        {
            try
            {
                Int16.Parse(n.Trim());
                return true;
            }
            catch { return false; }
        }

        public static DataTable ToDataTable<T, E>(this ICollection<T> collection, Func<T, E> expression)
        {
            if (collection == null) return null;

            var dt = new DataTable();

            foreach (T item in collection)
            {
                var dr = dt.NewRow();
                var entity = expression(item);
                var properties = entity.GetType().GetProperties();

                foreach (var prop in properties)
                {
                    var type = prop.PropertyType.IsNullable() ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType;

                    if (!dt.Columns.Contains(prop.Name)) dt.Columns.Add(prop.Name, type);

                    dr.SetField(prop.Name, prop.GetValue(entity, null));
                }

                dt.Rows.Add(dr);
            }

            return collection.Count.Equals(0) ? null : dt;
        }

        public static bool IsNullable(this Type t)
        {
            if (t.FullName == "System.String") return false;

            return (!t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>)));
        }

        public static DataTable ConvertTo<T>(ICollection<T> list)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (T item in list)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }

            return table;
        }

        public static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            return table;
        }
    }
}

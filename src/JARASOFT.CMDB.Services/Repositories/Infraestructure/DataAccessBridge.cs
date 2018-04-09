using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JARASOFT.CMDB.Services.Repositories.Infraestructure
{
    public class DataAccessBridge : IDataAccessBridge
    {
        public DataAccessBridge()
        {
            //DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());
        }

        #region Internal Models

        internal class ObjectParameter : DynamicParameters
        {
            public ObjectParameter(IDictionary<string, object> parameters)
            {
                if (parameters != null)
                {
                    foreach (var item in parameters)
                    {
                        if (!item.Key.StartsWith("@"))
                        {
                            this.Add("@" + item.Key, item.Value);
                        }
                        else
                        {
                            this.Add(item.Key, item.Value);
                        }
                    }
                }
            }
        }

        #endregion Internal Models

        #region Internal Methods

        private IDictionary<string, object> ConvertToDictionary(object Parameters)
        {
            if (Parameters is IDictionary<string, object>)
                return Parameters as IDictionary<string, object>;

            var result = new Dictionary<string, object>();
            if (Parameters == null)
                return result;

            var type = Parameters.GetType();
            foreach (var item in type.GetProperties())
            {
                var value = item.GetValue(Parameters, null);
                result.Add(item.Name, value);
            }
            return result;
        }

        #endregion Internal Methods

        public IDictionary<string, object> ExecuteProcedure(string DataSource, string Procedure, object Parameters, int? Timeout = 30)
        {
            return this.ExecuteList(DataSource, Procedure, Timeout).FirstOrDefault();
        }

        public void ExecuteNonQuery(string DataSource, string Procedure, object Parameters, int? Timeout = 30)
        {
            this.ExecuteNonQuery(DataSource, Procedure, ConvertToDictionary(Parameters), Timeout);
        }

        public void ExecuteNonQuery(string DataSource, string Procedure, IDictionary<string, object> Parameters, int? Timeout = 30)
        {
            int result = 0;

            var watch = new Stopwatch();
            watch.Start();
            var database = DatabaseFactory.CreateDatabase(DataSource);
            using (var connection = (SqlConnection)database.CreateConnection())
            {
                result = connection.Execute(Procedure, new ObjectParameter(Parameters),
                    commandType: CommandType.StoredProcedure);
            }
            watch.Stop();

            DataAccessTraceBridge.Log.TraceExecute(database.ConnectionString,
                Procedure, watch.ElapsedMilliseconds, Parameters);
        }

        public void ExecuteSqlNonQuery(string DataSource, string Sql, IDictionary<string, object> Parameters, int? Timeout = 30)
        {
            int result = 0;

            var watch = new Stopwatch();
            watch.Start();
            var database = DatabaseFactory.CreateDatabase(DataSource);
            using (var connection = (SqlConnection)database.CreateConnection())
            {
                result = connection.Execute(Sql, new ObjectParameter(Parameters),
                    commandType: CommandType.Text);
            }
            watch.Stop();

            DataAccessTraceBridge.Log.TraceExecute(database.ConnectionString,
                Sql, watch.ElapsedMilliseconds, Parameters);
        }

        public object ExecuteScalar(string DataSource, string Procedure, object Parameters, int? TimeOut = 30)
        {
            return this.ExecuteScalar(DataSource, Procedure, ConvertToDictionary(Parameters), TimeOut);
        }

        public object ExecuteSqlScalar(string DataSource, string Sql, IDictionary<string, object> Parameters, int? Timeout = 30)
        {
            var watch = new Stopwatch();
            watch.Start();

            object result = null;

            var database = DatabaseFactory.CreateDatabase(DataSource);
            using (var connection = (SqlConnection)database.CreateConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = Sql;
                cmd.CommandType = CommandType.Text;

                foreach (var item in Parameters)
                {
                    cmd.Parameters.AddWithValue(item.Key, item.Value);
                }
                connection.Open();

                result = cmd.ExecuteScalar();
            }

            watch.Stop();
            DataAccessTraceBridge.Log.TraceExecute(database.ConnectionString,
                Sql, watch.ElapsedMilliseconds, Parameters);

            return result;
        }

        public object ExecuteScalar(string DataSource, string Procedure, IDictionary<string, object> Parameters, int? Timeout = 30)
        {
            var watch = new Stopwatch();
            watch.Start();

            object result = null;

            var database = DatabaseFactory.CreateDatabase(DataSource);
            using (var connection = (SqlConnection)database.CreateConnection())
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = Procedure;
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (var item in Parameters)
                {
                    cmd.Parameters.AddWithValue(item.Key, item.Value);
                }
                connection.Open();

                result = cmd.ExecuteScalar();
            }

            watch.Stop();
            DataAccessTraceBridge.Log.TraceExecute(database.ConnectionString,
                Procedure, watch.ElapsedMilliseconds, Parameters);

            return result;
        }

        public IEnumerable<dynamic> ExecuteList(string DataSource, string Procedure, object Parameters, int? Timeout = 30)
        {
            return this.ExecuteList<dynamic>(DataSource, Procedure, ConvertToDictionary(Parameters), Timeout);
        }

        public DataSet ExecuteDataset(string DataSource, string Procedure, object Parameters, int? Timeout = 30)
        {
            var watch = new Stopwatch();
            watch.Start();

            var database = DatabaseFactory.CreateDatabase(DataSource);
            var command = new SqlCommand();
            command.CommandText = Procedure;
            command.CommandType = CommandType.StoredProcedure;
            if (Timeout != null) command.CommandTimeout = Timeout.GetValueOrDefault();
            ObjectToParameterToCommand(Parameters, command);

            var result = database.ExecuteDataSet(command);
            watch.Stop();
            return result;
        }

        private void ObjectToParameterToCommand(object Parameters, SqlCommand command)
        {
            var type = Parameters.GetType();
            foreach (var item in type.GetProperties())
            {
                var value = item.GetValue(Parameters, null);
                if (value is ICollection)
                {
                }
                else
                {
                    command.Parameters.AddWithValue(item.Name, value ?? DBNull.Value);
                }
            }
        }

        #region Execute Reader

        public IDataReader ExecuteReader(string DataSource, string Procedure,
            object Parameters, int? Timeout = 30)
        {
            SqlConnection connection = null;
            try
            {
                var database = DatabaseFactory.CreateDatabase(DataSource);
                connection = (SqlConnection)database.CreateConnection();
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = Procedure;
                cmd.CommandType = CommandType.StoredProcedure;
                ObjectToParameterToCommand(Parameters, cmd);
                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch (Exception ex)
            {
                if (connection != null) connection.Close();
                throw;
            }
        }

        public IDataReader ExecuteReader(string DataSource,
            string Procedure, object Parameters, string OutputName, out object Output, int? Timeout = 30)
        {
            Output = null;
            SqlConnection connection = null;
            try
            {
                var database = DatabaseFactory.CreateDatabase(DataSource);
                connection = (SqlConnection)database.CreateConnection();
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = Procedure;
                cmd.CommandType = CommandType.StoredProcedure;
                ObjectToParameterToCommand(Parameters, cmd);

                if (!string.IsNullOrWhiteSpace(OutputName))
                {
                    var outputParameter = new SqlParameter(OutputName, SqlDbType.BigInt);
                    outputParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParameter);
                }

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (OutputName != null)
                {
                    Output = cmd.Parameters[OutputName].Value;
                }

                return reader;
            }
            catch (Exception ex)
            {
                if (connection != null) connection.Close();
                throw;
            }
        }

        #endregion Execute Reader

        public T ExecuteProcedure<T>(string DataSource, string Procedure, IDictionary<string, object> Parameters, int? Timeout = 30) where T : class
        {
            var watch = new Stopwatch();
            watch.Start();
            T result = default(T);
            var database = DatabaseFactory.CreateDatabase(DataSource);
            using (var connection = (SqlConnection)database.CreateConnection())
            {
                result = connection.Query<T>(Procedure, new ObjectParameter(Parameters),
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
            watch.Stop();
            DataAccessTraceBridge.Log.TraceExecute(database.ConnectionString,
                Procedure, watch.ElapsedMilliseconds, Parameters);
            return result;
        }

        public T ExecuteProcedure<T>(string DataSource, string Procedure, object Parameters, int? Timeout = 30) where T : class
        {
            return this.ExecuteProcedure<T>(DataSource, Procedure, ConvertToDictionary(Parameters), Timeout);
        }

        public string ExecuteAsJson(string DataSource, string Procedure, object Parameters, int? Timeout = 30, bool CamelCase = false)
        {
            return this.ExecuteAsJson(DataSource, Procedure, ConvertToDictionary(Parameters), Timeout, CamelCase);
        }

        public string ExecuteAsMetadataJson(string DataSource, string Procedure, object Parameters, int? Timeout = 30, bool CamelCase = false)
        {
            return this.ExecuteAsMetadataJson(DataSource, Procedure, ConvertToDictionary(Parameters), Timeout, CamelCase);
        }

        public string ExecuteRowAsJson(string DataSource, string Procedure, object Parameters, int? Timeout = 30, bool CamelCase = false)
        {
            return this.ExecuteRowAsJson(DataSource, Procedure, ConvertToDictionary(Parameters), Timeout, CamelCase);
        }

        public string ExecuteRowAsJson(string DataSource, string Procedure, IDictionary<string, object> Parameters, int? Timeout = 30, bool CamelCase = false)
        {
            var watch = new Stopwatch();
            watch.Start();
            string result = null;
            var database = DatabaseFactory.CreateDatabase(DataSource);
            using (var connection = (SqlConnection)database.CreateConnection())
            {
                var model = connection.Query<dynamic>(Procedure, new ObjectParameter(Parameters),
                    commandType: CommandType.StoredProcedure);
                if (CamelCase)
                {
                    var settings = new JsonSerializerSettings();
                    settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    result = JsonConvert.SerializeObject(model == null ? model : model.FirstOrDefault(), settings);
                }
                else
                {
                    result = JsonConvert.SerializeObject(model == null ? model : model.FirstOrDefault());
                }
            }
            watch.Stop();
            DataAccessTraceBridge.Log.TraceExecute(database.ConnectionString, Procedure, watch.ElapsedMilliseconds, Parameters);
            return result;
        }

        public string ExecuteAsJson(string DataSource, string Procedure, IDictionary<string, object> Parameters, int? Timeout = 30, bool CamelCase = false)
        {
            var watch = new Stopwatch();
            watch.Start();
            string result = null;
            var database = DatabaseFactory.CreateDatabase(DataSource);
            using (var connection = (SqlConnection)database.CreateConnection())
            {
                var model = connection.Query<dynamic>(Procedure, new ObjectParameter(Parameters),
                    commandType: CommandType.StoredProcedure);
                if (CamelCase)
                {
                    var settings = new JsonSerializerSettings();
                    settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    result = JsonConvert.SerializeObject(model, settings);
                }
                else
                {
                    result = JsonConvert.SerializeObject(model);
                }
            }
            watch.Stop();
            DataAccessTraceBridge.Log.TraceExecute(database.ConnectionString, Procedure, watch.ElapsedMilliseconds, Parameters);
            return result;
        }

        public string ExecuteSqlAsJson(string DataSource, string Procedure, IDictionary<string, object> Parameters, int? Timeout = 30, bool CamelCase = false)
        {
            var watch = new Stopwatch();
            watch.Start();
            string result = null;
            var database = DatabaseFactory.CreateDatabase(DataSource);
            using (var connection = (SqlConnection)database.CreateConnection())
            {
                var model = connection.Query<dynamic>(Procedure, new ObjectParameter(Parameters),
                    commandType: CommandType.Text);

                if (CamelCase)
                {
                    var settings = new JsonSerializerSettings();
                    settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    result = JsonConvert.SerializeObject(model, settings);
                }
                else
                {
                    result = JsonConvert.SerializeObject(model);
                }
            }
            watch.Stop();
            DataAccessTraceBridge.Log.TraceExecute(database.ConnectionString, Procedure, watch.ElapsedMilliseconds, Parameters);
            return result;
        }

        public string ExecuteAsMetadataJson(string DataSource, string Procedure, IDictionary<string, object> Parameters, int? Timeout = 30, bool CamelCase = false)
        {
            var watch = new Stopwatch();
            watch.Start();
            string result = null;
            var database = DatabaseFactory.CreateDatabase(DataSource);
            using (var connection = (SqlConnection)database.CreateConnection())
            {
                var model = connection.Query<dynamic>(Procedure, new ObjectParameter(Parameters),
                    commandType: CommandType.StoredProcedure);
                int totalCount = 0;
                totalCount = (model.Count() > 0 ? model.First().total ?? model.First().Total ?? model.First().totalSize ?? model.First().TotalSize ?? model.First().totalRows ?? model.First().TotalRows ?? 0 : 0);
                var metadata = new { data = model, total = totalCount };
                if (CamelCase)
                {
                    var settings = new JsonSerializerSettings();
                    settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    result = JsonConvert.SerializeObject(metadata, settings);
                }
                else
                {
                    result = JsonConvert.SerializeObject(metadata);
                }
            }
            watch.Stop();
            DataAccessTraceBridge.Log.TraceExecute(database.ConnectionString, Procedure, watch.ElapsedMilliseconds, Parameters);
            return result;
        }

        #region List

        public IEnumerable<T> ExecuteList<T>(string DataSource, string Procedure, object Parameters,
            Func<IDataReader, T> Reader, int? Timeout = 30) where T : class
        {
            int output = 0;
            return this.ExecuteList<T>(DataSource, Procedure, Parameters, Reader,
                null, out output, Timeout);
        }

        public IEnumerable<T> ExecuteList<T>(string DataSource, string Procedure, object Parameters,
            Func<IDataReader, T> Reader,
            string OutputName, out int Output, int? Timeout = 30) where T : class
        {
            var watch = new Stopwatch();
            watch.Start();

            Output = 0;

            List<T> result = new List<T>();
            if (Reader == null) return result;
            SqlConnection connection = null;

            var database = DatabaseFactory.CreateDatabase(DataSource);
            try
            {
                connection = (SqlConnection)database.CreateConnection();
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = Procedure;
                cmd.CommandType = CommandType.StoredProcedure;
                ObjectToParameterToCommand(Parameters, cmd);
                if (OutputName != null)
                {
                    var pout = new SqlParameter(OutputName, SqlDbType.Int);
                    pout.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(pout);
                }

                using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        var model = Reader.Invoke(reader);
                        if (model != null)
                            result.Add(model);
                    }
                    reader.Close();
                }
                if (OutputName != null)
                {
                    Output = (int)cmd.Parameters[OutputName].Value;
                }
            }
            catch (Exception ex)
            {
                if (connection != null) connection.Close();
                throw;
            }
            watch.Stop();
            DataAccessTraceBridge.Log.TraceExecute(database.ConnectionString, Procedure, watch.ElapsedMilliseconds, ConvertToDictionary(Parameters));

            return result;
        }

        public IEnumerable<T> ExecuteList<T>(string DataSource, string Procedure, IDictionary<string, object> Parameters, int? Timeout = 30) where T : class
        {
            var watch = new Stopwatch();
            watch.Start();
            IEnumerable<T> result = new List<T>();
            var database = DatabaseFactory.CreateDatabase(DataSource);
            using (var connection = (SqlConnection)database.CreateConnection())
            {
                result = connection.Query<T>(Procedure, new ObjectParameter(Parameters),
                    commandType: CommandType.StoredProcedure, commandTimeout: Timeout);
            }
            watch.Stop();
            DataAccessTraceBridge.Log.TraceExecute(database.ConnectionString, Procedure, watch.ElapsedMilliseconds, Parameters);
            return result;
        }

        public IEnumerable<T> ExecuteSqlList<T>(string DataSource, string Sql, IDictionary<string, object> Parameters, int? Timeout = 30) where T : class
        {
            var watch = new Stopwatch();
            watch.Start();
            IEnumerable<T> result = new List<T>();
            var database = DatabaseFactory.CreateDatabase(DataSource);
            using (var connection = (SqlConnection)database.CreateConnection())
            {
                result = connection.Query<T>(Sql, new ObjectParameter(Parameters),
                    commandType: CommandType.Text);
            }
            watch.Stop();
            DataAccessTraceBridge.Log.TraceExecute(database.ConnectionString, Sql, watch.ElapsedMilliseconds, Parameters);
            return result;
        }

        #region execute output parameters

        public IEnumerable<T> ExecuteList<T>(string DataSource, string Procedure, object Parameters, string OutputName, out object Output, int? Timeout = 30) where T : class
        {
            var watch = new Stopwatch();
            watch.Start();
            IEnumerable<T> result = new List<T>();
            var database = DatabaseFactory.CreateDatabase(DataSource);
            Output = null;
            using (var connection = (SqlConnection)database.CreateConnection())
            {
                result = connection.QueryDynamic<T>(Procedure, Parameters, OutputName, out Output);
            }
            watch.Stop();
            DataAccessTraceBridge.Log.TraceExecute(database.ConnectionString, Procedure, watch.ElapsedMilliseconds,
                new Object[] { Output });
            return result;
        }

        #endregion execute output parameters

        public IEnumerable<T> ExecuteList<T>(string DataSource, string Procedure, object Parameters, int? Timeout = 30) where T : class
        {
            return this.ExecuteList<T>(DataSource, Procedure, ConvertToDictionary(Parameters), Timeout);
        }

        #endregion List

        public IEnumerable<IEnumerable<dynamic>> ExecuteQueryMultiple(string DataSource, string Procedure, IDictionary<string, object> Parameters, int? Timeout = 30)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IEnumerable<dynamic>> ExecuteQueryMultiple(string connection, SqlCommand Command)
        {
            var watch = new Stopwatch();
            watch.Start();

            var result = new List<IEnumerable<dynamic>>();
            IDataReader reader = null;
            var con = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
            try
            {
                var database = new SqlDatabase(con);
                reader = database.ExecuteReader(Command);

                var grid = reader.QueryMultiple(Command);

                while (!reader.IsClosed)
                {
                    result.Add(grid.Read());
                }
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
            }

            watch.Stop();
            DataAccessTraceBridge.Log.TraceExecute(Command, watch.ElapsedMilliseconds);

            return result;
        }

        public Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>, IEnumerable<TThird>, IEnumerable<TFour>> ExecuteQueryMultiple<TFirst, TSecond, TThird, TFour>(string DataSource, string Procedure, IDictionary<string, object> Parameters, int? Timeout = 30)
        {
            throw new NotImplementedException();
        }

        public Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>, IEnumerable<TThird>> ExecuteQueryMultiple<TFirst, TSecond, TThird>(string DataSource, string Procedure, IDictionary<string, object> Parameters, int? Timeout = 30)
        {
            throw new NotImplementedException();
        }

        public Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>> ExecuteQueryMultiple<TFirst, TSecond>(string DataSource, string Procedure, IDictionary<string, object> Parameters, int? Timeout = 30)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace JARASOFT.CMDB.Services.Repositories.Infraestructure
{
    public interface IDataAccessBridge
    {
        #region Business

        #region "   Execute Store Procedure "

        IDictionary<string, object> ExecuteProcedure(string DataSource, string Procedure, object Parameters, int? TimeOut = 30);

        void ExecuteNonQuery(string DataSource, string Procedure, object Parameters, int? TimeOut = 30);

        void ExecuteNonQuery(string DataSource, string Procedure, IDictionary<string, object> Parameters, int? TimeOut = 30);

        void ExecuteSqlNonQuery(string DataSource, string Procedure, IDictionary<string, object> Parameters, int? TimeOut = 30);

        object ExecuteScalar(string DataSource, string Procedure, object Parameters, int? TimeOut = 30);

        object ExecuteScalar(string DataSource, string Procedure, IDictionary<string, object> Parameters, int? TimeOut = 30);

        #endregion "   Execute Store Procedure "

        #region "   Execute Store Procedure List    "

        IEnumerable<dynamic> ExecuteList(string DataSource, string Procedure, object Parameters, int? TimeOut = 30);

        DataSet ExecuteDataset(string DataSource, string Procedure, object Parameters, int? TimeOut = 30);

        IDataReader ExecuteReader(string DataSource, string Procedure, object Parameters, int? TimeOut = 30);

        IDataReader ExecuteReader(string DataSource, string Procedure, object Parameters, string OutputName, out object Output, int? TimeOut = 30);

        #endregion "   Execute Store Procedure List    "

        #endregion Business

        #region " =================== Execute Store Procedure ============= "

        T ExecuteProcedure<T>(string DataSource, string Procedure, IDictionary<string, object> Parameters, int? Timeout = 30) where T : class;

        T ExecuteProcedure<T>(string DataSource, string Procedure, object Parameters, int? Timeout = 30) where T : class;

        #endregion " =================== Execute Store Procedure ============= "

        #region " =================== Execute Store Procedure List ============= "

        IEnumerable<T> ExecuteList<T>(string DataSource, string Procedure, IDictionary<string, object> Parameters, int? Timeout = 30) where T : class;

        IEnumerable<T> ExecuteSqlList<T>(string DataSource, string Sql, IDictionary<string, object> Parameters, int? Timeout = 30) where T : class;

        IEnumerable<T> ExecuteList<T>(string DataSource, string Procedure, object Parameters, int? Timeout = 30) where T : class;

        IEnumerable<T> ExecuteList<T>(string DataSource, string Procedure, object Parameters, Func<IDataReader, T> Reader, int? Timeout = 30) where T : class;

        IEnumerable<T> ExecuteList<T>(string DataSource, string Procedure, object Parameters, Func<IDataReader, T> Reader, string OutputName, out int Output, int? Timeout = 30) where T : class;

        IEnumerable<T> ExecuteList<T>(string DataSource, string Procedure, object Parameters, string OutputName, out object Output, int? Timeout = 30) where T : class;

        #endregion " =================== Execute Store Procedure List ============= "

        #region " =================== Execute Store Procedure Multiple ============= "

        IEnumerable<IEnumerable<dynamic>> ExecuteQueryMultiple(string DataSource, string Procedure, IDictionary<string, object> Parameters, int? Timeout = 30);

        Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>, IEnumerable<TThird>, IEnumerable<TFour>> ExecuteQueryMultiple<TFirst, TSecond, TThird, TFour>(string DataSource, string Procedure, IDictionary<string, object> Parameters, int? Timeout = 30);

        Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>, IEnumerable<TThird>> ExecuteQueryMultiple<TFirst, TSecond, TThird>(string DataSource, string Procedure, IDictionary<string, object> Parameters, int? Timeout = 30);

        Tuple<IEnumerable<TFirst>, IEnumerable<TSecond>> ExecuteQueryMultiple<TFirst, TSecond>(string DataSource, string Procedure, IDictionary<string, object> Parameters, int? Timeout = 30);

        IEnumerable<IEnumerable<dynamic>> ExecuteQueryMultiple(string connection, SqlCommand Command);

        #endregion " =================== Execute Store Procedure Multiple ============= "

        #region Json

        string ExecuteAsJson(string DataSource, string Procedure, IDictionary<string, object> Parameters, int? Timeout = 30, bool CamelCase = false);

        string ExecuteAsJson(string DataSource, string Procedure, object Parameters, int? Timeout = 30, bool CamelCase = false);

        string ExecuteRowAsJson(string DataSource, string Procedure, object Parameters, int? Timeout = 30, bool CamelCase = false);

        string ExecuteSqlAsJson(string DataSource, string Procedure, IDictionary<string, object> Parameters, int? Timeout = 30, bool CamelCase = false);

        string ExecuteAsMetadataJson(string DataSource, string Procedure, IDictionary<string, object> Parameters, int? Timeout = 30, bool CamelCase = false);

        string ExecuteAsMetadataJson(string DataSource, string Procedure, object Parameters, int? Timeout = 30, bool CamelCase = false);

        #endregion Json
    }
}

using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace JARASOFT.CMDB.Services.Repositories.Infraestructure
{
    static partial class SqlMapper
    {
        public static GridReader QueryMultiple(this IDataReader reader,
            SqlCommand command)
        {
            Identity identity = new Identity(command.CommandText, command.CommandType, command.Connection, null, null, null);
            CacheInfo info = GetCacheInfo(identity, null, false);
            var result = new GridReader(command, reader, identity, null);
            return result;
        }

        public static T Read<T>(this IDataReader reader,
            IDbConnection Connection, string sql, CommandType commandType)
        {
            var identity = new Identity(sql, commandType, Connection, typeof(T), null, null);
            var info = GetCacheInfo(identity, null, false);
            var tuple = info.Deserializer;
            int hash = GetColumnHash(reader);
            if (tuple.Func == null || tuple.Hash != hash)
            {
                tuple = info.Deserializer = new DeserializerState(hash, GetDeserializer(typeof(T), reader, 0, -1, false));
                SetQueryCache(identity, info);
            }
            var func = tuple.Func;
            return (T)func(reader);
        }

        public static IEnumerable<T> Reads<T>(this IDataReader reader,
            IDbConnection Connection, string sql, CommandType CommandType)
        {
            var identity = new Identity(sql, CommandType, Connection, typeof(T), null, null);
            var info = GetCacheInfo(identity, null, false);
            var tuple = info.Deserializer;
            int hash = GetColumnHash(reader);
            if (tuple.Func == null || tuple.Hash != hash)
            {
                tuple = info.Deserializer = new DeserializerState(hash, GetDeserializer(typeof(T), reader, 0, -1, false));
                SetQueryCache(identity, info);
            }
            var func = tuple.Func;
            while (reader.Read())
            {
                yield return (T)func(reader);
            }
        }

        public static IEnumerable<T> Query<T>(this IDataReader reader,
            CommandType CommandType,
            IDbConnection Connection, string sql)
        {
            var identity = new Identity(sql, CommandType, Connection, typeof(T), null, null);
            var info = GetCacheInfo(identity, null, false);
            try
            {
                // with the CloseConnection flag, so the reader will deal with the connection; we
                // still need something in the "finally" to ensure that broken SQL still results
                // in the connection closing itself
                var tuple = info.Deserializer;
                int hash = GetColumnHash(reader);
                if (tuple.Func == null || tuple.Hash != hash)
                {
                    tuple = info.Deserializer = new DeserializerState(hash, GetDeserializer(typeof(T), reader, 0, -1, false));
                    SetQueryCache(identity, info);
                }
                var func = tuple.Func;

                while (reader.Read())
                {
                    yield return (T)func(reader);
                }
                // happy path; close the reader cleanly - no
                // need for "Cancel" etc
                reader.Dispose();
                reader = null;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                    reader.Dispose();
                }

            }
        }

        public static IEnumerable<T> QueryDynamic<T>(this SqlConnection cnn,
                string sql,
                object param,
                string OutputName,
                out object Output,
                int? commandTimeout = 30
                )
        {
            List<T> result = new List<T>();

            SqlTransaction transaction = null;
            CommandType commandType = CommandType.StoredProcedure;
            var identity = new Identity(sql, commandType, cnn, typeof(T), param == null ? null : param.GetType(), null);
            var info = GetCacheInfo(identity, param, false);
            IDbCommand cmd = null;
            IDataReader reader = null;

            bool wasClosed = cnn.State == ConnectionState.Closed;
            try
            {
                CommandDefinition definition = new CommandDefinition(sql, param, null, commandTimeout, commandType, CommandFlags.Buffered);

                cmd = definition.SetupCommand(cnn, info.ParamReader);

                var poutput = new SqlParameter(OutputName, SqlDbType.VarChar, 8000);
                poutput.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(poutput);

                if (wasClosed) cnn.Open();
                reader = cmd.ExecuteReader(wasClosed ? CommandBehavior.CloseConnection : CommandBehavior.Default);
                wasClosed = false; // *if* the connection was closed and we got this far, then we now have a reader
                                   // with the CloseConnection flag, so the reader will deal with the connection; we
                                   // still need something in the "finally" to ensure that broken SQL still results
                                   // in the connection closing itself
                var tuple = info.Deserializer;
                int hash = GetColumnHash(reader);
                if (tuple.Func == null || tuple.Hash != hash)
                {
                    tuple = info.Deserializer = new DeserializerState(hash, GetDeserializer(typeof(T), reader, 0, -1, false));
                    SetQueryCache(identity, info);
                }

                var func = tuple.Func;

                while (reader.Read())
                {
                    result.Add((T)func(reader));
                }
                // happy path; close the reader cleanly - no
                // need for "Cancel" etc
                reader.Dispose();
                reader = null;

                Output = ((DbParameter)cmd.Parameters[OutputName]).Value;
            }
            finally
            {
                if (reader != null)
                {
                    if (!reader.IsClosed)
                        try { cmd.Cancel(); }
                        catch { /* don't spoil the existing exception */ }
                    reader.Dispose();
                }
                if (wasClosed) cnn.Close();
                if (cmd != null) cmd.Dispose();
            }
            return result;
        }
    }
}
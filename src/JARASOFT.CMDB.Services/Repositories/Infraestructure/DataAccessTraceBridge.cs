using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARASOFT.CMDB.Services.Repositories.Infraestructure
{
    public partial class DataAccessTraceBridge
    {
        private static Lazy<DataAccessTraceBridge> _log = new Lazy<DataAccessTraceBridge>(() => {
            var ins = new DataAccessTraceBridge();

            //ins.ConfigurationId = ConfigurationManager.Appsettings[TraceConstants.ENABLE_TRACE_CONFIGURATIONID] ?? TraceConstants.NODATA;

            return ins;
        });

        private string ConfigurationId;

        public static DataAccessTraceBridge Log
        {
            get
            {
                return _log.Value;
            }
        }

        public void TraceExecute(string dataSourceName, string procedure, long elapsed, object[] parameters, string comment = "")
        {
            try
            {
                //if (TraceUtilBridge.Instance,IsEnabled(ConfigurationId, TraceSinkConstants.TRACE_DATAACCESS))
                //    DataAccessEventSource.Log.TraceExecute(dataSourceName, procedure, elapsed, parameters, comment);
            }
            catch (Exception ex)
            {
                SwallowException(ex);
            }
        }

        public void TraceExecute(string dataSourceName, string procedure, long elapsed, IDictionary<string, object> parameters, string comment = "")
        {
            try
            {
                //if (TraceUtilBridge.Instance,IsEnabled(ConfigurationId, TraceSinkConstants.TRACE_DATAACCESS))
                //    DataAccessEventSource.Log.TraceExecute(dataSourceName, procedure, elapsed, null, comment);
            }
            catch (Exception ex)
            {
                SwallowException(ex);
            }
        }

        public void TraceExecute(System.Data.IDbCommand command, long elapsed, string comment = "")
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                string TimeExecutionStoreProcedure = "Duration: " + elapsed.ToString() + " (" + command.CommandText + ")";
                System.Diagnostics.Debug.WriteLine(TimeExecutionStoreProcedure);
                System.Console.WriteLine(TimeExecutionStoreProcedure);
            }
#endif
            try
            {
                //using (var ts2 = new TransactionScope(TransactionScopeOption.Suppress))
                //{
                //    if (TraceUtilBridge.Instance.IsEnabled(ConfigurationId, TraceSinkConstants.TRACE_DATAACCESS))
                //        DataAccessEventSource.Log.TraceExecute(command, elapsed, comment);
                //}
            }
            catch (Exception ex)
            {
                SwallowException(ex);
            }
        }

        private void SwallowException(Exception ex)
        {
            // Swallow exception
        }
    }
}


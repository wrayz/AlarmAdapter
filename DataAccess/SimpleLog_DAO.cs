using ModelLibrary;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    /// <summary>
    /// 簡易記錄資料處理
    /// </summary>
    public class SimpleLog_DAO : GenericDataAccess<SimpleLog>
    {
        public SimpleLog ModifyLog(SimpleLog log, string type)
        {
            var connectionString = GetConnectionString();

            // Sql connection
            var connection = new SqlConnection(connectionString);

            // Execute
            try
            {
                // Open connection
                connection.Open();
                // Execute transaction
                var result = ExecuteModify(log, type, connection);
                // Close connection
                connection.Close();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Release
                connection.Close();
                connection.Dispose();
            }
        }

        /// <summary>
        /// 取得 connection string
        /// </summary>
        /// <returns></returns>
        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        }

        /// <summary>
        /// 執行 Modify transaction
        /// </summary>
        /// <param name="log"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        private SimpleLog ExecuteModify(SimpleLog log, string type, SqlConnection connection)
        {
            // Begin transaction
            var transaction = connection.BeginTransaction();
            var procedure = type == "C" ? "dms.SPC_SIMPLE_LOG_INSERT" : "dms.SPC_LOGMASTER_INSERT";

            try
            {
                //SQL執行語法
                var cmd = new SqlCommand(procedure, connection, transaction) { CommandType = CommandType.StoredProcedure };

                //執行參數
                cmd.Parameters.Add("@DEVICE_SN", SqlDbType.VarChar).Value = log.DEVICE_SN;
                cmd.Parameters.Add("@ERROR_TIME", SqlDbType.DateTime).Value = log.ERROR_TIME;
                cmd.Parameters.Add("@ERROR_INFO", SqlDbType.NVarChar).Value = log.ERROR_INFO;
                cmd.Parameters.Add("@LOG_SN", SqlDbType.Int).Direction = ParameterDirection.Output;

                // Execute
                cmd.ExecuteNonQuery();
                
                log.LOG_SN = (int?)(cmd.Parameters["@LOG_SN"].Value == DBNull.Value ? null : cmd.Parameters["@LOG_SN"].Value);

                // Commit
                transaction.Commit();

                return log;
            }
            catch (Exception ex)
            {
                if (transaction.Connection != null)
                    // Rollback
                    transaction.Rollback();
                throw ex;
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
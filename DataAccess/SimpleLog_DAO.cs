﻿using ModelLibrary;
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
        public SimpleLog ModifyLog(SimpleLog log)
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
                var result = ExecuteModify(log, connection);
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
        private SimpleLog ExecuteModify(SimpleLog log, SqlConnection connection)
        {
            // Begin transaction
            var transaction = connection.BeginTransaction();

            try
            {
                //SQL執行語法
                var cmd = new SqlCommand("dms.SPC_SIMPLE_LOG_INSERT", connection, transaction) { CommandType = CommandType.StoredProcedure };

                //執行參數
                cmd.Parameters.Add("@DEVICE_SN", SqlDbType.VarChar).Value = log.DEVICE_SN;
                cmd.Parameters.Add("@ERROR_TIME", SqlDbType.DateTime).Value = log.ERROR_TIME;
                cmd.Parameters.Add("@ERROR_INFO", SqlDbType.NVarChar).Value = log.ERROR_INFO;

                // Execute
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    log.LOG_SN = Convert.ToInt32(reader["LOG_SN"]);
                }
                reader.Close();

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
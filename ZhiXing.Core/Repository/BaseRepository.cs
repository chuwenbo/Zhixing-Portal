using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ZhiXing.Core.Repository
{
    public class BaseRepository
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
         
        public static SqlConnection GetSqlConnection()
        {
            SqlConnection sqlConnecton = new SqlConnection(ConnectionString);
            sqlConnecton.Open(); 
            return sqlConnecton;
        }

        public static DataTable ExecuteDataTable(string commandText)
        {
            DataTable dt = new DataTable();

            try
            {
                using (var sqlConnection = GetSqlConnection())
                {  
                    SqlDataAdapter adapter = new SqlDataAdapter(commandText, sqlConnection);
                    adapter.Fill(dt);
                }
            }
            catch
            {
                //TODO: log x
            } 

            return dt;
        }

        public static int ExecuteNonQuery(string commandText)
        {
            int executeCount = 0;

            try
            {
                using (var sqlConnection = GetSqlConnection())
                {
                    SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection);

                    executeCount = sqlCommand.ExecuteNonQuery();
                }

            }
            catch
            {
                //TODO: log
            }

            return executeCount;
        }
    }
}

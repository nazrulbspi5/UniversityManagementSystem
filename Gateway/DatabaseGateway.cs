using System;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace UoUWebApp.Gateway
{
    public class DatabaseGateway
    {
        private string connectionString;
        protected SqlConnection connection;
        protected SqlCommand command;
        protected SqlDataReader reader;

        protected DatabaseGateway()
        {
            connectionString = WebConfigurationManager.ConnectionStrings["UoUDBContext"].ConnectionString;
            connection = new SqlConnection(connectionString);
            command = new SqlCommand();
            command.Connection = connection;
        }

        private void OpenConnection()
        {
            connection.Open();
        }

        private void CloseConnection()
        {
            connection.Close();
        }

        protected void ExecuteQuery()
        {
            OpenConnection();
            reader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        }

        protected int ExecuteNonQuery()
        {
            OpenConnection();
            int result = command.ExecuteNonQuery();
            CloseConnection();
            return result;
        }

        protected long ExecuteNonQuery(bool insert)
        {
            OpenConnection();
            long result = 0;
            if (insert)
                result = Convert.ToInt64(command.ExecuteScalar());
            else
                ExecuteNonQuery();
            CloseConnection();
            return result;
        }
    }
}
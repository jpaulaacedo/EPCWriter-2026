using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPCReaderWriter.Models;
using MySql.Data.MySqlClient;

namespace EPCReaderWriter.Helpers
{
    public static class DatabaseConnectionHelper
    {
        public static DatabaseConnectionResultModel TestConnection(string connectionString)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                }

                return new DatabaseConnectionResultModel
                {
                    Connected = true
                };
            }
            catch (Exception ex)
            {
                return new DatabaseConnectionResultModel
                {
                    Connected = false,
                    Errors = new DatabaseConnectionErrorModel[]
                    {
                        new DatabaseConnectionErrorModel
                        {
                            Message = ex.Message
                        }
                    }
                };
            }
        }
    }
}

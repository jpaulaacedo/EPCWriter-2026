using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPCReaderWriter.Models;
using MySql.Data.MySqlClient;

namespace EPCReaderWriter.Repositories
{
    public class UserRepository : RepositoryBase
    {
        public UserRepository()
        {
        }

        public IList<UserModel> Get()
        {
            var result = new List<UserModel>();

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                var query = "SELECT `id`,"
                            + "`firstname`,"
                            + "`lastname`"
                            + "FROM `users`";

                MySqlCommand command = new MySqlCommand(query, connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new UserModel()
                        {
                            UserId = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2)
                        });
                    }
                }
            }

            return result;
        }
    }
}
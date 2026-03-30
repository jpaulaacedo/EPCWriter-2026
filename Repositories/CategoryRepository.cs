using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPCReaderWriter.Models;
using MySql.Data.MySqlClient;

namespace EPCReaderWriter.Repositories
{
    public class CategoryRepository : RepositoryBase
    {
        public CategoryRepository()
        {
        }

        public IList<CategoryModel> Get()
        {
            var result = new List<CategoryModel>();

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                var query = "SELECT `id`,"
                            + "`category_name`"
                            + "FROM `categories`";

                MySqlCommand command = new MySqlCommand(query, connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new CategoryModel()
                        {
                            CategoryId = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        });
                    }
                }
            }

            return result;
        }
    }
}
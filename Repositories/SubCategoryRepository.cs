using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPCReaderWriter.Models;
using MySql.Data.MySqlClient;

namespace EPCReaderWriter.Repositories
{
    public class SubCategoryRepository : RepositoryBase
    {
        public SubCategoryRepository()
        {

        }

        public IList<SubCategoryModel> Get(int categoryId)
        {
            var result = new List<SubCategoryModel>();

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                var query = "SELECT `id`,"
                            + "`item_name`"
                            + "FROM `items`"
                            + "WHERE `category_id` = @categoryid";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@categoryid", categoryId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new SubCategoryModel()
                        {
                            SubCategoryId = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        });
                    }
                }
            }

            return result;
        }

        public string GeneratePropertyNum(int itemId)
        {

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                var itemQuery = "SELECT `id`, `item_code` FROM `items` WHERE `id` = @id";
                MySqlCommand itemCommand = new MySqlCommand(itemQuery, connection);
                itemCommand.Parameters.AddWithValue("@id", itemId);

                using (var reader = itemCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Get the item code from the database
                        string itemCode = reader.GetString("item_code");
                        reader.Close();
                        // Count the number of inventories for the item
                        var inventoryQuery = "SELECT COUNT(*) FROM `inventories` WHERE `item_id` = @item_id";
                        MySqlCommand inventoryCommand = new MySqlCommand(inventoryQuery, connection);
                        inventoryCommand.Parameters.AddWithValue("@item_id", itemId);

                        int itemCount = Convert.ToInt32(inventoryCommand.ExecuteScalar());

                        // Generate the property number
                        string propertyNo = DateTime.Now.Year + "-" + itemCode + "-" + (itemCount + 1);
                        return propertyNo;

                    }
                }
            }

            return null;
        }
    }
}

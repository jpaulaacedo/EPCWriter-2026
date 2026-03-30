using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MySql.Data.MySqlClient;

namespace EPCReaderWriter.Repositories
{
    public class InventoryRepository : RepositoryBase
    {
        public InventoryRepository()
        {
        }

        public bool IsItemAlreadyAdded(string epc)
        {
            try
            {
                if (string.IsNullOrEmpty(epc))
                    return false;

                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();

                    var query = "SELECT COUNT(*) " +
                                "FROM inventories " +
                                "WHERE epc = @epc";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@epc", epc);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                return (reader.GetInt32(0) > 0);
                            }
                        }
                    }
                }

                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool SaveRecord(InventoryModel input)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (MySqlTransaction transaction = connection.BeginTransaction()) // Start transaction
                    {
                        try
                        {
                            // 1. Insert into `inventories`
                            var inventoryQuery = "INSERT INTO `inventories`" +
                                                 "(`item_id`, `epc`, `description`, `date_acquired`, `property_no`, `fund_cluster`, `unit_measure`, `unit_value`, `serviceable`, `qty_physical_card`, `qty_physical_count`, `lifespan`, `item_type`, `created_at`, `updated_at`) " +
                                                 "VALUES (@item_id, @epc, @description, @date_acquired, @property_no, @fund_cluster, @unit_measure, @unit_value, @serviceable, @qty_physical_card, @qty_physical_count, @lifespan, @item_type, NOW(), NOW()); " +
                                                 "SELECT LAST_INSERT_ID();";

                            long inventoryId;
                            using (MySqlCommand command = new MySqlCommand(inventoryQuery, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@item_id", input.SubCategoryId);
                                command.Parameters.AddWithValue("@epc", input.Epc);
                                command.Parameters.AddWithValue("@description", input.Description);
                                command.Parameters.AddWithValue("@date_acquired", input.DateAcquired);
                                command.Parameters.AddWithValue("@property_no", input.PropertyNumber);
                                command.Parameters.AddWithValue("@fund_cluster", input.FundCluster);
                                command.Parameters.AddWithValue("@unit_measure", input.UnitMeasure);
                                command.Parameters.AddWithValue("@unit_value", input.UnitValue);
                                command.Parameters.AddWithValue("@serviceable", input.Serviceable);
                                command.Parameters.AddWithValue("@qty_physical_card", input.QtyPhysicalCard);
                                command.Parameters.AddWithValue("@qty_physical_count", input.QtyPhysicalCount);
                                command.Parameters.AddWithValue("@lifespan", input.Lifespan);
                                command.Parameters.AddWithValue("@item_type", input.ItemType);

                                inventoryId = Convert.ToInt64(command.ExecuteScalar()); // Get the last inserted ID
                            }

                            // 2. Insert into `inv_location`
                            var locationQuery = "INSERT INTO `inv_location` (`location_id`, `inventory_id`, `remarks`) " +
                                                "VALUES (@location_id, @inventory_id, 'Updated via EPC Writer');";

                            using (MySqlCommand command = new MySqlCommand(locationQuery, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@location_id", input.LocationId);
                                command.Parameters.AddWithValue("@inventory_id", inventoryId);
                                command.ExecuteNonQuery();
                            }

                            // 3. Insert into `inv_user`
                            var userQuery = "INSERT INTO `inv_user` (`user_id`, `inventory_id`, `status`, `remarks`) " +
                                            "VALUES (@user_id, @inventory_id, 'Updated via EPC Writer', 'Auto');";

                            using (MySqlCommand command = new MySqlCommand(userQuery, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@user_id", input.UserId);
                                command.Parameters.AddWithValue("@inventory_id", inventoryId);
                                command.ExecuteNonQuery();
                            }

                            // Commit transaction
                            transaction.Commit();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback(); // Rollback in case of error
                            throw new Exception("Error saving record: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Database connection error: " + ex.Message);
            }
        }

    }
}
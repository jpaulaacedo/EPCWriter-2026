using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPCReaderWriter.Models;
using MySql.Data.MySqlClient;

namespace EPCReaderWriter.Repositories
{
    public class LocationRepository : RepositoryBase
    {
        public LocationRepository()
        {
        }

        public IList<LocationModel> Get()
        {
            var result = new List<LocationModel>();

            using (var connection = new MySqlConnection(ConnectionString))
            {
                connection.Open();

                var query = "SELECT `id`,"
                            + "`location`"
                            + "FROM `ip_locations`";

                MySqlCommand command = new MySqlCommand(query, connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new LocationModel()
                        {
                            LocationId = reader.GetInt32(0),
                            Location = reader.GetString(1)
                        });
                    }
                }
            }

            return result;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EPCReaderWriter.Models;

namespace EPCReaderWriter.Helpers
{
    public static class ConnectionStringBuilder
    {
        public static string GetConnectionString()
        {
            var server = Properties.Settings.Default.Server;
            var port = Properties.Settings.Default.Port;
            var userId = Properties.Settings.Default.UserId;
            var password = Properties.Settings.Default.Password;
            var database = Properties.Settings.Default.Database;
            var sslMode = Properties.Settings.Default.SslMode;

            var connectionStringBuilder = new StringBuilder();

            if (!string.IsNullOrEmpty(server))
                connectionStringBuilder.Append(string.Format("server={0}; ", server));

            if (!string.IsNullOrEmpty(port))
                connectionStringBuilder.Append(string.Format("port={0}; ", port));

            if (!string.IsNullOrEmpty(userId))
                connectionStringBuilder.Append(string.Format("userId={0}; ", userId));

            if (!string.IsNullOrEmpty(password))
                connectionStringBuilder.Append(string.Format("password={0}; ", password));

            if (!string.IsNullOrEmpty(database))
                connectionStringBuilder.Append(string.Format("database={0}; ", database));

            if (!string.IsNullOrEmpty(sslMode))
                connectionStringBuilder.Append(string.Format("sslMode={0};", sslMode));

            return connectionStringBuilder.ToString();
        }

        public static string GetConnectionString(DatabaseConnectionModel connection)
        {
            var connectionStringBuilder = new StringBuilder();

            if (!string.IsNullOrEmpty(connection.Server))
                connectionStringBuilder.Append(string.Format("server={0}; ", connection.Server));

            if (!string.IsNullOrEmpty(connection.Port))
                connectionStringBuilder.Append(string.Format("port={0}; ", connection.Port));

            if (!string.IsNullOrEmpty(connection.UserId))
                connectionStringBuilder.Append(string.Format("userId={0}; ", connection.UserId));

            if (!string.IsNullOrEmpty(connection.Password))
                connectionStringBuilder.Append(string.Format("password={0}; ", connection.Password));

            if (!string.IsNullOrEmpty(connection.Database))
                connectionStringBuilder.Append(string.Format("database={0}; ", connection.Database));

            if (!string.IsNullOrEmpty(connection.SslMode))
                connectionStringBuilder.Append(string.Format("sslMode={0};", connection.SslMode));

            return connectionStringBuilder.ToString();
        }
    }
}
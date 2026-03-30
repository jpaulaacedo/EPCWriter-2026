using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EPCReaderWriter.Helpers;
using MySql.Data.MySqlClient;
using EPCReaderWriter.Models;

namespace EPCReaderWriter
{
    public partial class DatabaseConnectionSetupForm : Form
    {
        public DatabaseConnectionSetupForm()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            var connectionDetails = new DatabaseConnectionModel
            {
                Server = txtServer.Text,
                Port = txtPort.Text,
                UserId = txtUser.Text,
                Password = txtPassword.Text,
                Database = txtDatabase.Text,
                SslMode = txtSSL.Text
            };

            var connectionString = ConnectionStringBuilder.GetConnectionString(connectionDetails);
            var connectionStringTestResult = DatabaseConnectionHelper.TestConnection(connectionString);

            if (connectionStringTestResult.Connected)
            {
                MessageBox.Show("Connected successfully!");
                btnSave.Enabled = true;
            }
            else
            {
                MessageBox.Show("Can't connect to database. Error: " + connectionStringTestResult.Errors[0].Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Server = txtServer.Text;
            Properties.Settings.Default.Port = txtPort.Text;
            Properties.Settings.Default.UserId = txtUser.Text;
            Properties.Settings.Default.Password = txtPassword.Text;
            Properties.Settings.Default.Database = txtDatabase.Text;
            Properties.Settings.Default.SslMode = txtSSL.Text;
            Properties.Settings.Default.Save();

            MessageBox.Show("Database connection updated.");
            (this.Owner as Form1).CheckDatabaseConnection();
            this.Close();
        }

        private void DatabaseConnectionSetupForm_Load(object sender, EventArgs e)
        {
            txtServer.Text = Properties.Settings.Default.Server;
            txtPort.Text = Properties.Settings.Default.Port;
            txtUser.Text = Properties.Settings.Default.UserId;
            txtPassword.Text = Properties.Settings.Default.Password;
            txtDatabase.Text = Properties.Settings.Default.Database;
            txtSSL.Text = Properties.Settings.Default.SslMode;

            btnSave.Enabled = false;
        }
    }
}

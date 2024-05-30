using Microsoft.Data.SqlClient;
using QuestionServices;
using SharedResources;
using Survey_Configurator.Custom_controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Survey_Configurator.Sub_forms
{
    public partial class ConnectionSettings : Form
    {
        //class members
        private LogIn mLogInControl;
        private ConnectionString mConnectionString = new ConnectionString();

        //constants
        //authentication type
        private const string cWindowsAuthentication = "Windows Authentication";
        private const string cSQLServerAuthentication = "SQL Server Authentication";
        //encrytion options

        public ConnectionSettings(bool isOpenedFromSettings)
        {
            try
            {
                InitializeComponent();
                if (!isOpenedFromSettings)
                {
                    //some default settings
                    AuthenticationComboBox.SelectedItem = cWindowsAuthentication;
                }
                else
                {
                    PopulateConnectionString();

                    //fill the form with the existing data from the connection string
                    ServerNameTestBox.Text = mConnectionString.Server;
                    DatabaseNameTextBox.Text = mConnectionString.Database;
                    AuthenticationComboBox.SelectedItem = mConnectionString.IntegratedSecurity ?cWindowsAuthentication :cSQLServerAuthentication;
                    if(AuthenticationComboBox.SelectedItem == cSQLServerAuthentication)
                    {
                        mLogInControl.LoginTextBox.Text = mConnectionString.User;
                        mLogInControl.PasswordTextBox.Text = mConnectionString.Password;
                    }
                }
            }
            catch(Exception ex)
            {
                UtilityMethods.LogError(ex);
                MainScreen.ShowDefaultErrorMessage();
            }
    }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            try 
            { 
                Close();
            }
            catch(Exception ex)
            {
                UtilityMethods.LogError(ex);
                MainScreen.ShowDefaultErrorMessage();
            }
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                mConnectionString.Server = ServerNameTestBox.Text;
                mConnectionString.Database = DatabaseNameTextBox.Text;
                mConnectionString.IntegratedSecurity = AuthenticationComboBox.SelectedItem.ToString() == cWindowsAuthentication;
                if (AuthenticationComboBox.SelectedItem.ToString() == cSQLServerAuthentication)
                {
                    mConnectionString.User = mLogInControl.LoginTextBox.Text;
                    mConnectionString.Password = mLogInControl.PasswordTextBox.Text;
                }
                mConnectionString.Encrypt = false;

                QuestionOperations.SetConnectionString(mConnectionString);

                //test connection
                bool tIsDatabaseConnected = MainScreen.CheckDatabaseConnection();
                if (tIsDatabaseConnected)
                {
                    //connected nice and return a ok dialoue result
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    //not connected as if user wants to proceed anyway, if yes return continue dialouge result
                    DialogResult tContinueAnyWay = MessageBox.Show("Database refused to connect, would you like to continue anyway?", "Database connection error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if(tContinueAnyWay == DialogResult.Yes)
                    {
                        DialogResult = DialogResult.Continue;
                        Close();
                    }
                }
            }
            catch(Exception ex)
            {
                UtilityMethods.LogError(ex);
                MainScreen.ShowDefaultErrorMessage();
            }

        }

        private void AuthenticationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (AuthenticationComboBox.SelectedItem.ToString() == cSQLServerAuthentication)
                {
                    mLogInControl = new LogIn();
                    mLogInControl.Location = new Point(42, 159);
                    //specify location 
                    ConnectionCredintials.Controls.Add(mLogInControl);
                }
                else
                {
                    ConnectionCredintials.Controls.Remove(mLogInControl);
                }
            }
            catch(Exception ex)
            {
                UtilityMethods.LogError(ex);
                MainScreen.ShowDefaultErrorMessage();
            }
    }

        private void ServerNameTestBox_TextChanged(object sender, EventArgs e)
        {
            try 
            { 
                ConnectButton.Enabled =  ServerNameTestBox.Text.Length > 0 && DatabaseNameTextBox.Text.Length > 0;
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                MainScreen.ShowDefaultErrorMessage();
            }
        }

        private void DatabaseNameTextBox_TextChanged(object sender, EventArgs e)
        {
            try 
            { 
                ConnectButton.Enabled = ServerNameTestBox.Text.Length > 0 && DatabaseNameTextBox.Text.Length > 0;
            }
            catch( Exception ex)
            {
                UtilityMethods.LogError(ex);
                MainScreen.ShowDefaultErrorMessage();
            }
        }

        private void PopulateConnectionString()
        {
            using (StreamReader tFileReader = new StreamReader(QuestionOperations.mFilePath))
            {
                string tReadConnectionString = tFileReader.ReadToEnd();
                //de-serialize the obtained connection string and transform it to the correct format
                ConnectionString tDesrializedConnString = JsonSerializer.Deserialize<ConnectionString>(tReadConnectionString);
                mConnectionString = tDesrializedConnString;
            }
        }
    }
}

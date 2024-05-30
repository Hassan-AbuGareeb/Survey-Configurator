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
        /// <summary>
        /// this class is responsible for the connection setting form 
        /// which obtains the connection string settings from the user
        /// and test the connection to the database and advances the 
        /// user to the application accordingly
        /// </summary>


        //class members
        private LogIn mLogInControl;
        private ConnectionString mConnectionString = new ConnectionString();

        //constants
        //authentication type
        private const string cWindowsAuthentication = "Windows Authentication";
        private const string cSQLServerAuthentication = "SQL Server Authentication";

        /// <summary>
        /// constructor of this form, in the case the form is opened
        /// for the first time where the connection string doesn't exist
        /// only one field have a default value,
        /// if the form is opened from the options menu inside the app
        /// the fields must be filled with data from the existing connection string
        /// </summary>
        /// <param name="isOpenedFromSettings">indicates whether the form was opened from the options menu inside the app</param>
        public ConnectionSettings(bool pIsOpenedFromSettings = false)
        {
            try
            {
                InitializeComponent();
                if (!pIsOpenedFromSettings)
                {
                    //some default settings
                    AuthenticationComboBox.SelectedItem = cWindowsAuthentication;
                }
                else
                {
                    //fill the connection string with data stored in the connectionstring.json file
                    PopulateConnectionString();

                    //fill the form with the existing data from the connection string
                    ServerNameTestBox.Text = mConnectionString.mServer;
                    DatabaseNameTextBox.Text = mConnectionString.mDatabase;
                    AuthenticationComboBox.SelectedItem = mConnectionString.mIntegratedSecurity ?cWindowsAuthentication :cSQLServerAuthentication;
                    if(AuthenticationComboBox.SelectedItem == cSQLServerAuthentication)
                    {
                        mLogInControl.LoginTextBox.Text = mConnectionString.mUser;
                        mLogInControl.PasswordTextBox.Text = mConnectionString.mPassword;
                    }
                }
            }
            catch(Exception ex)
            {
                UtilityMethods.LogError(ex);
                MainScreen.ShowDefaultErrorMessage();
            }
    }

        /// <summary>
        /// closes the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// fill the connection string obj with data from the fields and attempt to 
        /// connect to the database, in the case of failure ask the user if the user
        /// wants to continue regardless, the flow of events is based on the dialogResult
        /// of this form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                mConnectionString.mServer = ServerNameTestBox.Text;
                mConnectionString.mDatabase = DatabaseNameTextBox.Text;
                mConnectionString.mIntegratedSecurity = AuthenticationComboBox.SelectedItem.ToString() == cWindowsAuthentication;
                if (AuthenticationComboBox.SelectedItem.ToString() == cSQLServerAuthentication)
                {
                    mConnectionString.mUser = mLogInControl.LoginTextBox.Text;
                    mConnectionString.mPassword = mLogInControl.PasswordTextBox.Text;
                }
                mConnectionString.mEncrypt = false;

                QuestionOperations.SetConnectionString(mConnectionString);

                //test connection
                bool tIsDatabaseConnected = MainScreen.CheckDatabaseConnection();
                if (tIsDatabaseConnected)
                {
                    //connected successfully
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    //connection failed
                    DialogResult tContinueAnyWay = MessageBox.Show("Database refused to connect, would you like to continue anyway?", "Database connection error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if(tContinueAnyWay == DialogResult.Yes)
                    {   //user wants to continue anyway
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

        /// <summary>
        /// show options for the user to use username and password
        /// to connect to the database, or use the windows authentication
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// prevent the user from connecting if the server name and the database name aren't provided
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// the same functionality as the above function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// fill the connection string with data stored in the connectionstring.json file
        /// </summary>
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

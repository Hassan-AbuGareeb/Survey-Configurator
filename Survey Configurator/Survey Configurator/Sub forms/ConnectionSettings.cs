﻿using Microsoft.Data.SqlClient;
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Survey_Configurator.Sub_forms
{
    public partial class ConnectionSettings : Form
    {
        //class members
        LogIn mLogInControl;
        //SqlConnectionStringBuilder mConnectionString = new SqlConnectionStringBuilder();
        ConnectionString mConnectionString = new ConnectionString();

        //constants
        //authentication type
        private const string cWindowsAuthentication = "Windows Authentication";
        private const string cSQLServerAuthentication = "SQL Server Authentication";
        //encrytion options

        public ConnectionSettings()
        {
            try
            {
                InitializeComponent();

                //some default settings
                AuthenticationComboBox.SelectedItem = cWindowsAuthentication;
                EncryptionComboBox.SelectedItem = SqlConnectionEncryptOption.Strict.ToString();
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
                //construct connection string
                //mConnectionString.Clear();

                //mConnectionString.DataSource = ServerNameTestBox.Text;
                //mConnectionString.InitialCatalog = DatabaseNameTextBox.Text;
                //mConnectionString.Authentication = SqlAuthenticationMethod.
                //if(mLogInControl != null) 
                //{ 
                //    mConnectionString.UserID = mLogInControl.LoginTextBox.Text;
                //    mConnectionString.Password = mLogInControl.PasswordTextBox.Text;
                //}

                mConnectionString.Server = ServerNameTestBox.Text;
                mConnectionString.Database = DatabaseNameTextBox.Text;
                mConnectionString.Trusted_Connection = AuthenticationComboBox.SelectedItem.ToString() == cWindowsAuthentication;
                if (AuthenticationComboBox.SelectedItem.ToString() == cSQLServerAuthentication)
                {
                    mConnectionString.User = mLogInControl.LoginTextBox.Text;
                    mConnectionString.Password = mLogInControl.PasswordTextBox.Text;
                }
                mConnectionString.Encrypt = SqlConnectionEncryptOption.Optional;
                mConnectionString.TrustServerCertificate = TrustServerCertificateCheckBox.Checked;
                if (HostNameInCertificateTextbox.Enabled)
                {
                    mConnectionString.HostNameInCertificate = HostNameInCertificateTextbox.Text;
                }

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
                    ServerGroupBox.Controls.Add(mLogInControl);
                }
                else
                {
                    ServerGroupBox.Controls.Remove(mLogInControl);
                }
            }
            catch(Exception ex)
            {
                UtilityMethods.LogError(ex);
                MainScreen.ShowDefaultErrorMessage();
            }
    }

        private void EncryptionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try 
            { 
                if (EncryptionComboBox.SelectedItem.ToString() == SqlConnectionEncryptOption.Strict.ToString())
                {
                    TrustServerCertificateCheckBox.Checked = false;
                    TrustServerCertificateCheckBox.Enabled = false;
                    HostNameInCertificateTextbox.Enabled = true;
                }
                else
                {
                    TrustServerCertificateCheckBox.Enabled = true;
                }
            }
            catch(Exception ex)
            {
                UtilityMethods.LogError(ex);
                MainScreen.ShowDefaultErrorMessage();
            }
        }

        private void TrustServerCertificateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try 
            { 
                HostNameInCertificateTextbox.Enabled = !TrustServerCertificateCheckBox.Checked;
            }
            catch (Exception ex)
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
    }
}

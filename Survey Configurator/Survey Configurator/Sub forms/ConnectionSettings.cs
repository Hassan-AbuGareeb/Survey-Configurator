using Microsoft.Data.SqlClient;
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

        LogIn mLogInControl;

        //constants
        //authentication type
        private const string cWindowsAuthentication = "Windows Authentication";
        private const string cSQLServerAuthentication = "SQL Server Authentication";
        //encrytion options

        public ConnectionSettings()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {

        }

        private void AuthenticationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AuthenticationComboBox.SelectedItem.ToString() == cWindowsAuthentication)
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

        private void EncryptionComboBox_SelectedIndexChanged(object sender, EventArgs e)
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

        private void TrustServerCertificateCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            HostNameInCertificateTextbox.Enabled = !TrustServerCertificateCheckBox.Checked;
        }
    }
}

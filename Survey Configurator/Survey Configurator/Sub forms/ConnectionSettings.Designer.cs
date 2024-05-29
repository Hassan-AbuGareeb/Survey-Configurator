namespace Survey_Configurator.Sub_forms
{
    partial class ConnectionSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ConnectionSecurityGroupBox = new GroupBox();
            HostNameInCertificateTextbox = new TextBox();
            HostNameInCertificateLabel = new Label();
            TrustServerCertificateCheckBox = new CheckBox();
            EncryptionComboBox = new ComboBox();
            EncryptionLabel = new Label();
            ServerGroupBox = new GroupBox();
            AuthenticationComboBox = new ComboBox();
            AuthenticationLabel = new Label();
            DatabaseNameTextBox = new TextBox();
            DatabaseNameLabel = new Label();
            ServerNameTestBox = new TextBox();
            ServerNameLabel = new Label();
            CancelButton = new Button();
            ConnectButton = new Button();
            ConnectionSecurityGroupBox.SuspendLayout();
            ServerGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // ConnectionSecurityGroupBox
            // 
            ConnectionSecurityGroupBox.Controls.Add(HostNameInCertificateTextbox);
            ConnectionSecurityGroupBox.Controls.Add(HostNameInCertificateLabel);
            ConnectionSecurityGroupBox.Controls.Add(TrustServerCertificateCheckBox);
            ConnectionSecurityGroupBox.Controls.Add(EncryptionComboBox);
            ConnectionSecurityGroupBox.Controls.Add(EncryptionLabel);
            ConnectionSecurityGroupBox.Location = new Point(12, 268);
            ConnectionSecurityGroupBox.Name = "ConnectionSecurityGroupBox";
            ConnectionSecurityGroupBox.Size = new Size(600, 190);
            ConnectionSecurityGroupBox.TabIndex = 1;
            ConnectionSecurityGroupBox.TabStop = false;
            ConnectionSecurityGroupBox.Text = "Connection Security";
            // 
            // HostNameInCertificateTextbox
            // 
            HostNameInCertificateTextbox.Location = new Point(194, 116);
            HostNameInCertificateTextbox.Name = "HostNameInCertificateTextbox";
            HostNameInCertificateTextbox.Size = new Size(400, 27);
            HostNameInCertificateTextbox.TabIndex = 7;
            // 
            // HostNameInCertificateLabel
            // 
            HostNameInCertificateLabel.AutoSize = true;
            HostNameInCertificateLabel.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            HostNameInCertificateLabel.Location = new Point(17, 120);
            HostNameInCertificateLabel.Name = "HostNameInCertificateLabel";
            HostNameInCertificateLabel.Size = new Size(171, 18);
            HostNameInCertificateLabel.TabIndex = 6;
            HostNameInCertificateLabel.Text = "Host name in certificate :";
            // 
            // TrustServerCertificateCheckBox
            // 
            TrustServerCertificateCheckBox.AutoSize = true;
            TrustServerCertificateCheckBox.Location = new Point(194, 70);
            TrustServerCertificateCheckBox.Name = "TrustServerCertificateCheckBox";
            TrustServerCertificateCheckBox.Size = new Size(175, 24);
            TrustServerCertificateCheckBox.TabIndex = 8;
            TrustServerCertificateCheckBox.Text = "Trust server certificate";
            TrustServerCertificateCheckBox.UseVisualStyleBackColor = true;
            TrustServerCertificateCheckBox.CheckedChanged += TrustServerCertificateCheckBox_CheckedChanged;
            // 
            // EncryptionComboBox
            // 
            EncryptionComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            EncryptionComboBox.FormattingEnabled = true;
            EncryptionComboBox.Items.AddRange(new object[] { "Optional", "Mandatory", "Strict" });
            EncryptionComboBox.Location = new Point(194, 36);
            EncryptionComboBox.Name = "EncryptionComboBox";
            EncryptionComboBox.Size = new Size(400, 28);
            EncryptionComboBox.TabIndex = 7;
            EncryptionComboBox.SelectedIndexChanged += EncryptionComboBox_SelectedIndexChanged;
            // 
            // EncryptionLabel
            // 
            EncryptionLabel.AutoSize = true;
            EncryptionLabel.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            EncryptionLabel.Location = new Point(17, 40);
            EncryptionLabel.Name = "EncryptionLabel";
            EncryptionLabel.Size = new Size(82, 18);
            EncryptionLabel.TabIndex = 6;
            EncryptionLabel.Text = "Encryption:";
            // 
            // ServerGroupBox
            // 
            ServerGroupBox.Controls.Add(AuthenticationComboBox);
            ServerGroupBox.Controls.Add(AuthenticationLabel);
            ServerGroupBox.Controls.Add(DatabaseNameTextBox);
            ServerGroupBox.Controls.Add(DatabaseNameLabel);
            ServerGroupBox.Controls.Add(ServerNameTestBox);
            ServerGroupBox.Controls.Add(ServerNameLabel);
            ServerGroupBox.Location = new Point(12, 12);
            ServerGroupBox.Name = "ServerGroupBox";
            ServerGroupBox.Size = new Size(600, 250);
            ServerGroupBox.TabIndex = 0;
            ServerGroupBox.TabStop = false;
            ServerGroupBox.Text = "Server";
            // 
            // AuthenticationComboBox
            // 
            AuthenticationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            AuthenticationComboBox.FormattingEnabled = true;
            AuthenticationComboBox.Items.AddRange(new object[] { "Windows Authentication", "SQL Server Authentication" });
            AuthenticationComboBox.Location = new Point(194, 113);
            AuthenticationComboBox.Name = "AuthenticationComboBox";
            AuthenticationComboBox.Size = new Size(400, 28);
            AuthenticationComboBox.TabIndex = 5;
            AuthenticationComboBox.SelectedIndexChanged += AuthenticationComboBox_SelectedIndexChanged;
            // 
            // AuthenticationLabel
            // 
            AuthenticationLabel.AutoSize = true;
            AuthenticationLabel.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            AuthenticationLabel.Location = new Point(17, 117);
            AuthenticationLabel.Name = "AuthenticationLabel";
            AuthenticationLabel.Size = new Size(104, 18);
            AuthenticationLabel.TabIndex = 4;
            AuthenticationLabel.Text = "Authentication:";
            // 
            // DatabaseNameTextBox
            // 
            DatabaseNameTextBox.Location = new Point(194, 71);
            DatabaseNameTextBox.Name = "DatabaseNameTextBox";
            DatabaseNameTextBox.Size = new Size(400, 27);
            DatabaseNameTextBox.TabIndex = 3;
            DatabaseNameTextBox.TextChanged += DatabaseNameTextBox_TextChanged;
            // 
            // DatabaseNameLabel
            // 
            DatabaseNameLabel.AutoSize = true;
            DatabaseNameLabel.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            DatabaseNameLabel.Location = new Point(17, 75);
            DatabaseNameLabel.Name = "DatabaseNameLabel";
            DatabaseNameLabel.Size = new Size(116, 18);
            DatabaseNameLabel.TabIndex = 2;
            DatabaseNameLabel.Text = "Database name:";
            // 
            // ServerNameTestBox
            // 
            ServerNameTestBox.Location = new Point(194, 29);
            ServerNameTestBox.Name = "ServerNameTestBox";
            ServerNameTestBox.Size = new Size(400, 27);
            ServerNameTestBox.TabIndex = 1;
            ServerNameTestBox.TextChanged += ServerNameTestBox_TextChanged;
            // 
            // ServerNameLabel
            // 
            ServerNameLabel.AutoSize = true;
            ServerNameLabel.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ServerNameLabel.Location = new Point(17, 33);
            ServerNameLabel.Name = "ServerNameLabel";
            ServerNameLabel.Size = new Size(96, 18);
            ServerNameLabel.TabIndex = 0;
            ServerNameLabel.Text = "Server name:";
            // 
            // CancelButton
            // 
            CancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            CancelButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            CancelButton.Font = new Font("Microsoft Sans Serif", 9F);
            CancelButton.ImeMode = ImeMode.NoControl;
            CancelButton.Location = new Point(509, 468);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(100, 30);
            CancelButton.TabIndex = 3;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += CancelButton_Click;
            // 
            // ConnectButton
            // 
            ConnectButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ConnectButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ConnectButton.Enabled = false;
            ConnectButton.Font = new Font("Microsoft Sans Serif", 9F);
            ConnectButton.ImeMode = ImeMode.NoControl;
            ConnectButton.Location = new Point(403, 468);
            ConnectButton.Name = "ConnectButton";
            ConnectButton.Size = new Size(100, 30);
            ConnectButton.TabIndex = 4;
            ConnectButton.Text = "Connect";
            ConnectButton.UseVisualStyleBackColor = true;
            ConnectButton.Click += ConnectButton_Click;
            // 
            // ConnectionSettings
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(625, 510);
            Controls.Add(ConnectionSecurityGroupBox);
            Controls.Add(ConnectButton);
            Controls.Add(ServerGroupBox);
            Controls.Add(CancelButton);
            Name = "ConnectionSettings";
            Text = "Connection settings";
            ConnectionSecurityGroupBox.ResumeLayout(false);
            ConnectionSecurityGroupBox.PerformLayout();
            ServerGroupBox.ResumeLayout(false);
            ServerGroupBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button CancelButton;
        private Button ConnectButton;
        private GroupBox ServerGroupBox;
        private GroupBox ConnectionSecurityGroupBox;
        private Label ServerNameLabel;
        private TextBox ServerNameTestBox;
        private TextBox DatabaseNameTextBox;
        private Label DatabaseNameLabel;
        private Label AuthenticationLabel;
        private ComboBox AuthenticationComboBox;
        private ComboBox EncryptionComboBox;
        private Label EncryptionLabel;
        private TextBox HostNameInCertificateTextbox;
        private Label HostNameInCertificateLabel;
        private CheckBox TrustServerCertificateCheckBox;
    }
}
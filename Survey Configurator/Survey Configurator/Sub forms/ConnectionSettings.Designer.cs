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
            ConnectionCredintials = new GroupBox();
            AuthenticationComboBox = new ComboBox();
            AuthenticationLabel = new Label();
            DatabaseNameTextBox = new TextBox();
            DatabaseNameLabel = new Label();
            ServerNameTestBox = new TextBox();
            ServerNameLabel = new Label();
            CancelButton = new Button();
            ConnectButton = new Button();
            ConnectionCredintials.SuspendLayout();
            SuspendLayout();
            // 
            // ConnectionCredintials
            // 
            ConnectionCredintials.Controls.Add(AuthenticationComboBox);
            ConnectionCredintials.Controls.Add(AuthenticationLabel);
            ConnectionCredintials.Controls.Add(DatabaseNameTextBox);
            ConnectionCredintials.Controls.Add(DatabaseNameLabel);
            ConnectionCredintials.Controls.Add(ServerNameTestBox);
            ConnectionCredintials.Controls.Add(ServerNameLabel);
            ConnectionCredintials.Location = new Point(12, 12);
            ConnectionCredintials.Name = "ConnectionCredintials";
            ConnectionCredintials.Size = new Size(600, 250);
            ConnectionCredintials.TabIndex = 0;
            ConnectionCredintials.TabStop = false;
            ConnectionCredintials.Text = "Connection credintials";
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
            CancelButton.Location = new Point(509, 274);
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
            ConnectButton.Location = new Point(403, 274);
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
            ClientSize = new Size(625, 316);
            Controls.Add(ConnectButton);
            Controls.Add(ConnectionCredintials);
            Controls.Add(CancelButton);
            MaximumSize = new Size(643, 363);
            MinimumSize = new Size(643, 363);
            Name = "ConnectionSettings";
            Text = "Connection settings";
            ConnectionCredintials.ResumeLayout(false);
            ConnectionCredintials.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button CancelButton;
        private Button ConnectButton;
        private GroupBox ConnectionCredintials;
        private Label ServerNameLabel;
        private TextBox ServerNameTestBox;
        private TextBox DatabaseNameTextBox;
        private Label DatabaseNameLabel;
        private Label AuthenticationLabel;
        private ComboBox AuthenticationComboBox;
    }
}
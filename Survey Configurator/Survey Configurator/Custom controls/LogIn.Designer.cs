namespace Survey_Configurator.Custom_controls
{
    partial class LogIn
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            LoginTextBox = new TextBox();
            LoginLabel = new Label();
            PasswordTextBox = new TextBox();
            PasswordLabel = new Label();
            SuspendLayout();
            // 
            // LoginTextBox
            // 
            LoginTextBox.Location = new Point(151, 0);
            LoginTextBox.Name = "LoginTextBox";
            LoginTextBox.Size = new Size(400, 27);
            LoginTextBox.TabIndex = 5;
            // 
            // LoginLabel
            // 
            LoginLabel.AutoSize = true;
            LoginLabel.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LoginLabel.Location = new Point(0, 4);
            LoginLabel.Name = "LoginLabel";
            LoginLabel.Size = new Size(48, 18);
            LoginLabel.TabIndex = 4;
            LoginLabel.Text = "Login:";
            // 
            // PasswordTextBox
            // 
            PasswordTextBox.Location = new Point(151, 42);
            PasswordTextBox.Name = "PasswordTextBox";
            PasswordTextBox.Size = new Size(400, 27);
            PasswordTextBox.TabIndex = 7;
            PasswordTextBox.UseSystemPasswordChar = true;
            // 
            // PasswordLabel
            // 
            PasswordLabel.AutoSize = true;
            PasswordLabel.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PasswordLabel.Location = new Point(0, 46);
            PasswordLabel.Name = "PasswordLabel";
            PasswordLabel.Size = new Size(79, 18);
            PasswordLabel.TabIndex = 6;
            PasswordLabel.Text = "Password:";
            // 
            // LogIn
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(PasswordTextBox);
            Controls.Add(PasswordLabel);
            Controls.Add(LoginTextBox);
            Controls.Add(LoginLabel);
            Name = "LogIn";
            Size = new Size(554, 74);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public TextBox LoginTextBox;
        private Label LoginLabel;
        public TextBox PasswordTextBox;
        private Label PasswordLabel;
    }
}

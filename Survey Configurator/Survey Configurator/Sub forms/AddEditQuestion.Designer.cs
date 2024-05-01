namespace Survey_Configurator.Sub_forms
{
    partial class AddEditQuestion
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
            QuestionOptions = new Panel();
            Cancel = new Button();
            Add = new Button();
            QuestionTypeComboBox = new ComboBox();
            QuestionOrderNumeric = new NumericUpDown();
            QuestionTextBox = new TextBox();
            TitleLabel = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)QuestionOrderNumeric).BeginInit();
            SuspendLayout();
            // 
            // QuestionOptions
            // 
            QuestionOptions.Location = new Point(27, 357);
            QuestionOptions.Name = "QuestionOptions";
            QuestionOptions.Size = new Size(511, 240);
            QuestionOptions.TabIndex = 19;
            // 
            // Cancel
            // 
            Cancel.Font = new Font("Segoe UI", 14.25F);
            Cancel.Location = new Point(438, 603);
            Cancel.Name = "Cancel";
            Cancel.Size = new Size(100, 44);
            Cancel.TabIndex = 18;
            Cancel.Text = "Cancel";
            Cancel.UseVisualStyleBackColor = true;
            // 
            // Add
            // 
            Add.Font = new Font("Segoe UI", 14.25F);
            Add.Location = new Point(120, 603);
            Add.Name = "Add";
            Add.Size = new Size(100, 44);
            Add.TabIndex = 17;
            Add.Text = "Add";
            Add.UseVisualStyleBackColor = true;
            // 
            // QuestionTypeComboBox
            // 
            QuestionTypeComboBox.FormattingEnabled = true;
            QuestionTypeComboBox.Items.AddRange(new object[] { "Slider", "Stars", "Smiley" });
            QuestionTypeComboBox.Location = new Point(119, 300);
            QuestionTypeComboBox.Name = "QuestionTypeComboBox";
            QuestionTypeComboBox.Size = new Size(121, 23);
            QuestionTypeComboBox.TabIndex = 16;
            // 
            // QuestionOrderNumeric
            // 
            QuestionOrderNumeric.Location = new Point(120, 221);
            QuestionOrderNumeric.Maximum = new decimal(new int[] { 120, 0, 0, 0 });
            QuestionOrderNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            QuestionOrderNumeric.Name = "QuestionOrderNumeric";
            QuestionOrderNumeric.Size = new Size(120, 23);
            QuestionOrderNumeric.TabIndex = 15;
            QuestionOrderNumeric.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // QuestionTextBox
            // 
            QuestionTextBox.Location = new Point(120, 89);
            QuestionTextBox.Multiline = true;
            QuestionTextBox.Name = "QuestionTextBox";
            QuestionTextBox.Size = new Size(418, 89);
            QuestionTextBox.TabIndex = 14;
            // 
            // TitleLabel
            // 
            TitleLabel.AutoSize = true;
            TitleLabel.Font = new Font("Segoe UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            TitleLabel.Location = new Point(12, 9);
            TitleLabel.Name = "label4";
            TitleLabel.Size = new Size(253, 40);
            TitleLabel.TabIndex = 13;
            TitleLabel.Text = "Add/Edit Question";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F);
            label3.Location = new Point(27, 295);
            label3.Name = "label3";
            label3.Size = new Size(51, 25);
            label3.TabIndex = 12;
            label3.Text = "Type";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F);
            label2.Location = new Point(27, 215);
            label2.Name = "label2";
            label2.Size = new Size(61, 25);
            label2.TabIndex = 11;
            label2.Text = "Order";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F);
            label1.Location = new Point(27, 84);
            label1.Name = "label1";
            label1.Size = new Size(45, 25);
            label1.TabIndex = 10;
            label1.Text = "Text";
            // 
            // AddEditQuestion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(642, 690);
            Controls.Add(QuestionOptions);
            Controls.Add(Cancel);
            Controls.Add(Add);
            Controls.Add(QuestionTypeComboBox);
            Controls.Add(QuestionOrderNumeric);
            Controls.Add(QuestionTextBox);
            Controls.Add(TitleLabel);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "AddEditQuestion";
            Text = "Add Edit";
            Load += AddEdit_Load;
            ((System.ComponentModel.ISupportInitialize)QuestionOrderNumeric).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel QuestionOptions;
        private Button Cancel;
        private Button Add;
        private ComboBox QuestionTypeComboBox;
        private NumericUpDown QuestionOrderNumeric;
        private TextBox QuestionTextBox;
        private Label TitleLabel;
        private Label label3;
        private Label label2;
        private Label label1;
    }
}
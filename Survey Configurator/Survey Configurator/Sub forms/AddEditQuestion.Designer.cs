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
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)QuestionOrderNumeric).BeginInit();
            SuspendLayout();
            // 
            // QuestionOptions
            // 
            QuestionOptions.Anchor = AnchorStyles.Left;
            QuestionOptions.Location = new Point(31, 446);
            QuestionOptions.Margin = new Padding(3, 4, 3, 4);
            QuestionOptions.Name = "QuestionOptions";
            QuestionOptions.Size = new Size(633, 204);
            QuestionOptions.TabIndex = 19;
            // 
            // Cancel
            // 
            Cancel.Anchor = AnchorStyles.Left;
            Cancel.Font = new Font("Segoe UI", 12F);
            Cancel.Location = new Point(550, 700);
            Cancel.Margin = new Padding(3, 4, 3, 4);
            Cancel.Name = "Cancel";
            Cancel.Size = new Size(120, 40);
            Cancel.TabIndex = 9;
            Cancel.Text = "Cancel";
            Cancel.UseVisualStyleBackColor = true;
            Cancel.Click += CancelButton_Click;
            // 
            // Add
            // 
            Add.Anchor = AnchorStyles.Left;
            Add.Font = new Font("Segoe UI", 12F);
            Add.Location = new Point(424, 700);
            Add.Margin = new Padding(3, 4, 3, 4);
            Add.Name = "Add";
            Add.Size = new Size(120, 40);
            Add.TabIndex = 8;
            Add.Text = "Add";
            Add.UseVisualStyleBackColor = true;
            Add.Click += AddButton_Click;
            // 
            // QuestionTypeComboBox
            // 
            QuestionTypeComboBox.Anchor = AnchorStyles.Left;
            QuestionTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            QuestionTypeComboBox.FormattingEnabled = true;
            QuestionTypeComboBox.Items.AddRange(new object[] { "Slider", "Stars", "Smiley" });
            QuestionTypeComboBox.Location = new Point(29, 343);
            QuestionTypeComboBox.Margin = new Padding(3, 4, 3, 4);
            QuestionTypeComboBox.Name = "QuestionTypeComboBox";
            QuestionTypeComboBox.Size = new Size(187, 28);
            QuestionTypeComboBox.TabIndex = 3;
            QuestionTypeComboBox.SelectedIndexChanged += QuestionTypeComboBox_SelectedIndexChanged;
            // 
            // QuestionOrderNumeric
            // 
            QuestionOrderNumeric.Anchor = AnchorStyles.Left;
            QuestionOrderNumeric.Location = new Point(30, 259);
            QuestionOrderNumeric.Margin = new Padding(3, 4, 3, 4);
            QuestionOrderNumeric.Maximum = new decimal(new int[] { 120, 0, 0, 0 });
            QuestionOrderNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            QuestionOrderNumeric.Name = "QuestionOrderNumeric";
            QuestionOrderNumeric.Size = new Size(186, 27);
            QuestionOrderNumeric.TabIndex = 2;
            QuestionOrderNumeric.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // QuestionTextBox
            // 
            QuestionTextBox.Anchor = AnchorStyles.Left;
            QuestionTextBox.Location = new Point(30, 62);
            QuestionTextBox.Margin = new Padding(3, 4, 3, 4);
            QuestionTextBox.MaxLength = 350;
            QuestionTextBox.Multiline = true;
            QuestionTextBox.Name = "QuestionTextBox";
            QuestionTextBox.Size = new Size(559, 140);
            QuestionTextBox.TabIndex = 1;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 14.25F);
            label3.Location = new Point(30, 307);
            label3.Name = "label3";
            label3.Size = new Size(65, 32);
            label3.TabIndex = 12;
            label3.Text = "Type";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F);
            label2.Location = new Point(30, 223);
            label2.Name = "label2";
            label2.Size = new Size(75, 32);
            label2.TabIndex = 11;
            label2.Text = "Order";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F);
            label1.Location = new Point(30, 26);
            label1.Name = "label1";
            label1.Size = new Size(57, 32);
            label1.TabIndex = 10;
            label1.Text = "Text";
            // 
            // AddEditQuestion
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(702, 753);
            Controls.Add(QuestionOptions);
            Controls.Add(Cancel);
            Controls.Add(Add);
            Controls.Add(QuestionTypeComboBox);
            Controls.Add(QuestionOrderNumeric);
            Controls.Add(QuestionTextBox);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximumSize = new Size(720, 800);
            MinimumSize = new Size(720, 800);
            Name = "AddEditQuestion";
            Text = "Add Edit";
            FormClosing += AddEditQuestion_FormClosing;
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
        private Label label3;
        private Label label2;
        private Label label1;
    }
}
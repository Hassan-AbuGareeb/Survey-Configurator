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
            CancelButton = new Button();
            OperationButton = new Button();
            QuestionTypeComboBox = new ComboBox();
            QuestionOrderNumeric = new NumericUpDown();
            QuestionTextBox = new TextBox();
            QuestionOrderType = new Label();
            QuestionOrderLabel = new Label();
            QuestionTextLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)QuestionOrderNumeric).BeginInit();
            SuspendLayout();
            // 
            // CancelButton
            // 
            CancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            CancelButton.Font = new Font("Microsoft Sans Serif", 9F);
            CancelButton.Location = new Point(580, 354);
            CancelButton.Margin = new Padding(3, 4, 3, 4);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(100, 36);
            CancelButton.TabIndex = 9;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += CancelButton_Click;
            // 
            // OperationButton
            // 
            OperationButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            OperationButton.Font = new Font("Microsoft Sans Serif", 9F);
            OperationButton.Location = new Point(474, 354);
            OperationButton.Margin = new Padding(3, 4, 3, 4);
            OperationButton.Name = "OperationButton";
            OperationButton.Size = new Size(100, 36);
            OperationButton.TabIndex = 8;
            OperationButton.Text = "Add";
            OperationButton.UseVisualStyleBackColor = true;
            // 
            // QuestionTypeComboBox
            // 
            QuestionTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            QuestionTypeComboBox.FormattingEnabled = true;
            QuestionTypeComboBox.Items.AddRange(new object[] { SharedResources.QuestionType.Stars, SharedResources.QuestionType.Smiley, SharedResources.QuestionType.Slider });
            QuestionTypeComboBox.Location = new Point(188, 129);
            QuestionTypeComboBox.Margin = new Padding(3, 4, 3, 4);
            QuestionTypeComboBox.Name = "QuestionTypeComboBox";
            QuestionTypeComboBox.Size = new Size(492, 28);
            QuestionTypeComboBox.TabIndex = 3;
            QuestionTypeComboBox.SelectedIndexChanged += QuestionTypeComboBox_SelectedIndexChanged;
            // 
            // QuestionOrderNumeric
            // 
            QuestionOrderNumeric.Location = new Point(188, 83);
            QuestionOrderNumeric.Margin = new Padding(3, 4, 3, 4);
            QuestionOrderNumeric.Maximum = new decimal(new int[] { 120, 0, 0, 0 });
            QuestionOrderNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            QuestionOrderNumeric.Name = "QuestionOrderNumeric";
            QuestionOrderNumeric.Size = new Size(492, 27);
            QuestionOrderNumeric.TabIndex = 2;
            QuestionOrderNumeric.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // QuestionTextBox
            // 
            QuestionTextBox.Location = new Point(188, 13);
            QuestionTextBox.Margin = new Padding(3, 4, 3, 4);
            QuestionTextBox.MaxLength = 350;
            QuestionTextBox.Multiline = true;
            QuestionTextBox.Name = "QuestionTextBox";
            QuestionTextBox.Size = new Size(492, 51);
            QuestionTextBox.TabIndex = 1;
            // 
            // QuestionOrderType
            // 
            QuestionOrderType.AutoSize = true;
            QuestionOrderType.Font = new Font("Segoe UI", 9F);
            QuestionOrderType.Location = new Point(12, 132);
            QuestionOrderType.Name = "QuestionOrderType";
            QuestionOrderType.Size = new Size(43, 20);
            QuestionOrderType.TabIndex = 12;
            QuestionOrderType.Text = "Type:";
            // 
            // QuestionOrderLabel
            // 
            QuestionOrderLabel.AutoSize = true;
            QuestionOrderLabel.Font = new Font("Segoe UI", 9F);
            QuestionOrderLabel.Location = new Point(12, 85);
            QuestionOrderLabel.Name = "QuestionOrderLabel";
            QuestionOrderLabel.Size = new Size(50, 20);
            QuestionOrderLabel.TabIndex = 11;
            QuestionOrderLabel.Text = "Order:";
            // 
            // QuestionTextLabel
            // 
            QuestionTextLabel.AutoSize = true;
            QuestionTextLabel.Font = new Font("Segoe UI", 9F);
            QuestionTextLabel.Location = new Point(12, 16);
            QuestionTextLabel.Name = "QuestionTextLabel";
            QuestionTextLabel.Size = new Size(39, 20);
            QuestionTextLabel.TabIndex = 10;
            QuestionTextLabel.Text = "Text:";
            // 
            // AddEditQuestion
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(692, 403);
            Controls.Add(CancelButton);
            Controls.Add(OperationButton);
            Controls.Add(QuestionTypeComboBox);
            Controls.Add(QuestionOrderNumeric);
            Controls.Add(QuestionTextBox);
            Controls.Add(QuestionOrderType);
            Controls.Add(QuestionOrderLabel);
            Controls.Add(QuestionTextLabel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            Name = "AddEditQuestion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Add Edit";
            FormClosing += AddEditQuestion_FormClosing;
            Load += AddEdit_Load;
            ((System.ComponentModel.ISupportInitialize)QuestionOrderNumeric).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button CancelButton;
        private Button OperationButton;
        private ComboBox QuestionTypeComboBox;
        private NumericUpDown QuestionOrderNumeric;
        private TextBox QuestionTextBox;
        private Label QuestionOrderType;
        private Label QuestionOrderLabel;
        private Label QuestionTextLabel;
    }
}
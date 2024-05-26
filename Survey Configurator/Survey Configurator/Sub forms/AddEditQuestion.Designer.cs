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
            QuestionTypeComboBox = new ComboBox();
            QuestionOrderNumeric = new NumericUpDown();
            QuestionTextBox = new TextBox();
            QuestionOrderType = new Label();
            QuestionOrderLabel = new Label();
            QuestionTextLabel = new Label();
            OperationButton = new Button();
            CancelButton = new Button();
            ((System.ComponentModel.ISupportInitialize)QuestionOrderNumeric).BeginInit();
            SuspendLayout();
            // 
            // QuestionTypeComboBox
            // 
            QuestionTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            QuestionTypeComboBox.FormattingEnabled = true;
            QuestionTypeComboBox.Items.AddRange(new object[] { SharedResources.QuestionType.Stars, SharedResources.QuestionType.Smiley, SharedResources.QuestionType.Slider });
            QuestionTypeComboBox.Location = new Point(191, 129);
            QuestionTypeComboBox.Margin = new Padding(3, 4, 3, 4);
            QuestionTypeComboBox.Name = "QuestionTypeComboBox";
            QuestionTypeComboBox.Size = new Size(386, 28);
            QuestionTypeComboBox.TabIndex = 3;
            QuestionTypeComboBox.SelectedIndexChanged += QuestionTypeComboBox_SelectedIndexChanged;
            // 
            // QuestionOrderNumeric
            // 
            QuestionOrderNumeric.Location = new Point(191, 83);
            QuestionOrderNumeric.Margin = new Padding(3, 4, 3, 4);
            QuestionOrderNumeric.Maximum = new decimal(new int[] { 120, 0, 0, 0 });
            QuestionOrderNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            QuestionOrderNumeric.Name = "QuestionOrderNumeric";
            QuestionOrderNumeric.Size = new Size(386, 27);
            QuestionOrderNumeric.TabIndex = 2;
            QuestionOrderNumeric.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // QuestionTextBox
            // 
            QuestionTextBox.Location = new Point(191, 13);
            QuestionTextBox.Margin = new Padding(3, 4, 3, 4);
            QuestionTextBox.MaxLength = 350;
            QuestionTextBox.Multiline = true;
            QuestionTextBox.Name = "QuestionTextBox";
            QuestionTextBox.Size = new Size(386, 51);
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
            // OperationButton
            // 
            OperationButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            OperationButton.Font = new Font("Microsoft Sans Serif", 9F);
            OperationButton.Location = new Point(371, 360);
            OperationButton.Margin = new Padding(3, 4, 3, 4);
            OperationButton.Name = "OperationButton";
            OperationButton.Size = new Size(100, 30);
            OperationButton.TabIndex = 8;
            OperationButton.Text = "Add";
            OperationButton.UseVisualStyleBackColor = true;
            // 
            // CancelButton
            // 
            CancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            CancelButton.Font = new Font("Microsoft Sans Serif", 9F);
            CancelButton.Location = new Point(477, 360);
            CancelButton.Margin = new Padding(3, 4, 3, 4);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(100, 30);
            CancelButton.TabIndex = 9;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += CancelButton_Click;
            // 
            // AddEditQuestion
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(589, 403);
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
        private ComboBox QuestionTypeComboBox;
        private NumericUpDown QuestionOrderNumeric;
        private TextBox QuestionTextBox;
        private Label QuestionOrderType;
        private Label QuestionOrderLabel;
        private Label QuestionTextLabel;
        private Button OperationButton;
        private Button CancelButton;
    }
}
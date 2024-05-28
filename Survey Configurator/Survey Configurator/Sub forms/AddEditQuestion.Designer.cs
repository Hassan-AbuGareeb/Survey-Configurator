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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEditQuestion));
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
            resources.ApplyResources(QuestionTypeComboBox, "QuestionTypeComboBox");
            QuestionTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            QuestionTypeComboBox.FormattingEnabled = true;
            QuestionTypeComboBox.Items.AddRange(new object[] { resources.GetString("QuestionTypeComboBox.Items"), resources.GetString("QuestionTypeComboBox.Items1"), resources.GetString("QuestionTypeComboBox.Items2") });
            QuestionTypeComboBox.Name = "QuestionTypeComboBox";
            QuestionTypeComboBox.SelectedIndexChanged += QuestionTypeComboBox_SelectedIndexChanged;
            // 
            // QuestionOrderNumeric
            // 
            resources.ApplyResources(QuestionOrderNumeric, "QuestionOrderNumeric");
            QuestionOrderNumeric.Maximum = new decimal(new int[] { 120, 0, 0, 0 });
            QuestionOrderNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            QuestionOrderNumeric.Name = "QuestionOrderNumeric";
            QuestionOrderNumeric.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // QuestionTextBox
            // 
            resources.ApplyResources(QuestionTextBox, "QuestionTextBox");
            QuestionTextBox.Name = "QuestionTextBox";
            // 
            // QuestionOrderType
            // 
            resources.ApplyResources(QuestionOrderType, "QuestionOrderType");
            QuestionOrderType.Name = "QuestionOrderType";
            // 
            // QuestionOrderLabel
            // 
            resources.ApplyResources(QuestionOrderLabel, "QuestionOrderLabel");
            QuestionOrderLabel.Name = "QuestionOrderLabel";
            // 
            // QuestionTextLabel
            // 
            resources.ApplyResources(QuestionTextLabel, "QuestionTextLabel");
            QuestionTextLabel.Name = "QuestionTextLabel";
            // 
            // OperationButton
            // 
            resources.ApplyResources(OperationButton, "OperationButton");
            OperationButton.Name = "OperationButton";
            OperationButton.UseVisualStyleBackColor = true;
            // 
            // CancelButton
            // 
            resources.ApplyResources(CancelButton, "CancelButton");
            CancelButton.Name = "CancelButton";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += CancelButton_Click;
            // 
            // AddEditQuestion
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(CancelButton);
            Controls.Add(OperationButton);
            Controls.Add(QuestionTypeComboBox);
            Controls.Add(QuestionOrderNumeric);
            Controls.Add(QuestionTextBox);
            Controls.Add(QuestionOrderType);
            Controls.Add(QuestionOrderLabel);
            Controls.Add(QuestionTextLabel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "AddEditQuestion";
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
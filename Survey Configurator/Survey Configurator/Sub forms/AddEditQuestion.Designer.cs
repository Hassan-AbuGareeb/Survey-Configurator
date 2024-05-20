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
            StarsQuestionOptionsPanel = new Panel();
            NumberOfStarsNumeric = new NumericUpDown();
            NumberOfStarsLabel = new Label();
            SmileyQuestionOptionsPanel = new Panel();
            NumberOfSmileysNumeric = new NumericUpDown();
            NumberOfSmileysLabel = new Label();
            SliderQuestionOptionsPanel = new Panel();
            SliderEndValueCaptionText = new TextBox();
            SliderStartValueCaptionText = new TextBox();
            SliderEndValueCaptionLabel = new Label();
            SliderStartValueCaptionLabel = new Label();
            SliderEndValueNumeric = new NumericUpDown();
            SliderEndValueLabel = new Label();
            SliderStartValueNumeric = new NumericUpDown();
            SliderStartValueLabel = new Label();
            CancelButton = new Button();
            OperationButton = new Button();
            QuestionTypeComboBox = new ComboBox();
            QuestionOrderNumeric = new NumericUpDown();
            QuestionTextBox = new TextBox();
            QuestionOrderType = new Label();
            QuestionOrderLabel = new Label();
            QuestionTextLabel = new Label();
            AddEditLabel = new Label();
            panel1 = new Panel();
            StarsQuestionOptionsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NumberOfStarsNumeric).BeginInit();
            SmileyQuestionOptionsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)NumberOfSmileysNumeric).BeginInit();
            SliderQuestionOptionsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SliderEndValueNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SliderStartValueNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)QuestionOrderNumeric).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // StarsQuestionOptionsPanel
            // 
            StarsQuestionOptionsPanel.Anchor = AnchorStyles.Left;
            StarsQuestionOptionsPanel.Controls.Add(NumberOfStarsNumeric);
            StarsQuestionOptionsPanel.Controls.Add(NumberOfStarsLabel);
            StarsQuestionOptionsPanel.Location = new Point(0, 0);
            StarsQuestionOptionsPanel.Margin = new Padding(3, 4, 3, 4);
            StarsQuestionOptionsPanel.Name = "StarsQuestionOptionsPanel";
            StarsQuestionOptionsPanel.Size = new Size(235, 289);
            StarsQuestionOptionsPanel.TabIndex = 19;
            StarsQuestionOptionsPanel.Visible = false;
            // 
            // NumberOfStarsNumeric
            // 
            NumberOfStarsNumeric.Anchor = AnchorStyles.Left;
            NumberOfStarsNumeric.Location = new Point(0, 36);
            NumberOfStarsNumeric.Margin = new Padding(3, 4, 3, 4);
            NumberOfStarsNumeric.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            NumberOfStarsNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NumberOfStarsNumeric.Name = "NumberOfStarsNumeric";
            NumberOfStarsNumeric.Size = new Size(235, 27);
            NumberOfStarsNumeric.TabIndex = 4;
            NumberOfStarsNumeric.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // NumberOfStarsLabel
            // 
            NumberOfStarsLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            NumberOfStarsLabel.AutoSize = true;
            NumberOfStarsLabel.Font = new Font("Segoe UI", 14.25F);
            NumberOfStarsLabel.Location = new Point(0, 0);
            NumberOfStarsLabel.Name = "NumberOfStarsLabel";
            NumberOfStarsLabel.Size = new Size(186, 32);
            NumberOfStarsLabel.TabIndex = 21;
            NumberOfStarsLabel.Text = "Number of stars";
            // 
            // SmileyQuestionOptionsPanel
            // 
            SmileyQuestionOptionsPanel.Anchor = AnchorStyles.Left;
            SmileyQuestionOptionsPanel.Controls.Add(NumberOfSmileysNumeric);
            SmileyQuestionOptionsPanel.Controls.Add(NumberOfSmileysLabel);
            SmileyQuestionOptionsPanel.Location = new Point(0, 0);
            SmileyQuestionOptionsPanel.Margin = new Padding(3, 4, 3, 4);
            SmileyQuestionOptionsPanel.Name = "SmileyQuestionOptionsPanel";
            SmileyQuestionOptionsPanel.Size = new Size(235, 289);
            SmileyQuestionOptionsPanel.TabIndex = 22;
            SmileyQuestionOptionsPanel.Visible = false;
            // 
            // NumberOfSmileysNumeric
            // 
            NumberOfSmileysNumeric.Anchor = AnchorStyles.Left;
            NumberOfSmileysNumeric.Location = new Point(3, 36);
            NumberOfSmileysNumeric.Margin = new Padding(3, 4, 3, 4);
            NumberOfSmileysNumeric.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            NumberOfSmileysNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NumberOfSmileysNumeric.Name = "NumberOfSmileysNumeric";
            NumberOfSmileysNumeric.Size = new Size(232, 27);
            NumberOfSmileysNumeric.TabIndex = 4;
            NumberOfSmileysNumeric.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // NumberOfSmileysLabel
            // 
            NumberOfSmileysLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            NumberOfSmileysLabel.AutoSize = true;
            NumberOfSmileysLabel.Font = new Font("Segoe UI", 14.25F);
            NumberOfSmileysLabel.Location = new Point(0, 0);
            NumberOfSmileysLabel.Name = "NumberOfSmileysLabel";
            NumberOfSmileysLabel.Size = new Size(219, 32);
            NumberOfSmileysLabel.TabIndex = 21;
            NumberOfSmileysLabel.Text = "Number of Smileys";
            // 
            // SliderQuestionOptionsPanel
            // 
            SliderQuestionOptionsPanel.Anchor = AnchorStyles.Left;
            SliderQuestionOptionsPanel.Controls.Add(SliderEndValueCaptionText);
            SliderQuestionOptionsPanel.Controls.Add(SliderStartValueCaptionText);
            SliderQuestionOptionsPanel.Controls.Add(SliderEndValueCaptionLabel);
            SliderQuestionOptionsPanel.Controls.Add(SliderStartValueCaptionLabel);
            SliderQuestionOptionsPanel.Controls.Add(SliderEndValueNumeric);
            SliderQuestionOptionsPanel.Controls.Add(SliderEndValueLabel);
            SliderQuestionOptionsPanel.Controls.Add(SliderStartValueNumeric);
            SliderQuestionOptionsPanel.Controls.Add(SliderStartValueLabel);
            SliderQuestionOptionsPanel.Location = new Point(0, 0);
            SliderQuestionOptionsPanel.Margin = new Padding(3, 4, 3, 4);
            SliderQuestionOptionsPanel.Name = "SliderQuestionOptionsPanel";
            SliderQuestionOptionsPanel.Size = new Size(235, 289);
            SliderQuestionOptionsPanel.TabIndex = 23;
            SliderQuestionOptionsPanel.Visible = false;
            // 
            // SliderEndValueCaptionText
            // 
            SliderEndValueCaptionText.Location = new Point(0, 262);
            SliderEndValueCaptionText.MaxLength = 40;
            SliderEndValueCaptionText.Name = "SliderEndValueCaptionText";
            SliderEndValueCaptionText.Size = new Size(229, 27);
            SliderEndValueCaptionText.TabIndex = 27;
            SliderEndValueCaptionText.Text = "Max";
            // 
            // SliderStartValueCaptionText
            // 
            SliderStartValueCaptionText.Location = new Point(-3, 190);
            SliderStartValueCaptionText.MaxLength = 40;
            SliderStartValueCaptionText.Name = "SliderStartValueCaptionText";
            SliderStartValueCaptionText.Size = new Size(229, 27);
            SliderStartValueCaptionText.TabIndex = 26;
            SliderStartValueCaptionText.Text = "Min";
            // 
            // SliderEndValueCaptionLabel
            // 
            SliderEndValueCaptionLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            SliderEndValueCaptionLabel.AutoSize = true;
            SliderEndValueCaptionLabel.Font = new Font("Segoe UI", 14.25F);
            SliderEndValueCaptionLabel.Location = new Point(0, 227);
            SliderEndValueCaptionLabel.Name = "SliderEndValueCaptionLabel";
            SliderEndValueCaptionLabel.Size = new Size(204, 32);
            SliderEndValueCaptionLabel.TabIndex = 25;
            SliderEndValueCaptionLabel.Text = "End value caption";
            // 
            // SliderStartValueCaptionLabel
            // 
            SliderStartValueCaptionLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            SliderStartValueCaptionLabel.AutoSize = true;
            SliderStartValueCaptionLabel.Font = new Font("Segoe UI", 14.25F);
            SliderStartValueCaptionLabel.Location = new Point(0, 155);
            SliderStartValueCaptionLabel.Name = "SliderStartValueCaptionLabel";
            SliderStartValueCaptionLabel.Size = new Size(212, 32);
            SliderStartValueCaptionLabel.TabIndex = 24;
            SliderStartValueCaptionLabel.Text = "Start value caption";
            // 
            // SliderEndValueNumeric
            // 
            SliderEndValueNumeric.Anchor = AnchorStyles.Left;
            SliderEndValueNumeric.Location = new Point(0, 114);
            SliderEndValueNumeric.Margin = new Padding(3, 4, 3, 4);
            SliderEndValueNumeric.Minimum = new decimal(new int[] { 50, 0, 0, 0 });
            SliderEndValueNumeric.Name = "SliderEndValueNumeric";
            SliderEndValueNumeric.Size = new Size(232, 27);
            SliderEndValueNumeric.TabIndex = 22;
            SliderEndValueNumeric.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // SliderEndValueLabel
            // 
            SliderEndValueLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            SliderEndValueLabel.AutoSize = true;
            SliderEndValueLabel.Font = new Font("Segoe UI", 14.25F);
            SliderEndValueLabel.Location = new Point(0, 78);
            SliderEndValueLabel.Name = "SliderEndValueLabel";
            SliderEndValueLabel.Size = new Size(118, 32);
            SliderEndValueLabel.TabIndex = 23;
            SliderEndValueLabel.Text = "End value";
            // 
            // SliderStartValueNumeric
            // 
            SliderStartValueNumeric.Anchor = AnchorStyles.Left;
            SliderStartValueNumeric.Location = new Point(0, 36);
            SliderStartValueNumeric.Margin = new Padding(3, 4, 3, 4);
            SliderStartValueNumeric.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            SliderStartValueNumeric.Name = "SliderStartValueNumeric";
            SliderStartValueNumeric.Size = new Size(232, 27);
            SliderStartValueNumeric.TabIndex = 4;
            // 
            // SliderStartValueLabel
            // 
            SliderStartValueLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            SliderStartValueLabel.AutoSize = true;
            SliderStartValueLabel.Font = new Font("Segoe UI", 14.25F);
            SliderStartValueLabel.Location = new Point(0, 0);
            SliderStartValueLabel.Name = "SliderStartValueLabel";
            SliderStartValueLabel.Size = new Size(126, 32);
            SliderStartValueLabel.TabIndex = 21;
            SliderStartValueLabel.Text = "Start value";
            // 
            // CancelButton
            // 
            CancelButton.Anchor = AnchorStyles.Left;
            CancelButton.Font = new Font("Segoe UI", 12F);
            CancelButton.Location = new Point(550, 750);
            CancelButton.Margin = new Padding(3, 4, 3, 4);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(120, 40);
            CancelButton.TabIndex = 9;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += CancelButton_Click;
            // 
            // OperationButton
            // 
            OperationButton.Anchor = AnchorStyles.Left;
            OperationButton.Font = new Font("Segoe UI", 12F);
            OperationButton.Location = new Point(424, 750);
            OperationButton.Margin = new Padding(3, 4, 3, 4);
            OperationButton.Name = "OperationButton";
            OperationButton.Size = new Size(120, 40);
            OperationButton.TabIndex = 8;
            OperationButton.Text = "Add";
            OperationButton.UseVisualStyleBackColor = true;
            // 
            // QuestionTypeComboBox
            // 
            QuestionTypeComboBox.Anchor = AnchorStyles.Left;
            QuestionTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            QuestionTypeComboBox.FormattingEnabled = true;
            QuestionTypeComboBox.Items.AddRange(new object[] { SharedResources.QuestionType.Stars, SharedResources.QuestionType.Smiley, SharedResources.QuestionType.Slider });
            QuestionTypeComboBox.Location = new Point(31, 393);
            QuestionTypeComboBox.Margin = new Padding(3, 4, 3, 4);
            QuestionTypeComboBox.Name = "QuestionTypeComboBox";
            QuestionTypeComboBox.Size = new Size(235, 28);
            QuestionTypeComboBox.TabIndex = 3;
            QuestionTypeComboBox.SelectedIndexChanged += QuestionTypeComboBox_SelectedIndexChanged;
            // 
            // QuestionOrderNumeric
            // 
            QuestionOrderNumeric.Anchor = AnchorStyles.Left;
            QuestionOrderNumeric.Location = new Point(31, 310);
            QuestionOrderNumeric.Margin = new Padding(3, 4, 3, 4);
            QuestionOrderNumeric.Maximum = new decimal(new int[] { 120, 0, 0, 0 });
            QuestionOrderNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            QuestionOrderNumeric.Name = "QuestionOrderNumeric";
            QuestionOrderNumeric.Size = new Size(235, 27);
            QuestionOrderNumeric.TabIndex = 2;
            QuestionOrderNumeric.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // QuestionTextBox
            // 
            QuestionTextBox.Anchor = AnchorStyles.Left;
            QuestionTextBox.Location = new Point(31, 116);
            QuestionTextBox.Margin = new Padding(3, 4, 3, 4);
            QuestionTextBox.MaxLength = 350;
            QuestionTextBox.Multiline = true;
            QuestionTextBox.Name = "QuestionTextBox";
            QuestionTextBox.Size = new Size(559, 140);
            QuestionTextBox.TabIndex = 1;
            // 
            // QuestionOrderType
            // 
            QuestionOrderType.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            QuestionOrderType.AutoSize = true;
            QuestionOrderType.Font = new Font("Segoe UI", 14.25F);
            QuestionOrderType.Location = new Point(31, 357);
            QuestionOrderType.Name = "QuestionOrderType";
            QuestionOrderType.Size = new Size(65, 32);
            QuestionOrderType.TabIndex = 12;
            QuestionOrderType.Text = "Type";
            // 
            // QuestionOrderLabel
            // 
            QuestionOrderLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            QuestionOrderLabel.AutoSize = true;
            QuestionOrderLabel.Font = new Font("Segoe UI", 14.25F);
            QuestionOrderLabel.Location = new Point(31, 274);
            QuestionOrderLabel.Name = "QuestionOrderLabel";
            QuestionOrderLabel.Size = new Size(75, 32);
            QuestionOrderLabel.TabIndex = 11;
            QuestionOrderLabel.Text = "Order";
            // 
            // QuestionTextLabel
            // 
            QuestionTextLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            QuestionTextLabel.AutoSize = true;
            QuestionTextLabel.Font = new Font("Segoe UI", 14.25F);
            QuestionTextLabel.Location = new Point(31, 80);
            QuestionTextLabel.Name = "QuestionTextLabel";
            QuestionTextLabel.Size = new Size(57, 32);
            QuestionTextLabel.TabIndex = 10;
            QuestionTextLabel.Text = "Text";
            // 
            // AddEditLabel
            // 
            AddEditLabel.AutoSize = true;
            AddEditLabel.Font = new Font("Segoe UI", 21F);
            AddEditLabel.Location = new Point(31, 18);
            AddEditLabel.Name = "AddEditLabel";
            AddEditLabel.Size = new Size(235, 47);
            AddEditLabel.TabIndex = 20;
            AddEditLabel.Text = "Add Question";
            // 
            // panel1
            // 
            panel1.Controls.Add(SmileyQuestionOptionsPanel);
            panel1.Controls.Add(StarsQuestionOptionsPanel);
            panel1.Controls.Add(SliderQuestionOptionsPanel);
            panel1.Location = new Point(31, 439);
            panel1.Name = "panel1";
            panel1.Size = new Size(238, 289);
            panel1.TabIndex = 23;
            // 
            // AddEditQuestion
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(702, 853);
            Controls.Add(panel1);
            Controls.Add(AddEditLabel);
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
            MaximumSize = new Size(720, 900);
            MinimumSize = new Size(720, 900);
            Name = "AddEditQuestion";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Add Edit";
            FormClosing += AddEditQuestion_FormClosing;
            Load += AddEdit_Load;
            StarsQuestionOptionsPanel.ResumeLayout(false);
            StarsQuestionOptionsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NumberOfStarsNumeric).EndInit();
            SmileyQuestionOptionsPanel.ResumeLayout(false);
            SmileyQuestionOptionsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)NumberOfSmileysNumeric).EndInit();
            SliderQuestionOptionsPanel.ResumeLayout(false);
            SliderQuestionOptionsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SliderEndValueNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)SliderStartValueNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)QuestionOrderNumeric).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel StarsQuestionOptionsPanel;
        private Button CancelButton;
        private Button OperationButton;
        private ComboBox QuestionTypeComboBox;
        private NumericUpDown QuestionOrderNumeric;
        private TextBox QuestionTextBox;
        private Label QuestionOrderType;
        private Label QuestionOrderLabel;
        private Label QuestionTextLabel;
        private Label AddEditLabel;
        private Label NumberOfStarsLabel;
        private NumericUpDown NumberOfStarsNumeric;
        private Panel SmileyQuestionOptionsPanel;
        private NumericUpDown NumberOfSmileysNumeric;
        private Label NumberOfSmileysLabel;
        private Panel SliderQuestionOptionsPanel;
        private NumericUpDown SliderStartValueNumeric;
        private Label SliderStartValueLabel;
        private TextBox SliderEndValueCaptionText;
        private TextBox SliderStartValueCaptionText;
        private Label SliderEndValueCaptionLabel;
        private Label SliderStartValueCaptionLabel;
        private NumericUpDown SliderEndValueNumeric;
        private Label SliderEndValueLabel;
        private Panel panel1;
    }
}
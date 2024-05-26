namespace Survey_Configurator.Custom_controls
{
    partial class SliderQuestionOptions
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
            SliderEndValueCaptionText = new TextBox();
            SliderStartValueCaptionText = new TextBox();
            SliderEndValueCaptionLabel = new Label();
            SliderStartValueCaptionLabel = new Label();
            SliderEndValueNumeric = new NumericUpDown();
            SliderEndValueLabel = new Label();
            SliderStartValueLabel = new Label();
            SliderStartValueNumeric = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)SliderEndValueNumeric).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SliderStartValueNumeric).BeginInit();
            SuspendLayout();
            // 
            // SliderEndValueCaptionText
            // 
            SliderEndValueCaptionText.Location = new Point(182, 135);
            SliderEndValueCaptionText.MaxLength = 40;
            SliderEndValueCaptionText.Name = "SliderEndValueCaptionText";
            SliderEndValueCaptionText.Size = new Size(386, 27);
            SliderEndValueCaptionText.TabIndex = 35;
            SliderEndValueCaptionText.Text = "Max";
            // 
            // SliderStartValueCaptionText
            // 
            SliderStartValueCaptionText.Location = new Point(182, 90);
            SliderStartValueCaptionText.MaxLength = 40;
            SliderStartValueCaptionText.Name = "SliderStartValueCaptionText";
            SliderStartValueCaptionText.Size = new Size(386, 27);
            SliderStartValueCaptionText.TabIndex = 34;
            SliderStartValueCaptionText.Text = "Min";
            // 
            // SliderEndValueCaptionLabel
            // 
            SliderEndValueCaptionLabel.AutoSize = true;
            SliderEndValueCaptionLabel.Font = new Font("Segoe UI", 9F);
            SliderEndValueCaptionLabel.Location = new Point(3, 142);
            SliderEndValueCaptionLabel.Name = "SliderEndValueCaptionLabel";
            SliderEndValueCaptionLabel.Size = new Size(130, 20);
            SliderEndValueCaptionLabel.TabIndex = 33;
            SliderEndValueCaptionLabel.Text = "End value caption:";
            // 
            // SliderStartValueCaptionLabel
            // 
            SliderStartValueCaptionLabel.AutoSize = true;
            SliderStartValueCaptionLabel.Font = new Font("Segoe UI", 9F);
            SliderStartValueCaptionLabel.Location = new Point(3, 93);
            SliderStartValueCaptionLabel.Name = "SliderStartValueCaptionLabel";
            SliderStartValueCaptionLabel.Size = new Size(136, 20);
            SliderStartValueCaptionLabel.TabIndex = 32;
            SliderStartValueCaptionLabel.Text = "Start value caption:";
            // 
            // SliderEndValueNumeric
            // 
            SliderEndValueNumeric.Location = new Point(182, 46);
            SliderEndValueNumeric.Margin = new Padding(3, 4, 3, 4);
            SliderEndValueNumeric.Minimum = new decimal(new int[] { 50, 0, 0, 0 });
            SliderEndValueNumeric.Name = "SliderEndValueNumeric";
            SliderEndValueNumeric.Size = new Size(386, 27);
            SliderEndValueNumeric.TabIndex = 30;
            SliderEndValueNumeric.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // SliderEndValueLabel
            // 
            SliderEndValueLabel.AutoSize = true;
            SliderEndValueLabel.Font = new Font("Segoe UI", 9F);
            SliderEndValueLabel.Location = new Point(3, 48);
            SliderEndValueLabel.Name = "SliderEndValueLabel";
            SliderEndValueLabel.Size = new Size(76, 20);
            SliderEndValueLabel.TabIndex = 31;
            SliderEndValueLabel.Text = "End value:";
            // 
            // SliderStartValueLabel
            // 
            SliderStartValueLabel.AutoSize = true;
            SliderStartValueLabel.Font = new Font("Segoe UI", 9F);
            SliderStartValueLabel.Location = new Point(3, 2);
            SliderStartValueLabel.Name = "SliderStartValueLabel";
            SliderStartValueLabel.Size = new Size(82, 20);
            SliderStartValueLabel.TabIndex = 29;
            SliderStartValueLabel.Text = "Start value:";
            // 
            // SliderStartValueNumeric
            // 
            SliderStartValueNumeric.Anchor = AnchorStyles.Top;
            SliderStartValueNumeric.Location = new Point(182, 2);
            SliderStartValueNumeric.Margin = new Padding(3, 4, 3, 4);
            SliderStartValueNumeric.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            SliderStartValueNumeric.Name = "SliderStartValueNumeric";
            SliderStartValueNumeric.Size = new Size(386, 27);
            SliderStartValueNumeric.TabIndex = 36;
            // 
            // SliderQuestionOptions
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(SliderStartValueNumeric);
            Controls.Add(SliderEndValueCaptionText);
            Controls.Add(SliderStartValueCaptionText);
            Controls.Add(SliderEndValueCaptionLabel);
            Controls.Add(SliderStartValueCaptionLabel);
            Controls.Add(SliderEndValueNumeric);
            Controls.Add(SliderEndValueLabel);
            Controls.Add(SliderStartValueLabel);
            Name = "SliderQuestionOptions";
            Size = new Size(679, 188);
            Load += SliderQuestionOptions_Load;
            ((System.ComponentModel.ISupportInitialize)SliderEndValueNumeric).EndInit();
            ((System.ComponentModel.ISupportInitialize)SliderStartValueNumeric).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public TextBox SliderEndValueCaptionText;
        public TextBox SliderStartValueCaptionText;
        private Label SliderEndValueCaptionLabel;
        private Label SliderStartValueCaptionLabel;
        public NumericUpDown SliderEndValueNumeric;
        private Label SliderEndValueLabel;
        private Label SliderStartValueLabel;
        public NumericUpDown SliderStartValueNumeric;
    }
}

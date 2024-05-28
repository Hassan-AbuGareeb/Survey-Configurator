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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SliderQuestionOptions));
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
            resources.ApplyResources(SliderEndValueCaptionText, "SliderEndValueCaptionText");
            SliderEndValueCaptionText.Name = "SliderEndValueCaptionText";
            // 
            // SliderStartValueCaptionText
            // 
            resources.ApplyResources(SliderStartValueCaptionText, "SliderStartValueCaptionText");
            SliderStartValueCaptionText.Name = "SliderStartValueCaptionText";
            // 
            // SliderEndValueCaptionLabel
            // 
            resources.ApplyResources(SliderEndValueCaptionLabel, "SliderEndValueCaptionLabel");
            SliderEndValueCaptionLabel.Name = "SliderEndValueCaptionLabel";
            // 
            // SliderStartValueCaptionLabel
            // 
            resources.ApplyResources(SliderStartValueCaptionLabel, "SliderStartValueCaptionLabel");
            SliderStartValueCaptionLabel.Name = "SliderStartValueCaptionLabel";
            // 
            // SliderEndValueNumeric
            // 
            resources.ApplyResources(SliderEndValueNumeric, "SliderEndValueNumeric");
            SliderEndValueNumeric.Name = "SliderEndValueNumeric";
            SliderEndValueNumeric.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // SliderEndValueLabel
            // 
            resources.ApplyResources(SliderEndValueLabel, "SliderEndValueLabel");
            SliderEndValueLabel.Name = "SliderEndValueLabel";
            // 
            // SliderStartValueLabel
            // 
            resources.ApplyResources(SliderStartValueLabel, "SliderStartValueLabel");
            SliderStartValueLabel.Name = "SliderStartValueLabel";
            // 
            // SliderStartValueNumeric
            // 
            resources.ApplyResources(SliderStartValueNumeric, "SliderStartValueNumeric");
            SliderStartValueNumeric.Name = "SliderStartValueNumeric";
            // 
            // SliderQuestionOptions
            // 
            resources.ApplyResources(this, "$this");
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

namespace Survey_Configurator.Custom_controls
{
    partial class StarsQuestionOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StarsQuestionOptions));
            NumberOfStarsNumeric = new NumericUpDown();
            NumberOfStarsLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)NumberOfStarsNumeric).BeginInit();
            SuspendLayout();
            // 
            // NumberOfStarsNumeric
            // 
            resources.ApplyResources(NumberOfStarsNumeric, "NumberOfStarsNumeric");
            NumberOfStarsNumeric.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            NumberOfStarsNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NumberOfStarsNumeric.Name = "NumberOfStarsNumeric";
            NumberOfStarsNumeric.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // NumberOfStarsLabel
            // 
            resources.ApplyResources(NumberOfStarsLabel, "NumberOfStarsLabel");
            NumberOfStarsLabel.Name = "NumberOfStarsLabel";
            // 
            // StarsQuestionOptions
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(NumberOfStarsNumeric);
            Controls.Add(NumberOfStarsLabel);
            Name = "StarsQuestionOptions";
            Load += StarsQuestionOptions_Load;
            ((System.ComponentModel.ISupportInitialize)NumberOfStarsNumeric).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public NumericUpDown NumberOfStarsNumeric;
        private Label NumberOfStarsLabel;
    }
}

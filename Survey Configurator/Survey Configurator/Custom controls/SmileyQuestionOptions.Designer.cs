namespace Survey_Configurator.Custom_controls
{
    partial class SmileyQuestionOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SmileyQuestionOptions));
            NumberOfSmileysLabel = new Label();
            NumberOfSmileysNumeric = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)NumberOfSmileysNumeric).BeginInit();
            SuspendLayout();
            // 
            // NumberOfSmileysLabel
            // 
            resources.ApplyResources(NumberOfSmileysLabel, "NumberOfSmileysLabel");
            NumberOfSmileysLabel.Name = "NumberOfSmileysLabel";
            // 
            // NumberOfSmileysNumeric
            // 
            resources.ApplyResources(NumberOfSmileysNumeric, "NumberOfSmileysNumeric");
            NumberOfSmileysNumeric.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            NumberOfSmileysNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NumberOfSmileysNumeric.Name = "NumberOfSmileysNumeric";
            NumberOfSmileysNumeric.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // SmileyQuestionOptions
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(NumberOfSmileysNumeric);
            Controls.Add(NumberOfSmileysLabel);
            Name = "SmileyQuestionOptions";
            Load += SmileyQuestionOptions_Load;
            ((System.ComponentModel.ISupportInitialize)NumberOfSmileysNumeric).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label NumberOfSmileysLabel;
        public NumericUpDown NumberOfSmileysNumeric;
    }
}

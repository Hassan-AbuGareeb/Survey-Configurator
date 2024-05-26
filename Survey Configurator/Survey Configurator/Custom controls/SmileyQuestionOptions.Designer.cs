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
            NumberOfSmileysNumeric = new NumericUpDown();
            NumberOfSmileysLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)NumberOfSmileysNumeric).BeginInit();
            SuspendLayout();
            // 
            // NumberOfSmileysNumeric
            // 
            NumberOfSmileysNumeric.Anchor = AnchorStyles.Top;
            NumberOfSmileysNumeric.Location = new Point(180, 0);
            NumberOfSmileysNumeric.Margin = new Padding(3, 4, 3, 4);
            NumberOfSmileysNumeric.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            NumberOfSmileysNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NumberOfSmileysNumeric.Name = "NumberOfSmileysNumeric";
            NumberOfSmileysNumeric.Size = new Size(386, 27);
            NumberOfSmileysNumeric.TabIndex = 22;
            NumberOfSmileysNumeric.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // NumberOfSmileysLabel
            // 
            NumberOfSmileysLabel.Anchor = AnchorStyles.Top;
            NumberOfSmileysLabel.AutoSize = true;
            NumberOfSmileysLabel.Font = new Font("Segoe UI", 9F);
            NumberOfSmileysLabel.Location = new Point(3, 0);
            NumberOfSmileysLabel.Name = "NumberOfSmileysLabel";
            NumberOfSmileysLabel.Size = new Size(138, 20);
            NumberOfSmileysLabel.TabIndex = 23;
            NumberOfSmileysLabel.Text = "Number of Smileys:";
            // 
            // SmileyQuestionOptions
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(NumberOfSmileysNumeric);
            Controls.Add(NumberOfSmileysLabel);
            Name = "SmileyQuestionOptions";
            Size = new Size(670, 35);
            Load += SmileyQuestionOptions_Load;
            ((System.ComponentModel.ISupportInitialize)NumberOfSmileysNumeric).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public NumericUpDown NumberOfSmileysNumeric;
        private Label NumberOfSmileysLabel;
    }
}

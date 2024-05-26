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
            NumberOfStarsNumeric = new NumericUpDown();
            NumberOfStarsLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)NumberOfStarsNumeric).BeginInit();
            SuspendLayout();
            // 
            // NumberOfStarsNumeric
            // 
            NumberOfStarsNumeric.Anchor = AnchorStyles.Top;
            NumberOfStarsNumeric.Location = new Point(181, 0);
            NumberOfStarsNumeric.Margin = new Padding(3, 4, 3, 4);
            NumberOfStarsNumeric.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            NumberOfStarsNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            NumberOfStarsNumeric.Name = "NumberOfStarsNumeric";
            NumberOfStarsNumeric.Size = new Size(386, 27);
            NumberOfStarsNumeric.TabIndex = 24;
            NumberOfStarsNumeric.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // NumberOfStarsLabel
            // 
            NumberOfStarsLabel.AutoSize = true;
            NumberOfStarsLabel.Font = new Font("Segoe UI", 9F);
            NumberOfStarsLabel.Location = new Point(3, 0);
            NumberOfStarsLabel.Name = "NumberOfStarsLabel";
            NumberOfStarsLabel.Size = new Size(118, 20);
            NumberOfStarsLabel.TabIndex = 25;
            NumberOfStarsLabel.Text = "Number of stars:";
            // 
            // StarsQuestionOptions
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(NumberOfStarsNumeric);
            Controls.Add(NumberOfStarsLabel);
            Name = "StarsQuestionOptions";
            Size = new Size(670, 32);
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

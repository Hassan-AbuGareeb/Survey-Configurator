namespace Survey_Configurator
{
    partial class MainScreen
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
            groupBox1 = new GroupBox();
            DeleteQuestionButton = new Button();
            EditQuestionButton = new Button();
            AddQuestionButton = new Button();
            AlphabeticalRadioButton = new RadioButton();
            NumberRadioButton = new RadioButton();
            label1 = new Label();
            panel1 = new Panel();
            QuestionsListBox = new ListBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(DeleteQuestionButton);
            groupBox1.Controls.Add(EditQuestionButton);
            groupBox1.Controls.Add(AddQuestionButton);
            groupBox1.Controls.Add(AlphabeticalRadioButton);
            groupBox1.Controls.Add(NumberRadioButton);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(panel1);
            groupBox1.Controls.Add(QuestionsListBox);
            groupBox1.Location = new Point(0, 2);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(1420, 573);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "groupBox1";
            // 
            // DeleteQuestionButton
            // 
            DeleteQuestionButton.Location = new Point(590, 500);
            DeleteQuestionButton.Margin = new Padding(3, 2, 3, 2);
            DeleteQuestionButton.Name = "DeleteQuestionButton";
            DeleteQuestionButton.Size = new Size(82, 22);
            DeleteQuestionButton.TabIndex = 7;
            DeleteQuestionButton.Text = "Delete";
            DeleteQuestionButton.UseVisualStyleBackColor = true;
            DeleteQuestionButton.Click += DeleteQuestionButton_Click;
            // 
            // EditQuestionButton
            // 
            EditQuestionButton.Location = new Point(363, 500);
            EditQuestionButton.Margin = new Padding(3, 2, 3, 2);
            EditQuestionButton.Name = "EditQuestionButton";
            EditQuestionButton.Size = new Size(82, 22);
            EditQuestionButton.TabIndex = 6;
            EditQuestionButton.Text = "Edit";
            EditQuestionButton.UseVisualStyleBackColor = true;
            EditQuestionButton.Click += EditQuestionButton_Click;
            // 
            // AddQuestionButton
            // 
            AddQuestionButton.Location = new Point(151, 500);
            AddQuestionButton.Margin = new Padding(3, 2, 3, 2);
            AddQuestionButton.Name = "AddQuestionButton";
            AddQuestionButton.Size = new Size(82, 22);
            AddQuestionButton.TabIndex = 5;
            AddQuestionButton.Text = "Add";
            AddQuestionButton.UseVisualStyleBackColor = true;
            AddQuestionButton.Click += AddQuestionButton_Click;
            // 
            // AlphabeticalRadioButton
            // 
            AlphabeticalRadioButton.Appearance = Appearance.Button;
            AlphabeticalRadioButton.AutoSize = true;
            AlphabeticalRadioButton.Location = new Point(363, 33);
            AlphabeticalRadioButton.Margin = new Padding(3, 2, 3, 2);
            AlphabeticalRadioButton.Name = "AlphabeticalRadioButton";
            AlphabeticalRadioButton.Size = new Size(83, 25);
            AlphabeticalRadioButton.TabIndex = 4;
            AlphabeticalRadioButton.TabStop = true;
            AlphabeticalRadioButton.Text = "Alphabetical";
            AlphabeticalRadioButton.UseVisualStyleBackColor = true;
            AlphabeticalRadioButton.CheckedChanged += AlphabeticalRadioButton_CheckedChanged;
            // 
            // NumberRadioButton
            // 
            NumberRadioButton.Appearance = Appearance.Button;
            NumberRadioButton.AutoSize = true;
            NumberRadioButton.Location = new Point(270, 33);
            NumberRadioButton.Margin = new Padding(3, 2, 3, 2);
            NumberRadioButton.Name = "NumberRadioButton";
            NumberRadioButton.Size = new Size(61, 25);
            NumberRadioButton.TabIndex = 3;
            NumberRadioButton.TabStop = true;
            NumberRadioButton.Text = "Number";
            NumberRadioButton.UseVisualStyleBackColor = true;
            NumberRadioButton.CheckedChanged += NumberRadioButton_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(151, 30);
            label1.Name = "label1";
            label1.Size = new Size(90, 25);
            label1.TabIndex = 2;
            label1.Text = "Order by:";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlLightLight;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Location = new Point(797, 63);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(520, 408);
            panel1.TabIndex = 1;
            // 
            // QuestionsListBox
            // 
            QuestionsListBox.FormattingEnabled = true;
            QuestionsListBox.ItemHeight = 15;
            QuestionsListBox.Items.AddRange(new object[] { "it1", "it2", "it3", "it4", "it0" });
            QuestionsListBox.Location = new Point(151, 63);
            QuestionsListBox.Margin = new Padding(3, 2, 3, 2);
            QuestionsListBox.Name = "QuestionsListBox";
            QuestionsListBox.SelectionMode = SelectionMode.MultiSimple;
            QuestionsListBox.Size = new Size(521, 409);
            QuestionsListBox.TabIndex = 0;
            // 
            // MainScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1421, 658);
            Controls.Add(groupBox1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "MainScreen";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button button4;
        private Button button2;
        private Button AddQuestionButton;
        private RadioButton AlphabeticalRadioButton;
        private RadioButton NumberRadioButton;
        private Label label1;
        private Panel panel1;
        private ListBox QuestionsListBox;
        private Button DeleteQuestionButton;
        private Button EditQuestionButton;
    }
}
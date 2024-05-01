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
            QuestionsDataGrid = new DataGridView();
            button1 = new Button();
            DeleteQuestionButton = new Button();
            EditQuestionButton = new Button();
            AddQuestionButton = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)QuestionsDataGrid).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(QuestionsDataGrid);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(DeleteQuestionButton);
            groupBox1.Controls.Add(EditQuestionButton);
            groupBox1.Controls.Add(AddQuestionButton);
            groupBox1.Location = new Point(0, 2);
            groupBox1.Margin = new Padding(3, 2, 3, 2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 2, 3, 2);
            groupBox1.Size = new Size(1420, 573);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "groupBox1";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // QuestionsDataGrid
            // 
            QuestionsDataGrid.AllowUserToAddRows = false;
            QuestionsDataGrid.AllowUserToDeleteRows = false;
            QuestionsDataGrid.AllowUserToOrderColumns = true;
            QuestionsDataGrid.BackgroundColor = SystemColors.ControlLight;
            QuestionsDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            QuestionsDataGrid.Location = new Point(363, 89);
            QuestionsDataGrid.Name = "QuestionsDataGrid";
            QuestionsDataGrid.ReadOnly = true;
            QuestionsDataGrid.Size = new Size(523, 350);
            QuestionsDataGrid.TabIndex = 9;
            // 
            // button1
            // 
            button1.Location = new Point(374, 541);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 8;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
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
            // MainScreen
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1421, 658);
            Controls.Add(groupBox1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "MainScreen";
            Text = "Form1";
            Load += MainScreen_Load;
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)QuestionsDataGrid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button button4;
        private Button button2;
        private Button AddQuestionButton;
        private Button DeleteQuestionButton;
        private Button EditQuestionButton;
        private Button button1;
        private DataGridView QuestionsDataGrid;
    }
}
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
            groupBox1.Location = new Point(0, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1623, 764);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "groupBox1";
            // 
            // QuestionsDataGrid
            // 
            QuestionsDataGrid.AllowUserToAddRows = false;
            QuestionsDataGrid.AllowUserToDeleteRows = false;
            QuestionsDataGrid.AllowUserToOrderColumns = true;
            QuestionsDataGrid.BackgroundColor = SystemColors.ControlLight;
            QuestionsDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            QuestionsDataGrid.Location = new Point(415, 119);
            QuestionsDataGrid.Margin = new Padding(3, 4, 3, 4);
            QuestionsDataGrid.Name = "QuestionsDataGrid";
            QuestionsDataGrid.ReadOnly = true;
            QuestionsDataGrid.RowHeadersWidth = 51;
            QuestionsDataGrid.Size = new Size(598, 467);
            QuestionsDataGrid.TabIndex = 9;
            QuestionsDataGrid.SelectionChanged += QuestionsDataGrid_SelectionChanged;
            // 
            // button1
            // 
            button1.Location = new Point(427, 721);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(86, 31);
            button1.TabIndex = 8;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // DeleteQuestionButton
            // 
            DeleteQuestionButton.Location = new Point(674, 667);
            DeleteQuestionButton.Name = "DeleteQuestionButton";
            DeleteQuestionButton.Size = new Size(94, 29);
            DeleteQuestionButton.TabIndex = 7;
            DeleteQuestionButton.Text = "Delete";
            DeleteQuestionButton.UseVisualStyleBackColor = true;
            DeleteQuestionButton.Click += DeleteQuestionButton_Click;
            // 
            // EditQuestionButton
            // 
            EditQuestionButton.Location = new Point(415, 667);
            EditQuestionButton.Name = "EditQuestionButton";
            EditQuestionButton.Size = new Size(94, 29);
            EditQuestionButton.TabIndex = 6;
            EditQuestionButton.Text = "Edit";
            EditQuestionButton.UseVisualStyleBackColor = true;
            EditQuestionButton.Click += AddEditQuestionButton_Click;
            // 
            // AddQuestionButton
            // 
            AddQuestionButton.Location = new Point(173, 667);
            AddQuestionButton.Name = "AddQuestionButton";
            AddQuestionButton.Size = new Size(94, 29);
            AddQuestionButton.TabIndex = 5;
            AddQuestionButton.Text = "Add";
            AddQuestionButton.UseVisualStyleBackColor = true;
            AddQuestionButton.Click += AddEditQuestionButton_Click;
            // 
            // MainScreen
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1624, 877);
            Controls.Add(groupBox1);
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
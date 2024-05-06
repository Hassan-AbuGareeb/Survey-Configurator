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
            DeleteQuestionButton = new Button();
            EditQuestionButton = new Button();
            AddQuestionButton = new Button();
            button1 = new Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)QuestionsDataGrid).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(QuestionsDataGrid);
            groupBox1.Controls.Add(DeleteQuestionButton);
            groupBox1.Controls.Add(EditQuestionButton);
            groupBox1.Controls.Add(AddQuestionButton);
            groupBox1.Location = new Point(0, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1623, 1009);
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
            // DeleteQuestionButton
            // 
            DeleteQuestionButton.Location = new Point(910, 668);
            DeleteQuestionButton.Name = "DeleteQuestionButton";
            DeleteQuestionButton.Size = new Size(103, 65);
            DeleteQuestionButton.TabIndex = 7;
            DeleteQuestionButton.Text = "Delete";
            DeleteQuestionButton.UseVisualStyleBackColor = true;
            DeleteQuestionButton.Click += DeleteQuestionButton_Click;
            // 
            // EditQuestionButton
            // 
            EditQuestionButton.Location = new Point(663, 668);
            EditQuestionButton.Name = "EditQuestionButton";
            EditQuestionButton.Size = new Size(103, 65);
            EditQuestionButton.TabIndex = 6;
            EditQuestionButton.Text = "Edit";
            EditQuestionButton.UseVisualStyleBackColor = true;
            EditQuestionButton.Click += EditQuestionButton_Click;
            // 
            // AddQuestionButton
            // 
            AddQuestionButton.Location = new Point(415, 668);
            AddQuestionButton.Name = "AddQuestionButton";
            AddQuestionButton.Size = new Size(103, 65);
            AddQuestionButton.TabIndex = 5;
            AddQuestionButton.Text = "Add";
            AddQuestionButton.UseVisualStyleBackColor = true;
            AddQuestionButton.Click += AddQuestionButton_Click;
            // 
            // button1
            // 
            button1.Location = new Point(663, 806);
            button1.Name = "button1";
            button1.Size = new Size(103, 57);
            button1.TabIndex = 10;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // MainScreen
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1624, 1003);
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
        private Button AddQuestionButton;
        private Button DeleteQuestionButton;
        private Button EditQuestionButton;
        private DataGridView QuestionsDataGrid;
        private Button button1;
    }
}
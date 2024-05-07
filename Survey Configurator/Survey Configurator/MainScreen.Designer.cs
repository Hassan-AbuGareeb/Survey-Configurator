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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            QuestionsDataGrid = new DataGridView();
            DeleteQuestionButton = new Button();
            EditQuestionButton = new Button();
            AddQuestionButton = new Button();
            sqlCommandBuilder1 = new Microsoft.Data.SqlClient.SqlCommandBuilder();
            menuStrip1 = new MenuStrip();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            fontSizeToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            openManualToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)QuestionsDataGrid).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // QuestionsDataGrid
            // 
            QuestionsDataGrid.AllowUserToAddRows = false;
            QuestionsDataGrid.AllowUserToDeleteRows = false;
            QuestionsDataGrid.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = Color.Gainsboro;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            QuestionsDataGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            QuestionsDataGrid.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            QuestionsDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            QuestionsDataGrid.BackgroundColor = SystemColors.Window;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(224, 224, 224);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 12F);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            QuestionsDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            QuestionsDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(224, 224, 224);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 12F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            QuestionsDataGrid.DefaultCellStyle = dataGridViewCellStyle3;
            QuestionsDataGrid.GridColor = SystemColors.InfoText;
            QuestionsDataGrid.Location = new Point(12, 53);
            QuestionsDataGrid.Margin = new Padding(3, 4, 3, 4);
            QuestionsDataGrid.Name = "QuestionsDataGrid";
            QuestionsDataGrid.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 12F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            QuestionsDataGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            QuestionsDataGrid.RowHeadersWidth = 35;
            dataGridViewCellStyle5.BackColor = Color.White;
            QuestionsDataGrid.RowsDefaultCellStyle = dataGridViewCellStyle5;
            QuestionsDataGrid.RowTemplate.Height = 33;
            QuestionsDataGrid.ScrollBars = ScrollBars.Vertical;
            QuestionsDataGrid.Size = new Size(1170, 527);
            QuestionsDataGrid.TabIndex = 9;
            QuestionsDataGrid.SelectionChanged += QuestionsDataGrid_SelectionChanged;
            // 
            // DeleteQuestionButton
            // 
            DeleteQuestionButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            DeleteQuestionButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            DeleteQuestionButton.Font = new Font("Segoe UI", 15F);
            DeleteQuestionButton.Location = new Point(999, 631);
            DeleteQuestionButton.Name = "DeleteQuestionButton";
            DeleteQuestionButton.Size = new Size(131, 65);
            DeleteQuestionButton.TabIndex = 7;
            DeleteQuestionButton.Text = "Delete";
            DeleteQuestionButton.UseVisualStyleBackColor = true;
            DeleteQuestionButton.Click += DeleteQuestionButton_Click;
            // 
            // EditQuestionButton
            // 
            EditQuestionButton.Anchor = AnchorStyles.Top;
            EditQuestionButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            EditQuestionButton.Font = new Font("Segoe UI", 15F);
            EditQuestionButton.Location = new Point(516, 631);
            EditQuestionButton.Name = "EditQuestionButton";
            EditQuestionButton.Size = new Size(131, 65);
            EditQuestionButton.TabIndex = 6;
            EditQuestionButton.Text = "Edit";
            EditQuestionButton.UseVisualStyleBackColor = true;
            EditQuestionButton.Click += EditQuestionButton_Click;
            // 
            // AddQuestionButton
            // 
            AddQuestionButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AddQuestionButton.Font = new Font("Segoe UI", 15F);
            AddQuestionButton.Location = new Point(57, 631);
            AddQuestionButton.Name = "AddQuestionButton";
            AddQuestionButton.Size = new Size(141, 65);
            AddQuestionButton.TabIndex = 5;
            AddQuestionButton.Text = "Add";
            AddQuestionButton.UseVisualStyleBackColor = true;
            AddQuestionButton.Click += AddQuestionButton_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.AutoSize = false;
            menuStrip1.BackColor = SystemColors.ButtonFace;
            menuStrip1.Font = new Font("Segoe UI", 10F);
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { optionsToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1194, 36);
            menuStrip1.TabIndex = 10;
            menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { fontSizeToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(84, 32);
            optionsToolStripMenuItem.Text = "Options";
            // 
            // fontSizeToolStripMenuItem
            // 
            fontSizeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toolStripMenuItem2, toolStripMenuItem3, toolStripMenuItem4 });
            fontSizeToolStripMenuItem.Name = "fontSizeToolStripMenuItem";
            fontSizeToolStripMenuItem.Size = new Size(161, 28);
            fontSizeToolStripMenuItem.Text = "Font size";
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(112, 28);
            toolStripMenuItem2.Text = "9";
            toolStripMenuItem2.Click += toolStripMenuItem2_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(112, 28);
            toolStripMenuItem3.Text = "12";
            toolStripMenuItem3.Click += toolStripMenuItem3_Click;
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(112, 28);
            toolStripMenuItem4.Text = "15";
            toolStripMenuItem4.Click += toolStripMenuItem4_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openManualToolStripMenuItem });
            helpToolStripMenuItem.Margin = new Padding(10, 0, 0, 0);
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(59, 32);
            helpToolStripMenuItem.Text = "Help";
            // 
            // openManualToolStripMenuItem
            // 
            openManualToolStripMenuItem.Name = "openManualToolStripMenuItem";
            openManualToolStripMenuItem.Size = new Size(198, 28);
            openManualToolStripMenuItem.Text = "Open manual";
            // 
            // MainScreen
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1194, 753);
            Controls.Add(DeleteQuestionButton);
            Controls.Add(QuestionsDataGrid);
            Controls.Add(EditQuestionButton);
            Controls.Add(AddQuestionButton);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(1000, 800);
            Name = "MainScreen";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Survey Configurator";
            WindowState = FormWindowState.Maximized;
            Load += MainScreen_Load;
            ((System.ComponentModel.ISupportInitialize)QuestionsDataGrid).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button AddQuestionButton;
        private Button DeleteQuestionButton;
        private Button EditQuestionButton;
        private DataGridView QuestionsDataGrid;
        private Microsoft.Data.SqlClient.SqlCommandBuilder sqlCommandBuilder1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem fontSizeToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem openManualToolStripMenuItem;
    }
}
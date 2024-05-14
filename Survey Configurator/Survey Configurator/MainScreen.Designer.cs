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
            DeleteQuestionButton = new Button();
            EditQuestionButton = new Button();
            AddQuestionButton = new Button();
            sqlCommandBuilder1 = new Microsoft.Data.SqlClient.SqlCommandBuilder();
            menuStrip1 = new MenuStrip();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            fontSizeToolStripMenuItem = new ToolStripMenuItem();
            fontSize9StripMenuItem = new ToolStripMenuItem();
            fontSize12StripMenuItem = new ToolStripMenuItem();
            fontSize15StripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            openManualToolStripMenuItem = new ToolStripMenuItem();
            QuestionsListView = new ListView();
            QuestionOrder = new ColumnHeader();
            QuestionText = new ColumnHeader();
            QuestionType = new ColumnHeader();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // DeleteQuestionButton
            // 
            DeleteQuestionButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            DeleteQuestionButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            DeleteQuestionButton.Font = new Font("Microsoft Sans Serif", 12F);
            DeleteQuestionButton.Location = new Point(1117, 673);
            DeleteQuestionButton.Name = "DeleteQuestionButton";
            DeleteQuestionButton.Size = new Size(120, 40);
            DeleteQuestionButton.TabIndex = 4;
            DeleteQuestionButton.Text = "Delete";
            DeleteQuestionButton.UseVisualStyleBackColor = true;
            DeleteQuestionButton.Click += DeleteQuestionButton_Click;
            // 
            // EditQuestionButton
            // 
            EditQuestionButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            EditQuestionButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            EditQuestionButton.Font = new Font("Microsoft Sans Serif", 12F);
            EditQuestionButton.Location = new Point(990, 673);
            EditQuestionButton.Name = "EditQuestionButton";
            EditQuestionButton.Size = new Size(120, 40);
            EditQuestionButton.TabIndex = 3;
            EditQuestionButton.Text = "Edit";
            EditQuestionButton.UseVisualStyleBackColor = true;
            EditQuestionButton.Click += EditQuestionButton_Click;
            // 
            // AddQuestionButton
            // 
            AddQuestionButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            AddQuestionButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AddQuestionButton.Font = new Font("Microsoft Sans Serif", 12F);
            AddQuestionButton.Location = new Point(863, 673);
            AddQuestionButton.Name = "AddQuestionButton";
            AddQuestionButton.Size = new Size(120, 40);
            AddQuestionButton.TabIndex = 2;
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
            menuStrip1.Size = new Size(1250, 36);
            menuStrip1.TabIndex = 5;
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
            fontSizeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { fontSize9StripMenuItem, fontSize12StripMenuItem, fontSize15StripMenuItem });
            fontSizeToolStripMenuItem.Name = "fontSizeToolStripMenuItem";
            fontSizeToolStripMenuItem.Size = new Size(161, 28);
            fontSizeToolStripMenuItem.Text = "Font size";
            // 
            // fontSize9StripMenuItem
            // 
            fontSize9StripMenuItem.Name = "fontSize9StripMenuItem";
            fontSize9StripMenuItem.Size = new Size(112, 28);
            fontSize9StripMenuItem.Text = "9";
            fontSize9StripMenuItem.Click += toolStripMenuItem2_Click;
            // 
            // fontSize12StripMenuItem
            // 
            fontSize12StripMenuItem.Name = "fontSize12StripMenuItem";
            fontSize12StripMenuItem.Size = new Size(112, 28);
            fontSize12StripMenuItem.Text = "12";
            fontSize12StripMenuItem.Click += toolStripMenuItem3_Click;
            // 
            // fontSize15StripMenuItem
            // 
            fontSize15StripMenuItem.Name = "fontSize15StripMenuItem";
            fontSize15StripMenuItem.Size = new Size(112, 28);
            fontSize15StripMenuItem.Text = "15";
            fontSize15StripMenuItem.Click += toolStripMenuItem4_Click;
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
            // QuestionsListView
            // 
            QuestionsListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            QuestionsListView.Columns.AddRange(new ColumnHeader[] { QuestionOrder, QuestionText, QuestionType });
            QuestionsListView.FullRowSelect = true;
            QuestionsListView.Location = new Point(12, 39);
            QuestionsListView.Name = "QuestionsListView";
            QuestionsListView.Size = new Size(1226, 574);
            QuestionsListView.Sorting = SortOrder.Ascending;
            QuestionsListView.TabIndex = 6;
            QuestionsListView.UseCompatibleStateImageBehavior = false;
            QuestionsListView.View = View.Details;
            // 
            // QuestionOrder
            // 
            QuestionOrder.Text = "Order";
            QuestionOrder.Width = 80;
            // 
            // QuestionText
            // 
            QuestionText.Text = "Text";
            QuestionText.Width = 300;
            // 
            // QuestionType
            // 
            QuestionType.Text = "Type";
            QuestionType.Width = 80;
            // 
            // MainScreen
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1250, 753);
            Controls.Add(QuestionsListView);
            Controls.Add(DeleteQuestionButton);
            Controls.Add(EditQuestionButton);
            Controls.Add(AddQuestionButton);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(1000, 800);
            Name = "MainScreen";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Survey Configurator";
            WindowState = FormWindowState.Maximized;
            FormClosing += MainScreen_FormClosing;
            Load += MainScreen_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button AddQuestionButton;
        private Button DeleteQuestionButton;
        private Button EditQuestionButton;
        private Microsoft.Data.SqlClient.SqlCommandBuilder sqlCommandBuilder1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem fontSizeToolStripMenuItem;
        private ToolStripMenuItem fontSize9StripMenuItem;
        private ToolStripMenuItem fontSize12StripMenuItem;
        private ToolStripMenuItem fontSize15StripMenuItem;
        private ToolStripMenuItem openManualToolStripMenuItem;
        private ListView QuestionsListView;
        private ColumnHeader QuestionOrder;
        private ColumnHeader QuestionText;
        private ColumnHeader QuestionType;
    }
}
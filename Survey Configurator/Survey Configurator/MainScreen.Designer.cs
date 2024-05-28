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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainScreen));
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
            languageToolStripMenuItem = new ToolStripMenuItem();
            EnglishToolStripMenuItem = new ToolStripMenuItem();
            ArabicToolStripMenuItem = new ToolStripMenuItem();
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
            resources.ApplyResources(DeleteQuestionButton, "DeleteQuestionButton");
            DeleteQuestionButton.Name = "DeleteQuestionButton";
            DeleteQuestionButton.UseVisualStyleBackColor = true;
            DeleteQuestionButton.Click += DeleteQuestionButton_Click;
            // 
            // EditQuestionButton
            // 
            resources.ApplyResources(EditQuestionButton, "EditQuestionButton");
            EditQuestionButton.Name = "EditQuestionButton";
            EditQuestionButton.UseVisualStyleBackColor = true;
            EditQuestionButton.Click += EditQuestionButton_Click;
            // 
            // AddQuestionButton
            // 
            resources.ApplyResources(AddQuestionButton, "AddQuestionButton");
            AddQuestionButton.Name = "AddQuestionButton";
            AddQuestionButton.UseVisualStyleBackColor = true;
            AddQuestionButton.Click += AddQuestionButton_Click;
            // 
            // menuStrip1
            // 
            resources.ApplyResources(menuStrip1, "menuStrip1");
            menuStrip1.BackColor = SystemColors.ButtonFace;
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { optionsToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Name = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            resources.ApplyResources(optionsToolStripMenuItem, "optionsToolStripMenuItem");
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { fontSizeToolStripMenuItem, languageToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            // 
            // fontSizeToolStripMenuItem
            // 
            resources.ApplyResources(fontSizeToolStripMenuItem, "fontSizeToolStripMenuItem");
            fontSizeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { fontSize9StripMenuItem, fontSize12StripMenuItem, fontSize15StripMenuItem });
            fontSizeToolStripMenuItem.Name = "fontSizeToolStripMenuItem";
            // 
            // fontSize9StripMenuItem
            // 
            resources.ApplyResources(fontSize9StripMenuItem, "fontSize9StripMenuItem");
            fontSize9StripMenuItem.Name = "fontSize9StripMenuItem";
            fontSize9StripMenuItem.Click += toolStripMenuItem2_Click;
            // 
            // fontSize12StripMenuItem
            // 
            resources.ApplyResources(fontSize12StripMenuItem, "fontSize12StripMenuItem");
            fontSize12StripMenuItem.Name = "fontSize12StripMenuItem";
            fontSize12StripMenuItem.Click += toolStripMenuItem3_Click;
            // 
            // fontSize15StripMenuItem
            // 
            resources.ApplyResources(fontSize15StripMenuItem, "fontSize15StripMenuItem");
            fontSize15StripMenuItem.Name = "fontSize15StripMenuItem";
            fontSize15StripMenuItem.Click += toolStripMenuItem4_Click;
            // 
            // languageToolStripMenuItem
            // 
            resources.ApplyResources(languageToolStripMenuItem, "languageToolStripMenuItem");
            languageToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { EnglishToolStripMenuItem, ArabicToolStripMenuItem });
            languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            // 
            // EnglishToolStripMenuItem
            // 
            resources.ApplyResources(EnglishToolStripMenuItem, "EnglishToolStripMenuItem");
            EnglishToolStripMenuItem.Name = "EnglishToolStripMenuItem";
            EnglishToolStripMenuItem.Click += EnglishToolStripMenuItem_Click;
            // 
            // ArabicToolStripMenuItem
            // 
            resources.ApplyResources(ArabicToolStripMenuItem, "ArabicToolStripMenuItem");
            ArabicToolStripMenuItem.Name = "ArabicToolStripMenuItem";
            ArabicToolStripMenuItem.Click += ArabicToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            resources.ApplyResources(helpToolStripMenuItem, "helpToolStripMenuItem");
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openManualToolStripMenuItem });
            helpToolStripMenuItem.Margin = new Padding(10, 0, 0, 0);
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            // 
            // openManualToolStripMenuItem
            // 
            resources.ApplyResources(openManualToolStripMenuItem, "openManualToolStripMenuItem");
            openManualToolStripMenuItem.Name = "openManualToolStripMenuItem";
            // 
            // QuestionsListView
            // 
            resources.ApplyResources(QuestionsListView, "QuestionsListView");
            QuestionsListView.Columns.AddRange(new ColumnHeader[] { QuestionOrder, QuestionText, QuestionType });
            QuestionsListView.FullRowSelect = true;
            QuestionsListView.Name = "QuestionsListView";
            QuestionsListView.Sorting = SortOrder.Ascending;
            QuestionsListView.UseCompatibleStateImageBehavior = false;
            QuestionsListView.View = View.Details;
            QuestionsListView.ColumnClick += QuestionsListView_ColumnClick;
            QuestionsListView.SelectedIndexChanged += QuestionsListView_SelectedIndexChanged;
            // 
            // QuestionOrder
            // 
            resources.ApplyResources(QuestionOrder, "QuestionOrder");
            // 
            // QuestionText
            // 
            resources.ApplyResources(QuestionText, "QuestionText");
            // 
            // QuestionType
            // 
            resources.ApplyResources(QuestionType, "QuestionType");
            // 
            // MainScreen
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(QuestionsListView);
            Controls.Add(DeleteQuestionButton);
            Controls.Add(EditQuestionButton);
            Controls.Add(AddQuestionButton);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MainScreen";
            WindowState = FormWindowState.Maximized;
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
        private ToolStripMenuItem languageToolStripMenuItem;
        private ToolStripMenuItem EnglishToolStripMenuItem;
        private ToolStripMenuItem ArabicToolStripMenuItem;
    }
}
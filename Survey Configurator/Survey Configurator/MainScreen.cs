using Survey_Configurator.Sub_forms;
using QuestionServices;
using SharedResources;
using SharedResources.models;
using System.Configuration;

namespace Survey_Configurator
{
    public partial class MainScreen : Form
    {

        /// <summary>
        /// This is the main class and the main window that will appear first thing when openning the app
        /// it contains all the event handlers and some class utility functions each will have explaination and summary.
        /// </summary>


        //the sorting order for the questions items in the list view
        private static SortOrder mSortingOrder = SortOrder.Ascending;
        //flag for disabling & enabling the UI elements
        private static bool mIsDatabaseConnected = true;

        //constants    
        private const string cEnglishLanguageSettings = "en";
        private const string cArabicLanguageSettings = "ar";
        private const string cLanguageSettingsKey = "Culture";

        /// <summary>
        /// constructor for the main form,first it sets the language of the app to what last saved in the app.config file,
        /// then it checks whether the connection string can obtained from the
        /// connectionString.json file and checks if the a connection to the database 
        /// can be created if either fails the application will ask show a form
        /// to ask for connection settings
        /// </summary>
        public MainScreen()
        {
            try
            {

                //set the language for the app
                SetAppLanguage();

                //check if connection string is successfully obtained 
                bool tConnectionStringExists = QuestionOperations.GetConnectionString();
                if (!tConnectionStringExists)
                {
                    //show connection settings form
                    ConnectionSettings tConnectionSettingsForm = new ConnectionSettings();
                    DialogResult tContinueToAppResult = tConnectionSettingsForm.ShowDialog();
                    //decied based on dialogue result
                    if (tContinueToAppResult == DialogResult.Cancel)
                    {
                        Close();
                    }
                    else if (tContinueToAppResult == DialogResult.Continue)
                    {
                        //user decieded to continue anyway regardless of not being able to connect to database
                        mIsDatabaseConnected = false;
                    }
                }
                else
                {
                    //test database connection
                    bool tIsDatabaseConnected = CheckDatabaseConnection();
                    if (!tIsDatabaseConnected)
                    {
                        DialogResult tContinueToAppResult = MessageBox.Show(GlobalStrings.DataBaseConnectionError, GlobalStrings.DataBaseConnectionErrortitle,
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (tContinueToAppResult == DialogResult.No || tContinueToAppResult == DialogResult.Cancel)
                        {
                            Close();
                        }
                        else
                        {
                            //disable functionalities
                            mIsDatabaseConnected = false;
                        }
                    }
                }

                InitializeComponent();
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                ShowDefaultErrorMessage();
                Close();
            }
        }


        /// <summary>
        /// This function initializes the ListView control with questions objects 
        /// and calls a function to start montitoring the database for changes,
        /// intializes all the event listeners and set the sorting order for the 
        /// ListView control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainScreen_Load(object sender, EventArgs e)
        {
            try
            {
                LoadApplication();
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                ShowDefaultErrorMessage();
                DisableUIElements();
            }
        }

        /// <summary>
        /// instantiate a form to add a new question object to the listView control
        /// if the database is not connected the form won't show at all,
        /// the disabling of the edit and delete button is because they
        /// gets automatically enabled after adding a question which causes problems
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddQuestionButton_Click(object sender, EventArgs e)
        {
            try
            {
                //check connection before showing the add question form
                OperationResult tIsDatabaseConnected = QuestionOperations.TestDBConnection();
                if (tIsDatabaseConnected.IsSuccess)
                {

                    AddEditQuestion tAddForm = new AddEditQuestion();
                    DialogResult tQuestionAdded = tAddForm.ShowDialog();

                    //disable delete and edit button
                    if (tQuestionAdded != DialogResult.Cancel)
                    {
                        DeleteQuestionButton.Enabled = false;
                        EditQuestionButton.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show(tIsDatabaseConnected.mErrorMessage, tIsDatabaseConnected.mError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                ShowDefaultErrorMessage();
            }
        }

        /// <summary>
        /// instantiate a form to edit an existing question object 
        /// have a similar structure to the AddQuestion function but this
        /// one gets the quesion Id which is stored in the Tag property of the
        /// selected ListView item which in turn contains the question object info
        /// the reason for disabling the edit and delete button is the same as the
        /// previous function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditQuestionButton_Click(object sender, EventArgs e)
        {
            try
            {
                EditQuestion();
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                ShowDefaultErrorMessage();
            }
        }

        /// <summary>
        /// delete a quesiton object, from the List view control and database
        /// and disables the edit and delete buttons for the same reason in the 
        /// addQuestion function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteQuestionButton_Click(object sender, EventArgs e)
        {
            try
            {
                int tNumberOfSelectedQuestions = QuestionsListView.SelectedItems.Count;

                DialogResult tDeleteQuestion = MessageBox.Show(GlobalStrings.DeleteQuestionConfirm, GlobalStrings.DeleteOperationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                //to prevent any interruption in deleting
                QuestionOperations.mOperationOngoing = true;
                if (tDeleteQuestion == DialogResult.Yes)
                {
                    List<Question> tSelectedQuestions = new List<Question>();
                    //obtain the selected questions and store them
                    for (int i = 0; i < tNumberOfSelectedQuestions; i++)
                    {
                        //cast the selected grid row to dataRowView to store it in a dataRow
                        Question tCurrentQuestion = QuestionsListView.SelectedItems[i].Tag as Question;
                        tSelectedQuestions.Add(tCurrentQuestion);
                    }

                    //check operation result here
                    OperationResult tDeleteQuestionResult = QuestionOperations.DeleteQuestion(tSelectedQuestions);
                    if (!tDeleteQuestionResult.IsSuccess)
                    {
                        MessageBox.Show(tDeleteQuestionResult.mErrorMessage, tDeleteQuestionResult.mError.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        //disable delete and edit button
                        DeleteQuestionButton.Enabled = false;
                        EditQuestionButton.Enabled = false;

                        MessageBox.Show(GlobalStrings.OperationSuccessful, GlobalStrings.OperationSuccessfulTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                MessageBox.Show(GlobalStrings.OperationError, GlobalStrings.OperationErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                QuestionOperations.mOperationOngoing = false;
            }
        }

        /// <summary>
        /// this functions enables and disables the edit and delete buttons 
        /// whenever the selected question changes in the ListView control
        /// as the user can't edit more than one question at a time
        /// but delete as many questions as the user wants
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuestionsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int tNumberOfSelectedQuestions = QuestionsListView.SelectedItems.Count;
                //disable delete button if no questions are selected
                if (tNumberOfSelectedQuestions > 0)
                {
                    DeleteQuestionButton.Enabled = true;
                }
                else
                {
                    DeleteQuestionButton.Enabled = false;
                }

                //enable the edit questions only if one question is selected
                if (tNumberOfSelectedQuestions > 0 && tNumberOfSelectedQuestions < 2)
                {
                    EditQuestionButton.Enabled = true;
                }
                else
                {
                    EditQuestionButton.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                ShowDefaultErrorMessage();
            }
        }

        /// <summary>
        /// sorts the questions ListView when any column header is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuestionsListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                if (mSortingOrder == SortOrder.Ascending)
                {
                    mSortingOrder = SortOrder.Descending;
                }
                else
                {
                    mSortingOrder = SortOrder.Ascending;
                }
                //change the itemSorter of the listview based on what column is clicked
                QuestionsListView.ListViewItemSorter = new ListViewItemComparer(e.Column, mSortingOrder);
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                ShowDefaultErrorMessage();
            }
        }

        /// <summary>
        /// called on any database change from any source
        /// and update the viewList control with the updated data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuestionOperations_DataBaseChangedEvent(object? sender, EventArgs e)
        {
            try
            {
                UpdateQuestionsList();
                EditQuestionButton.Enabled = false;
                DeleteQuestionButton.Enabled = false;
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                ShowDefaultErrorMessage();
                DisableUIElements();
            }
        }

        /// <summary>
        /// called when a connection with the data base cannot be
        /// established for at least 3 times to disable the UI elements in the app
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuestionOperations_DataBaseNotConnectedEvent(object? sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(GlobalStrings.DatabaseNotConnectedError, GlobalStrings.DatabaseNotConnectedErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                DisableUIElements();
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                ShowDefaultErrorMessage();
            }
        }

        /// <summary>
        /// the reason for removing these lines from the EditButtonClick
        /// event handler is to be able to use them in for the event of double
        /// clicking a question
        /// </summary>
        private void EditQuestion()
        {
            try
            {
                OperationResult tIsDatabaseConnected = QuestionOperations.TestDBConnection();
                if (tIsDatabaseConnected.IsSuccess)
                {
                    Question tSelectedQuestion = QuestionsListView.SelectedItems[0].Tag as Question;
                    AddEditQuestion tAddForm = new AddEditQuestion(tSelectedQuestion.Id);
                    DialogResult tQuestionEdited = tAddForm.ShowDialog();

                    //disable delete and edit button
                    if (tQuestionEdited != DialogResult.Cancel)
                    {
                        DeleteQuestionButton.Enabled = false;
                        EditQuestionButton.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show(tIsDatabaseConnected.mErrorMessage, tIsDatabaseConnected.mError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                ShowDefaultErrorMessage();
            }
        }

        /// <summary>
        /// this function shows the edit form whenever the user
        /// double-clicks a question in the ListView cotrol
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuestionsListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo tClickedQuestionInfo = QuestionsListView.HitTest(e.X, e.Y);
            ListViewItem tSelectedQuestion = tClickedQuestionInfo.Item;

            //check if the user clicked on an empty space in the listview control
            if (tSelectedQuestion != null)
            {
                EditQuestion();
            }
        }

        #region menu strip items functions
        /// <summary>
        /// these fucntions are concerned with the toolStrip menu
        /// options
        /// </summary>

        /// <summary>
        /// these fucntions are concerned with the font change options
        /// </summary>
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                ClearFontSelectedOptions();
                fontSize9StripMenuItem.Checked = true;
                QuestionsListView.Font = new Font(QuestionsListView.Font.FontFamily, 9);
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                ShowDefaultErrorMessage();
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                ClearFontSelectedOptions();
                fontSize12StripMenuItem.Checked = true;
                QuestionsListView.Font = new Font(QuestionsListView.Font.FontFamily, 12);
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                ShowDefaultErrorMessage();
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            try
            {
                ClearFontSelectedOptions();
                fontSize15StripMenuItem.Checked = true;
                QuestionsListView.Font = new Font(QuestionsListView.Font.FontFamily, 15);
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
            }
        }

        private void ClearFontSelectedOptions()
        {
            try
            {
                fontSize9StripMenuItem.Checked = false;
                fontSize12StripMenuItem.Checked = false;
                fontSize15StripMenuItem.Checked = false;
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                ShowDefaultErrorMessage();
            }
        }

        /// <summary>
        /// these functions are conecerned with the language change options
        /// the functions restarts the app if the user changes the language
        /// to a one that is different languge
        /// </summary>

        private void EnglishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string tAppLanguage = ConfigurationManager.AppSettings[cLanguageSettingsKey];
                if (tAppLanguage != cEnglishLanguageSettings)
                {

                    ChangeAppLanguage(cEnglishLanguageSettings);
                    Application.Restart();
                }
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);

            }
        }

        private void ArabicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string tAppLanguage = ConfigurationManager.AppSettings[cLanguageSettingsKey];
                if (tAppLanguage != cArabicLanguageSettings)
                {
                    ChangeAppLanguage(cArabicLanguageSettings);
                    Application.Restart();
                }
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);

            }
        }

        /// <summary>
        /// this funciton shows the connection settings form for the 
        /// user to change the settings 
        /// </summary>
        private void ConnectionSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try 
            { 
                ConnectionSettings tConnectionSettingsForm = new ConnectionSettings(true);
                DialogResult tContinueToAppResult = tConnectionSettingsForm.ShowDialog();
                //decied based on dialogue result
                if (tContinueToAppResult == DialogResult.Continue)
                {
                    //user decieded to continue anyway
                    mIsDatabaseConnected = false;
                    DisableUIElements();
                }
                else if (tContinueToAppResult == DialogResult.OK)
                {
                    mIsDatabaseConnected= true;
                    LoadApplication();
                    EnableUIElements();

                }
            }
            catch(Exception ex)
            {
                UtilityMethods.LogError(ex);
                ShowDefaultErrorMessage();
                DisableUIElements();

            }
        }
        #endregion

        #region class utility functions

        /// <summary>
        /// initailaize the List view control with questions objects through the UpdateQuestionFunction
        /// if the initialization fails for any reason the application will show an error message and 
        /// close the app
        /// </summary>
        private void QuestionsListViewInit()
        {
            try
            {
                OperationResult tGetQuestionsSuccessful = QuestionOperations.GetQuestions();
                if (!tGetQuestionsSuccessful.IsSuccess)
                {
                    MessageBox.Show(tGetQuestionsSuccessful.mErrorMessage, tGetQuestionsSuccessful.mError.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DisableUIElements();
                }
                else
                {
                    UpdateQuestionsList();
                }
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                ShowDefaultErrorMessage();
                DisableUIElements();
            }
        }

        /// <summary>
        /// this funciton is responsible for populating the ListView control with 
        /// Questions Data
        /// </summary>
        private void UpdateQuestionsList()
        {
            try
            {
                //check if there was any questions selected before updating the view list data on database update
                //to keep them selected after any change happening to the database
                int[] tSelectedQuestions = new int[QuestionsListView.SelectedItems.Count];
                for (int i = 0; i < tSelectedQuestions.Length; i++)
                {
                    Question tCurrentQuestion = (Question)QuestionsListView.SelectedItems[i].Tag;
                    tSelectedQuestions[i] = tCurrentQuestion.Id;
                }

                //re-populate database
                QuestionsListView.Items.Clear();
                foreach (Question tQuestion in QuestionOperations.mQuestionsList)
                {
                    //foreach question add its info to a listViewItem and also to the listViewItem.tag property 
                    // to be able to access that info later on
                    string[] tCurrentQuestionData = new[] { tQuestion.Order.ToString(), tQuestion.Text, tQuestion.Type.ToString() };
                    ListViewItem tCurrentQuestionItem = new ListViewItem(tCurrentQuestionData);
                    tCurrentQuestionItem.Tag = tQuestion;

                    //selectes the questions if it was selected before the ListView data was changed
                    //if a question was selected before re populating the data
                    if (tSelectedQuestions.Contains(tQuestion.Id))
                    {
                        tCurrentQuestionItem.Selected = true;
                    }
                    //add question to the listview control
                    QuestionsListView.Items.Add(tCurrentQuestionItem);
                }
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                MessageBox.Show(GlobalStrings.DataFetchingError, GlobalStrings.DataFetchingErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                DisableUIElements();
            }
        }

        /// <summary>
        /// a default message error for any unknown error
        /// </summary>
        public static void ShowDefaultErrorMessage()
        {
            try
            {
                MessageBox.Show(GlobalStrings.UnknownError, GlobalStrings.UnknownErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                ShowDefaultErrorMessage();
            }
        }

        /// <summary>
        /// sets the language and cultrue for the application from 
        /// the language stored in the app config in case of any error
        /// sets the language to english;
        /// </summary>
        private static void SetAppLanguage()
        {
            try
            {
                //read language value settings from app config
                string tAppLanguage = ConfigurationManager.AppSettings[cLanguageSettingsKey];
                if (tAppLanguage == null)
                {
                    tAppLanguage = cEnglishLanguageSettings;
                }
                //set app language
                Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo(tAppLanguage);
                Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(tAppLanguage);
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo(cEnglishLanguageSettings);
                Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(cEnglishLanguageSettings);
            }

        }

        /// <summary>
        /// change the language of the application
        /// </summary>
        private static void ChangeAppLanguage(string pLanguage)
        {
            try
            {
                //change the language settings value in app config
                Configuration configfile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configfile.AppSettings.Settings[cLanguageSettingsKey].Value = pLanguage;
                configfile.Save();
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
            }
        }

        /// <summary>
        /// disables all UI elements excep the toolstripMenu
        /// and shows the user a note that the app is not working
        /// normally
        /// </summary>
        private void DisableUIElements()
        {
            try
            {
                QuestionsListView.Enabled = false;
                AddQuestionButton.Enabled = false;
                EditQuestionButton.Enabled = false;
                DeleteQuestionButton.Enabled = false;
                DatabaseConnectionIsseuesLabel.Show();
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                ShowDefaultErrorMessage();
            }
        }

        private void EnableUIElements()
        {
            try
            {
                QuestionsListView.Enabled = true;
                AddQuestionButton.Enabled = true;
                DatabaseConnectionIsseuesLabel.Hide();
                if (QuestionsListView.SelectedItems.Count > 0)
                {
                    DeleteQuestionButton.Enabled = false;
                }

                if (QuestionsListView.SelectedItems.Count > 0 && QuestionsListView.SelectedItems.Count < 2)
                {
                    EditQuestionButton.Enabled = false;

                }

            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                ShowDefaultErrorMessage();
            }
        }

        /// <summary>
        /// check database connection
        /// </summary>
        /// <returns>bool value indicating whether the connection succeded or not</returns>
        public static bool CheckDatabaseConnection()
        {
            try 
            {
                OperationResult tDatabaseConnected = QuestionOperations.TestDBConnection();
                return tDatabaseConnected.IsSuccess;
            }
            catch(Exception ex)
            {
                UtilityMethods.LogError(ex);
                ShowDefaultErrorMessage();
                return false;
            }
        }

        /// <summary>
        /// loads the data into the application in the case that
        /// the database is connected, else all the UI elements gets disabled,
        /// the reason for seperating these operations is to reuse in case the 
        /// user decides to change the connection settings inside the app
        /// </summary>
        private void LoadApplication()
        {
            try 
            { 
                if (mIsDatabaseConnected)
                {
                    //initialize the list view with questions data
                    QuestionsListViewInit();

                    //launch the database change checker to monitor database for any change and reflect it to the UI
                    QuestionOperations.StartCheckingDataBaseChange();

                }
                else
                {
                    DisableUIElements();
                }

                //listen to any database change event
                QuestionOperations.DataBaseChangedEvent += QuestionOperations_DataBaseChangedEvent;

                //listener for the event of database refusing to connect multiple times
                QuestionOperations.DataBaseNotConnectedEvent += QuestionOperations_DataBaseNotConnectedEvent;

                //sort the questions list alphabetically on first load
                QuestionsListView.ListViewItemSorter = new ListViewItemComparer(1, mSortingOrder);
            }
            catch(Exception ex)
            {
                UtilityMethods.LogError(ex);
                ShowDefaultErrorMessage();
            }
        }
        #endregion

    }
}

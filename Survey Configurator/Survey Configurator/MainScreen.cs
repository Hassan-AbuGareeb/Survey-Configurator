using Survey_Configurator.Sub_forms;
using QuestionServices;
using SharedResources;
using SharedResources.models;

namespace Survey_Configurator
{
    public partial class MainScreen : Form
    {

        /// <summary>
        /// This is the main class and the main window that will appear first thing when openning the app
        /// it contains all the event handlers and some class utility functions each will have explaination and summary.
        /// </summary>


        //the sorting order for the questions items in the list view
        private SortOrder SortingOrder = SortOrder.Ascending;


        /// <summary>
        /// constructor for the main form, it checks whether the connection string can obtained from the
        /// connectionString.json file and checks if the a connection to the database can be created if
        /// either fails the application closes immediatly
        /// </summary>
        public MainScreen()
        {
            try {
                //check if connection string is successfully obtained 
                OperationResult tConnectionStringCreated = QuestionOperations.SetConnectionString();
                if(!tConnectionStringCreated.IsSuccess)
                {
                    MessageBox.Show(SharedData.ErrorMessages[ErrorTypes.SqlError], $"{ErrorTypes.SqlError} error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
                //check database connectivity
                OperationResult tDatabaseConnected = QuestionOperations.TestDBConnection();
                if(!tDatabaseConnected.IsSuccess )
                {
                    MessageBox.Show(SharedData.ErrorMessages[ErrorTypes.SqlError], $"{ErrorTypes.SqlError} error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }

                InitializeComponent();
            }
            catch(Exception ex)
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
                //initialize the list view with questions data
                QuestionsListViewInit();

                //launch the database change checker to monitor database for any change and reflect it to the UI
                OperationResult tStartDatabaseCheckResult = QuestionOperations.StartCheckingDataBaseChange();
                if(!tStartDatabaseCheckResult.IsSuccess)
                {
                    MessageBox.Show(SharedData.ErrorMessages[ErrorTypes.SqlError], $"{ErrorTypes.SqlError} error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
                //listen to any database change event
                QuestionOperations.DataBaseChangedEvent += QuestionOperations_DataBaseChangedEvent;

                //listener for the event of database refusing to connect multiple times
                QuestionOperations.DataBaseNotConnectedEvent += QuestionOperations_DataBaseNotConnectedEvent;
                
                //sort the questions list alphabetically on first load
                QuestionsListView.ListViewItemSorter = new ListViewItemComparer(1, SortingOrder);
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                MessageBox.Show(SharedData.ErrorMessages[ErrorTypes.UnknownError], $"{ErrorTypes.UnknownError} error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
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
                if(tIsDatabaseConnected.IsSuccess) { 

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
                    MessageBox.Show(SharedData.ErrorMessages[ErrorTypes.SqlError], $"{ErrorTypes.SqlError} error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch(Exception ex)
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
            try {
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
                    MessageBox.Show(SharedData.ErrorMessages[ErrorTypes.SqlError], $"{ErrorTypes.SqlError} error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
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
                
                DialogResult tDeleteQuestion = MessageBox.Show($"Are you sure you want to delete {(tNumberOfSelectedQuestions > 1 ? "these " : "this ")}question{(tNumberOfSelectedQuestions > 1 ? "s" : "")}?", "Delete question", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                
                //to prevent any interruption in deleting
                QuestionOperations.OperationOngoing = true;
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
                    if(!tDeleteQuestionResult.IsSuccess) 
                    {
                        MessageBox.Show(tDeleteQuestionResult.ErrorMessage, tDeleteQuestionResult.Error.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        //disable delete and edit button
                        DeleteQuestionButton.Enabled = false;
                        EditQuestionButton.Enabled = false;

                        MessageBox.Show($"Question{(tNumberOfSelectedQuestions > 1 ? "s " : " ")}deleted successfully!", "Operation successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                MessageBox.Show(SharedData.ErrorMessages[ErrorTypes.OperationError], $"{ErrorTypes.OperationError} error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                QuestionOperations.OperationOngoing = false;
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
            }catch (Exception ex)
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
                if (SortingOrder == SortOrder.Ascending)
                {
                    SortingOrder = SortOrder.Descending;
                }
                else
                {
                    SortingOrder = SortOrder.Ascending;
                }
                //change the itemSorter of the listview based on what column is clicked
                QuestionsListView.ListViewItemSorter = new ListViewItemComparer(e.Column, SortingOrder);
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
            }
        }

        /// <summary>
        /// called when a connection with the data base cannot be
        /// established for at least 3 times to terminate the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuestionOperations_DataBaseNotConnectedEvent(object? sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(SharedData.ErrorMessages[ErrorTypes.SqlError], $"{ErrorTypes.SqlError} error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                ShowDefaultErrorMessage();
            }
        }

        #region menu strip items functions
        /// <summary>
        /// these fucntions are concerned with the toolStrip menu
        /// and changning the font size of the ListView control only
        /// 
        /// </summary>

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try 
            { 
                ClearSelectedOptions();
                fontSize9StripMenuItem.Checked = true;
                QuestionsListView.Font = new Font(QuestionsListView.Font.FontFamily, 9);
            }
            catch(Exception ex) 
            { 
                UtilityMethods.LogError(ex);
                ShowDefaultErrorMessage();
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try 
            { 
                ClearSelectedOptions();
                fontSize12StripMenuItem.Checked = true;
                QuestionsListView.Font = new Font(QuestionsListView.Font.FontFamily, 12);
            }
            catch(Exception ex)
            {
                UtilityMethods.LogError(ex);
                ShowDefaultErrorMessage();
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            try 
            { 
                ClearSelectedOptions();
                fontSize15StripMenuItem.Checked = true;
                QuestionsListView.Font = new Font(QuestionsListView.Font.FontFamily, 15);
            }catch(Exception ex)
            {
                UtilityMethods.LogError(ex);
            }
          }

        private void ClearSelectedOptions()
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
                    MessageBox.Show(tGetQuestionsSuccessful.ErrorMessage, tGetQuestionsSuccessful.Error.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
                else
                {
                    UpdateQuestionsList();
                }
            }
            catch(Exception ex)
            {
                UtilityMethods.LogError(ex);
                ShowDefaultErrorMessage();
                Close();
            }
        }

        /// <summary>
        /// this funciton is responsible for populating the ListView control with 
        /// Questions Data
        /// </summary>
        private void UpdateQuestionsList()
        {
            try { 
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
                foreach (Question tQuestion in QuestionOperations.QuestionsList)
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
            }catch(Exception ex) 
            { 
                UtilityMethods.LogError(ex);
                MessageBox.Show(SharedData.ErrorMessages[ErrorTypes.DataFetchingError], $"{ErrorTypes.DataFetchingError} error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// a default message error for any unknown error
        /// </summary>
        private static void ShowDefaultErrorMessage()
        {
            try
            {
                MessageBox.Show(SharedData.ErrorMessages[ErrorTypes.UnknownError], $"{ErrorTypes.UnknownError} error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                ShowDefaultErrorMessage();
            }
        }

        #endregion
    }
}

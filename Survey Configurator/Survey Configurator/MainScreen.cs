using Survey_Configurator.Sub_forms;
using System.Data;
using QuestionServices;
using System.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.Logging;
using SharedResources;
using SharedResources.models;
namespace Survey_Configurator
{
    public partial class MainScreen : Form
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        private void MainScreen_Load(object sender, EventArgs e)
        {
            try
            {
                //either find a way to test connection before showing the main form or move this function from here 
                bool tIsConnectionStringFound = QuestionOperations.SetConnectionString();

                //initialize the list view with questions data
                QuestionsListViewInit();

                //launch the database change checker to monitor database for any change and reflect it to the UI
                QuestionOperations.CheckDataBaseChange();

            }
            catch (ArgumentException)
            {
                MessageBox.Show("Wrong connection parameters please check the connectionSettings.txt file");
                Close();
            }
            catch (InvalidOperationException)
            {
                //error during getting data from database, implement a retry mechanism
                MessageBox.Show("error occured while Loading data, please try again");
            }
            catch (SqlException)
            {
                MessageBox.Show("Database connection error, check the connection parameters or the sql server configurations");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.GetType().FullName}, {ex.StackTrace}");
                Close();
            }
        }

        private void MainScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            //stop the function checking the database changes
            QuestionOperations.IsAppRunning = false;
        }

        //buttons click functions
        private void AddQuestionButton_Click(object sender, EventArgs e)
        {
            AddEditQuestion addForm = new AddEditQuestion();
            addForm.ShowDialog();
            UpdateQuestionsList();
        }

        private void EditQuestionButton_Click(object sender, EventArgs e)
        {
            Question tSelectedQuestion = QuestionsListView.SelectedItems[0].Tag as Question;
            AddEditQuestion addForm = new AddEditQuestion(tSelectedQuestion.Id);
            addForm.ShowDialog();
            UpdateQuestionsList();
        }

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
                    Question[] tSelectedQuestions = new Question[tNumberOfSelectedQuestions];
                    //obtain the selected questions and store them
                    for (int i = 0; i < tNumberOfSelectedQuestions; i++)
                    {
                        //cast the selected grid row to dataRowView to store it in a dataRow
                        Question tCurrentQuestion = QuestionsListView.SelectedItems[i].Tag as Question;
                        tSelectedQuestions[i] = tCurrentQuestion;
                    }
                    QuestionOperations.DeleteQuestion(tSelectedQuestions);

                    //Update UI
                    UpdateQuestionsList();
                    
                    MessageBox.Show($"Question{(tNumberOfSelectedQuestions > 1 ? "s " : " ")}deleted successfully!", "Operation successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                UtilityMethods.LogError(ex);
                MessageBox.Show("Please select the entire row to delete a question");
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                MessageBox.Show("An unexpected error occured");
            }
            finally
            {
                QuestionOperations.OperationOngoing = false;
            }
        }

        private void QuestionsListView_SelectedIndexChanged(object sender, EventArgs e)
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

        #region menu strip items functions
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //ClearSelectedOptions();

            //fontSize9StripMenuItem.Checked = true;
            //QuestionsDataGrid.RowsDefaultCellStyle.Font = new Font(QuestionsDataGrid.Font.FontFamily, 9);
            //for (int i = 0; i < QuestionsDataGrid.Rows.Count; i++)
            //{
            //    QuestionsDataGrid.Rows[i].Height = 29;
            //}
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            //ClearSelectedOptions();
            //fontSize12StripMenuItem.Checked = true;
            //QuestionsDataGrid.RowsDefaultCellStyle.Font = new Font(QuestionsDataGrid.Font.FontFamily, 12);
            //for (int i = 0; i < QuestionsDataGrid.Rows.Count; i++)
            //{
            //    QuestionsDataGrid.Rows[i].Height = 33;

            //}
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            //ClearSelectedOptions();
            //fontSize15StripMenuItem.Checked = true;
            //QuestionsDataGrid.RowsDefaultCellStyle.Font = new Font(QuestionsDataGrid.Font.FontFamily, 15);
            //for (int i = 0; i < QuestionsDataGrid.Rows.Count; i++)
            //{
            //    QuestionsDataGrid.Rows[i].Height = 39;

            //}
        }
        private void ClearSelectedOptions()
        {
            //fontSize9StripMenuItem.Checked = false;
            //fontSize12StripMenuItem.Checked = false;
            //fontSize15StripMenuItem.Checked = false;
        }
        #endregion


        #region class utility functions
        private void QuestionsListViewInit()
        {
            QuestionOperations.GetQuestions();
            UpdateQuestionsList();
            //try and catch
        }
        
        private void UpdateQuestionsList()
        {
            QuestionsListView.Items.Clear();
            foreach (Question question in QuestionOperations.QuestionsList)
            {
                string[] tCurrentQuestionData = new[] { question.Order.ToString(), question.Text, question.Type.ToString() };
                ListViewItem tCurrentQuestionItem = new ListViewItem(tCurrentQuestionData);
                tCurrentQuestionItem.Tag = question;
                QuestionsListView.Items.Add(tCurrentQuestionItem);
            }
        }
        #endregion

        
    }
}

using Survey_Configurator.Sub_forms;
using System.Data;
using Logic;
using System.Configuration;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.Logging;
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
                //get a default connection string stored in the app.config file
                string defaultConnectionString = ConfigurationManager.ConnectionStrings["app"].ConnectionString;
                bool isConnectionStringFound = QuestionOperations.SetConnectionString(defaultConnectionString);

                //if the connection string isn't found for any reason the function used above will
                //automatically try to use the connection string stored in the app.config file
                if (!isConnectionStringFound )
                {
                    MessageBox.Show("File issue occured, please check your permission on creating and editing files");
                    MessageBox.Show("Trying to connect using default connection parameters");
                }

                //notify the logic layer to fetch the questions data from the database
                QuestionOperations.GetQuestions();

                QuestionsDataGrid.DataSource = QuestionOperations.Questions;

                //launch the database change checker to monitor database for any change and reflect it to the UI
                QuestionOperations.CheckDataBaseChange();

                //hide the question id column
                QuestionsDataGrid.Columns["Q_id"].Visible = false;

                //properly naming the columns in the datagrid view
                QuestionsDataGrid.Columns["Q_order"].HeaderText = "Order";
                QuestionsDataGrid.Columns["Q_text"].HeaderText = "Text";
                QuestionsDataGrid.Columns["Q_type"].HeaderText = "Type";

                //change the order of the columns in the grid view
                QuestionsDataGrid.Columns["Q_order"].DisplayIndex = 0;
                QuestionsDataGrid.Columns["Q_text"].DisplayIndex = 1;
                QuestionsDataGrid.Columns["Q_type"].DisplayIndex = 2;

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
        }

        private void EditQuestionButton_Click(object sender, EventArgs e)
        {
            //get the selected question id 
            int QuesitonId = (int)QuestionsDataGrid.SelectedRows[0].Cells["Q_id"].Value;

            AddEditQuestion addForm = new AddEditQuestion(QuesitonId);
            addForm.ShowDialog();
        }

        private void DeleteQuestionButton_Click(object sender, EventArgs e)
        {
            try
            {
                int numberOfSelectedRows = QuestionsDataGrid.SelectedRows.Count;
                DialogResult DeleteQuestion = MessageBox.Show($"Are you sure you want to delete {(numberOfSelectedRows > 1 ? "these " : "this ")}question{(numberOfSelectedRows > 1 ? "s" : "")}?", "Delete question", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                //to prevent any interruption in deleting
                QuestionOperations.OperationOngoing = true;
                if (DeleteQuestion == DialogResult.Yes)
                {
                    DataRow[] selectedQuestions = new DataRow[numberOfSelectedRows];
                    //obtain the selected questions and store them
                    for (int i = 0; i < numberOfSelectedRows; i++)
                    {
                        //cast the selected grid row to dataRowView to store it in a dataRow
                        DataRow currentQuestion = ((DataRowView)QuestionsDataGrid.SelectedRows[i].DataBoundItem).Row;
                        selectedQuestions[i] = currentQuestion;
                    }
                    //delete the questions from database and ui
                    QuestionOperations.DeleteQuestion(selectedQuestions);
                    MessageBox.Show($"Question{(numberOfSelectedRows > 1 ? "s " : " ")}deleted successfully!", "Operation successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(IndexOutOfRangeException ex)
            {
                QuestionOperations.LogError(ex);
                MessageBox.Show("Please select the entire row to delete a question");
            }
            catch (Exception ex)
            {
                QuestionOperations.LogError(ex);
                MessageBox.Show("An unexpected error occured");
            }
            finally
            {
                QuestionOperations.OperationOngoing = false;
            }
        }


        private void QuestionsDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            int numberOfSelectedQuestions = QuestionsDataGrid.SelectedRows.Count;
            //disable delete button if no questions are selected
            if (numberOfSelectedQuestions > 0)
            {
                DeleteQuestionButton.Enabled = true;
            }
            else
            {
                DeleteQuestionButton.Enabled = false;
            }

            //enable the edit questions only if one question is selected
            if (numberOfSelectedQuestions > 0 && numberOfSelectedQuestions < 2)
            {
                EditQuestionButton.Enabled = true;
            }
            else
            {
                EditQuestionButton.Enabled = false;
            }
        }

        //menu strip items functions
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ClearSelectedOptions();

            fontSize9StripMenuItem.Checked = true;
            QuestionsDataGrid.RowsDefaultCellStyle.Font = new Font(QuestionsDataGrid.Font.FontFamily, 9);
            for (int i = 0; i < QuestionsDataGrid.Rows.Count; i++)
            {
                QuestionsDataGrid.Rows[i].Height = 29;
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ClearSelectedOptions();
            fontSize12StripMenuItem.Checked = true;
            QuestionsDataGrid.RowsDefaultCellStyle.Font = new Font(QuestionsDataGrid.Font.FontFamily, 12);
            for (int i = 0; i < QuestionsDataGrid.Rows.Count; i++)
            {
                QuestionsDataGrid.Rows[i].Height = 33;

            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            ClearSelectedOptions();
            fontSize15StripMenuItem.Checked = true;
            QuestionsDataGrid.RowsDefaultCellStyle.Font = new Font(QuestionsDataGrid.Font.FontFamily, 15);
            for (int i = 0; i < QuestionsDataGrid.Rows.Count; i++)
            {
                QuestionsDataGrid.Rows[i].Height = 39;

            }
        }
        private void ClearSelectedOptions()
        {
            fontSize9StripMenuItem.Checked = false;
            fontSize12StripMenuItem.Checked = false;
            fontSize15StripMenuItem.Checked = false;
        }

    }
}

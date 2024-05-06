using Survey_Configurator.Sub_forms;
using System.Data;
using Logic;
using System.Configuration;
using Microsoft.Data.SqlClient;
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
                //get the connection string from file
                string defaultConnectionString = ConfigurationManager.ConnectionStrings["app"].ConnectionString;
                string connectionStringObtained =QuestionOperations.SetConnectionString(defaultConnectionString);
                if (connectionStringObtained != "success")
                {
                    MessageBox.Show("File issue occured, please check your permission on creating and editing files");
                    MessageBox.Show("Trying to connect using default connection parameters");
                }
                //get the questions table from the controller and bind it to the datagrid
                            //danger
                QuestionsDataGrid.DataSource = QuestionOperations.GetQuestions();
                //hide the question id column
                            //danger
                QuestionsDataGrid.Columns["Q_id"].Visible = false;
            }catch(ArgumentException)
            {
                MessageBox.Show("Wrong connection parameters please check the connectionSettings.txt file");
                Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Database connection error, check the connection parameters or the sql server configurations");
                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"{ex.GetType().FullName}, {ex.StackTrace}");
                Close();
            }
        }

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
            int numberOfSelectedRows = QuestionsDataGrid.SelectedRows.Count;
            DialogResult DeleteQuestion = MessageBox.Show($"Are you sure you want to delete {(numberOfSelectedRows > 1 ? "these " : "this ")}question{(numberOfSelectedRows > 1 ? "s" : "")}?", "Delete question", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

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
                        //danger
                QuestionOperations.DeleteQuestion(selectedQuestions);
                MessageBox.Show($"Question{(numberOfSelectedRows > 1 ? "s " : " ")}deleted successfully!", "Operation successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            if(numberOfSelectedQuestions > 0 && numberOfSelectedQuestions < 2)
            {
                EditQuestionButton.Enabled = true;
            }
            else
            {
                EditQuestionButton.Enabled = false;
            }
        }

    }
}

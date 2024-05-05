using Survey_Configurator.Sub_forms;
using System.Data;
using Logic;
using System.Configuration;
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
            //initialize the connection string
            QuestionOperations.setConnectionString(getConnectionString());
            //call the controller to obtain data from the db and populate the list of questions
            var questions = QuestionOperations.getQuestions();
            //bind the questions table to the datagrid
            QuestionsDataGrid.DataSource = questions;
            //hide the question id column
            QuestionsDataGrid.Columns["Q_id"].Visible = false;
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

        private string getConnectionString()
        {
            //try to obtain the connection string from a file
            //check if the file exists at all
            //get connection string and assign it to the QuestionsOperations property

            //check that file exists
            string filePath = Directory.GetCurrentDirectory() + "\\connectionSettings.txt";
            if (!File.Exists(filePath))
            {
                //create the file if it doesn't exist
                FileStream fs = File.Create(filePath);
                fs.Close();

                //add the default values to the file
                StreamWriter writer = new StreamWriter(filePath);
                writer.WriteLine("Server : HASSANABUGHREEB,\r\nDatabase : Questions_DB,\r\nTrusted_Connection : true,\r\nUser : ,\r\nPassword : ,\r\nEncrypt : false");
                writer.Close();
            }

            //read connection string values from file
            StreamReader sr = new StreamReader(filePath);
            string connectionString="";

            string[] connectionStringParameters = sr.ReadToEnd().Split(",");
            foreach (string parameter in connectionStringParameters)
            {
                string property = parameter.Split(":")[0].Trim();
                string value = parameter.Split(":")[1].Trim();
                connectionString += $"{property} = {value};\n";
            }
            return connectionString;
        }
    }
}

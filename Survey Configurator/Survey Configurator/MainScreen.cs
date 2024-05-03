using Survey_Configurator.Sub_forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logic;
using Database.models;

namespace Survey_Configurator
{
    public partial class MainScreen : Form
    {
        //create a list of type Question to hold questions data obtained from the controller

        public MainScreen()
        {
            InitializeComponent();
        }

        private void MainScreen_Load(object sender, EventArgs e)
        {
            //after obtaining the connection string 
            //call the controller to obtain data from the db and populate the list of questions
            var questions = QuestionOperations.getQuestions();
            QuestionsDataGrid.DataSource = questions;
            QuestionsDataGrid.Columns["Q_id"].Visible = false;
        }

        private void AddEditQuestionButton_Click(object sender, EventArgs e)
        {
            AddEditQuestion addForm = new AddEditQuestion(((sender as Button).Text));
            addForm.ShowDialog();
        }


        //disable the the delete button if non of the rows are selected

        private void DeleteQuestionButton_Click(object sender, EventArgs e)
        {
            //check first if any question is selected

            DialogResult DeleteQuestion = MessageBox.Show("Are you sure you want to delete this question?", "Delete question", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (DeleteQuestion == DialogResult.Yes)
            {
                //get the number of selected rows
                int numberOfSelectedRows = QuestionsDataGrid.SelectedRows.Count;
                DataRow[] selectedQuestions = new DataRow[numberOfSelectedRows];

                //obtain the selected questions
                for (int i = 0; i < numberOfSelectedRows; i++)
                {
                    DataRow currentQuestion = ((DataRowView)QuestionsDataGrid.Rows[i].DataBoundItem).Row;
                    selectedQuestions[i] = currentQuestion;
                }
                //delete the questions from db and ui
                QuestionOperations.DeleteQuestion(selectedQuestions);
                MessageBox.Show($"Question{(numberOfSelectedRows > 1 ? "s " : " ")}deleted successfully!", "Operation successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //QuestionsListBox.DisplayMember = "Q_text";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void QuestionsDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            //disable delete button if no questions are selected
            if(QuestionsDataGrid.SelectedRows.Count > 0)
            {
                DeleteQuestionButton.Enabled = true;
            }
            else
            {
                DeleteQuestionButton.Enabled = false;
            }
        }
    }
}

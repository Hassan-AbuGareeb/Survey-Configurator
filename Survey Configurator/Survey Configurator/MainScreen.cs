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
        }

        private void AddEditQuestionButton_Click(object sender, EventArgs e)
        {
            AddEditQuestion addForm = new AddEditQuestion(((sender as Button).Text));
            addForm.ShowDialog();
        }


        private void DeleteQuestionButton_Click(object sender, EventArgs e)
        {
            //check first if any question is selected

            DialogResult DeleteQuestion = MessageBox.Show("Are you sure you want to delete this question?", "Delete question", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (DeleteQuestion == DialogResult.Yes)
            {
                //delete the question from database
                //on deletion, delete the specific details row first then delete the question general info
                //when the above successful
                //show confirmation message
                //delete question from interface
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            //QuestionsListBox.DisplayMember = "Q_text";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}

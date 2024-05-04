﻿using Survey_Configurator.Sub_forms;
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

        private void AddQuestionButton_Click(object sender, EventArgs e)
        {
            //get the selected question id 
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
                //get the number of selected rows
                DataRow[] selectedQuestions = new DataRow[numberOfSelectedRows];

                //obtain the selected questions
                for (int i = 0; i < numberOfSelectedRows; i++)
                {
                    DataRow currentQuestion = ((DataRowView)QuestionsDataGrid.SelectedRows[i].DataBoundItem).Row;
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

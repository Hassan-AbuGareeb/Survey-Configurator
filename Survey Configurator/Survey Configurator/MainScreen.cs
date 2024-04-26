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

namespace Survey_Configurator
{
    public partial class MainScreen : Form
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        private void AddQuestionButton_Click(object sender, EventArgs e)
        {
            AddQuestion addForm = new AddQuestion();
            addForm.ShowDialog();
        }

        private void EditQuestionButton_Click(object sender, EventArgs e)
        {
            //check that only one question is selected
            EditQuestion editForm = new EditQuestion();
            editForm.ShowDialog();
        }

        private void DeleteQuestionButton_Click(object sender, EventArgs e)
        {
            //check first if any question is selected

            DialogResult DeleteQuestion = MessageBox.Show("Are you sure you want to delete this question?", "Delete question", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            var selectedQuestions =QuestionsListBox.SelectedItems;
            
            if(DeleteQuestion == DialogResult.Yes)
            {
                //delete the question from database
                //when the above successful
                //show confirmation message
                //delete question from interface
            }
        }
    }
}

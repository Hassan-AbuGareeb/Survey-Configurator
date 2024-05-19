using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using QuestionServices;
using Microsoft.Data.SqlClient;
using SharedResources.models;
using SharedResources;

namespace Survey_Configurator.Sub_forms
{
    public partial class AddEditQuestion : Form
    {
        private static int QuestionId;

        public AddEditQuestion()
        {
            InitializeComponent();
            Text = "Add";
            Add.Text = "Add";
        }

        public AddEditQuestion(int pQuestionId)
        {
            InitializeComponent();
            QuestionId = pQuestionId;
            Text = "Edit";
            Add.Text = "Save";
        }

        private void AddEdit_Load(object sender, EventArgs e)
        {
            QuestionTypeComboBox.SelectedItem = null;
            try
            {
                //to prevent any interruption in adding/updating
                QuestionOperations.OperationOngoing = true;
                //check if the operation is edit and fill the fields with selected question data
                if (Add.Text.Equals("Save"))
                {
                    Question tQeneralQuestionData = QuestionOperations.GetQuestionData(QuestionId);
                    //extract question data an add it to UI
                    QuestionTextBox.Text = tQeneralQuestionData.Text;
                    QuestionOrderNumeric.Value = tQeneralQuestionData.Order;
                    QuestionTypeComboBox.SelectedItem = tQeneralQuestionData.Type;

                    //based on the combobox value further data about the question should be obtained and added to UI
                    Question tQuestionSpecificData = QuestionOperations.GetQuestionSpecificData(QuestionId);
                    switch (tQuestionSpecificData.Type)
                    {
                        //for each case Get the info and downcast it and assign it to its respective field
                        case QuestionType.Stars:
                            NumberOfStarsNumeric.Value = ((StarsQuestion)tQuestionSpecificData).NumberOfStars;
                            break;
                        case QuestionType.Smiley:
                            NumberOfSmileysNumeric.Value = ((SmileyQuestion)tQuestionSpecificData).NumberOfSmileyFaces;
                            break;
                        case QuestionType.Slider:
                            SliderStartValueNumeric.Value = ((SliderQuestion)tQuestionSpecificData).StartValue;
                            SliderEndValueNumeric.Value = ((SliderQuestion)tQuestionSpecificData).EndValue;
                            SliderStartValueCaptionText.Text = ((SliderQuestion)tQuestionSpecificData).StartValueCaption;
                            SliderEndValueCaptionText.Text = ((SliderQuestion)tQuestionSpecificData).EndValueCaption;
                            break;
                    }
                    Add.Click -= AddButton_Click;
                    Add.Click += EditButton_Click;
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Database connection error, check the connection parameters or the sql server configurations");
                Close();
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                MessageBox.Show($"{ex.GetType().FullName}, {ex.StackTrace}");
                Close();
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all general fields are filled properly
                if (QuestionTextBox.Text.Length != 0 &&
                    QuestionTypeComboBox.SelectedItem != null)
                {
                    //get the enum value of the question type name
                    QuestionType tQuestionType = (QuestionType)QuestionTypeComboBox.SelectedItem;
                    //encapsulate obtained data in a question object
                    Question tNewQuestionData = new Question(QuestionTextBox.Text, (int)QuestionOrderNumeric.Value, tQuestionType);

                    //add question to db and interface
                    switch (tQuestionType)
                    {
                        //decied whether to add or edit question based on the Clicked button text
                        case QuestionType.Stars:
                            StarsQuestion tStarsData = new StarsQuestion(tNewQuestionData,(int)NumberOfStarsNumeric.Value);
                                QuestionOperations.AddQuestion(tStarsData);
                            break;
                        case QuestionType.Smiley:
                            SmileyQuestion tSmileyData = new SmileyQuestion(tNewQuestionData, (int)NumberOfSmileysNumeric.Value);
                            QuestionOperations.AddQuestion(tSmileyData);
                            break;
                        case QuestionType.Slider:
                            SliderQuestion tSliderData = new SliderQuestion(tNewQuestionData,
                                (int)SliderStartValueNumeric.Value, (int)SliderEndValueNumeric.Value,
                                SliderStartValueCaptionText.Text, SliderEndValueCaptionText.Text);
                                QuestionOperations.AddQuestion(tSliderData);
                            break;
                    }
                    //close form
                    Close();
                }
                else
                {
                    //some fields are empty or have wrong inputs
                    string tMissingFieldsMessage = "";
                    if (QuestionTextBox.Text.Length == 0 && QuestionTypeComboBox.SelectedItem == null)
                    {
                        tMissingFieldsMessage += "Question text, Question type ";
                    }
                    else if (QuestionTextBox.Text.Length == 0)
                    {
                        tMissingFieldsMessage += "Question text";
                    }
                    else
                    {
                        tMissingFieldsMessage += "Question type";
                    }
                    MessageBox.Show($"The following fields must have proper values: {tMissingFieldsMessage}", "Missing fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("error occured while Loading data, please try again");
                Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.StackTrace);
                MessageBox.Show("Database connection error, check the connection parameters or the sql server configurations");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.GetType().FullName}, {ex.StackTrace}");
                Close();
            }
            finally
            {
                QuestionOperations.OperationOngoing = false;
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all general fields are filled properly
                if (QuestionTextBox.Text.Length != 0 &&
                    QuestionTypeComboBox.SelectedItem != null)
                {
                    //get the enum value of the question type name
                    QuestionType tQuestionType = (QuestionType)QuestionTypeComboBox.SelectedItem;
                    //encapsulate obtained data in a question object
                    Question tNewQuestionData = new Question(QuestionId, QuestionTextBox.Text, (int)QuestionOrderNumeric.Value, tQuestionType);

                    //add question to db and interface
                    switch (QuestionTypeComboBox.SelectedItem)
                    {
                        //decied whether to add or edit question based on the Clicked button text
                        case QuestionType.Stars:
                            StarsQuestion tStarsData = new StarsQuestion(tNewQuestionData, (int)NumberOfStarsNumeric.Value);
                            QuestionOperations.UpdateQuestion(tStarsData);
                            break;
                        case QuestionType.Smiley:
                            SmileyQuestion tSmileyData = new SmileyQuestion(tNewQuestionData, (int)NumberOfSmileysNumeric.Value);
                            QuestionOperations.UpdateQuestion(tSmileyData);
                            break;
                        case QuestionType.Slider:
                            SliderQuestion tSliderData = new SliderQuestion(tNewQuestionData,
                                (int)SliderStartValueNumeric.Value, (int)SliderEndValueNumeric.Value,
                                SliderStartValueCaptionText.Text, SliderEndValueCaptionText.Text);
                                QuestionOperations.UpdateQuestion(tSliderData);
                            break;
                        
                    }
                    //close form
                    Close();
                }
                else
                {
                    //some fields are empty or have wrong inputs
                    string tMissingFieldsMessage = "";
                    if (QuestionTextBox.Text.Length == 0 && QuestionTypeComboBox.SelectedItem == null)
                    {
                        tMissingFieldsMessage += "Question text, Question type ";
                    }
                    else if (QuestionTextBox.Text.Length == 0)
                    {
                        tMissingFieldsMessage += "Question text";
                    }
                    else
                    {
                        tMissingFieldsMessage += "Question type";
                    }
                    MessageBox.Show($"The following fields must have proper values: {tMissingFieldsMessage}", "Missing fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("error occured while Loading data, please try again");
                Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.StackTrace);
                MessageBox.Show("Database connection error, check the connection parameters or the sql server configurations");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.GetType().FullName}, {ex.StackTrace}");
                Close();
            }
            finally
            {
                QuestionOperations.OperationOngoing = false;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult tCancelCreateQuestion = MessageBox.Show("Any changes made won't be saved.", "Cancel Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (tCancelCreateQuestion == DialogResult.Yes)
            {
                Close();
            }
        }

        private void QuestionTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            HideQuesitonOptionsPanel();
            switch (QuestionTypeComboBox.SelectedItem)
            {
                case QuestionType.Stars:
                    AddStarsOptions();
                    break;
                case QuestionType.Slider:
                    AddSliderOptions();
                    break;
                case QuestionType.Smiley:
                    AddSmileysOptions();
                    break;
            }
        }

        private void AddStarsOptions()
        {
            StarsQuestionOptionsPanel.Show();

            SliderQuestionOptionsPanel.SendToBack();
            SmileyQuestionOptionsPanel.SendToBack();
        }
        private void AddSmileysOptions()
        {
            SmileyQuestionOptionsPanel.Show();
            SliderQuestionOptionsPanel.SendToBack();
            StarsQuestionOptionsPanel.SendToBack();
        }
        private void AddSliderOptions()
        {
            SliderQuestionOptionsPanel.Show();
            StarsQuestionOptionsPanel.SendToBack();
            SmileyQuestionOptionsPanel.SendToBack();
        }

        private void HideQuesitonOptionsPanel()
        {
            StarsQuestionOptionsPanel.Hide();
            SmileyQuestionOptionsPanel.Hide();
            SliderQuestionOptionsPanel.Hide();
        }

        private void AddEditQuestion_FormClosing(object sender, FormClosingEventArgs e)
        {
            QuestionOperations.OperationOngoing = false;
        }
    }
}

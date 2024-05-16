﻿using System;
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

        //members that conditionally appears
        //stars question options
        private NumericUpDown NumberOfStarsNumeric;

        //smileys question options
        private NumericUpDown NumberOfSmileysNumeric;

        //slider question options
        private NumericUpDown SliderStartValueNumeric;
        private NumericUpDown SliderEndValueNumeric;
        private TextBox SliderStartValueCaptionText;
        private TextBox SliderEndValueCaptionText;

        public AddEditQuestion()
        {
            InitializeComponent();
            Text = "Add";
            Add.Text = "Add";

        }

        public AddEditQuestion(int questionId)
        {
            InitializeComponent();
            QuestionId = questionId;
            Text = "Edit";
            Add.Text = "Save";
        }

        private void AddEdit_Load(object sender, EventArgs e)
        {
            try
            {
                //to prevent any interruption in adding/updating
                QuestionOperations.OperationOngoing = true;
                //check if the operation is edit and fill the fields with selected question data
                if (Add.Text.Equals("Save"))
                {
                    //assing function on load
                    Add.Click += AddButton_Click;

                    Question tQeneralQuestionData = QuestionOperations.GetQuestionData(QuestionId);
                    //extract question data an add it to UI
                    QuestionTextBox.Text = tQeneralQuestionData.Text;
                    QuestionOrderNumeric.Value = tQeneralQuestionData.Order;
                    QuestionTypeComboBox.SelectedItem = tQeneralQuestionData.Type.ToString();

                    //based on the combobox value further data about the question should be obtained and added to UI
                    Question questionSpecificData = QuestionOperations.GetQuestionSpecificData(QuestionId);
                    switch (questionSpecificData.Type)
                    {
                        //for each case Get the info and downcast it and assign it to its respective field
                        case SharedData.cSmileyType:
                            NumberOfSmileysNumeric.Value = ((SmileyQuestion)questionSpecificData).NumberOfSmileyFaces;
                            break;
                        case SharedData.cStarsType:
                            NumberOfStarsNumeric.Value = ((StarsQuestion)questionSpecificData).NumberOfStars;
                            break;
                        case SharedData.cSliderType:
                            SliderStartValueNumeric.Value = ((SliderQuestion)questionSpecificData).StartValue;
                            SliderEndValueNumeric.Value = ((SliderQuestion)questionSpecificData).EndValue;
                            SliderStartValueCaptionText.Text = ((SliderQuestion)questionSpecificData).StartValueCaption;
                            SliderEndValueCaptionText.Text = ((SliderQuestion)questionSpecificData).EndValueCaption;
                            break;
                    }
                }
                else
                {
                    Add.Click += AddButton_Click;
                }
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

        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all general fields are filled properly
                if (QuestionTextBox.Text.Length != 0 &&
                    QuestionTypeComboBox.SelectedItem != null)
                {
                    //get the enum value of the question type name
                    Enum.TryParse(QuestionTypeComboBox.Text,out QuestionType tQuestionType);
                    //encapsulate obtained data in a question object
                    Question tNewQuestionData = new Question(QuestionTextBox.Text, (int)QuestionOrderNumeric.Value, tQuestionType);

                    //add question to db and interface
                    switch (tQuestionType)
                    {
                        //decied whether to add or edit question based on the Clicked button text
                        case SharedData.cStarsType:
                            StarsQuestion tStarsData = new StarsQuestion(tNewQuestionData,(int)NumberOfStarsNumeric.Value);
                                QuestionOperations.AddQuestion(tStarsData);
                            break;
                        case SharedData.cSliderType:
                            SliderQuestion tSliderData = new SliderQuestion(tNewQuestionData,
                                (int)SliderStartValueNumeric.Value, (int)SliderEndValueNumeric.Value,
                                SliderStartValueCaptionText.Text, SliderEndValueCaptionText.Text);
                                QuestionOperations.AddQuestion(tSliderData);
                            break;
                        case SharedData.cSmileyType:
                            SmileyQuestion tSmileyData = new SmileyQuestion(tNewQuestionData, (int)NumberOfSmileysNumeric.Value);
                                QuestionOperations.AddQuestion(tSmileyData);
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
                    Enum.TryParse(QuestionTypeComboBox.Text, out QuestionType tQuestionType);
                    //encapsulate obtained data in a question object
                    Question tNewQuestionData = new Question(QuestionId, QuestionTextBox.Text, (int)QuestionOrderNumeric.Value, tQuestionType);

                    //add question to db and interface
                    switch (QuestionTypeComboBox.Text)
                    {
                        //decied whether to add or edit question based on the Clicked button text
                        case nameof(SharedData.cStarsType):
                            StarsQuestion tStarsData = new StarsQuestion(tNewQuestionData, (int)NumberOfStarsNumeric.Value);
                            QuestionOperations.UpdateQuestion(tStarsData);
                            break;
                        case nameof(SharedData.cSliderType):
                            SliderQuestion tSliderData = new SliderQuestion(tNewQuestionData,
                                (int)SliderStartValueNumeric.Value, (int)SliderEndValueNumeric.Value,
                                SliderStartValueCaptionText.Text, SliderEndValueCaptionText.Text);
                                QuestionOperations.UpdateQuestion(tSliderData);
                            break;
                        case nameof(SharedData.cSmileyType):
                            SmileyQuestion tSmileyData = new SmileyQuestion(tNewQuestionData, (int)NumberOfSmileysNumeric.Value);
                                QuestionOperations.UpdateQuestion(tSmileyData);
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
            QuestionOptions.Controls.Clear();
            switch (QuestionTypeComboBox.SelectedItem.ToString())
            {
                case "Slider":
                    AddSliderOptions();
                    break;
                case "Smiley":
                    AddSmileysOptions();
                    break;
                case "Stars":
                    AddStarsOptions();
                    break;
            }
        }

        private void AddStarsOptions()
        {
            //add a label next to the numeric field
            Label NumberOfStarsLabel = new Label();
            NumberOfStarsLabel.AutoSize = true;
            NumberOfStarsLabel.Font = new Font("Segoe UI", 14.25F);
            NumberOfStarsLabel.Location = new Point(0, 0);
            NumberOfStarsLabel.Name = "NumberOfStarsLabel";
            NumberOfStarsLabel.Size = new Size(45, 25);
            NumberOfStarsLabel.TabIndex = 9;
            NumberOfStarsLabel.Text = "Number of Stars";

            //add a numeric field to specify a number for the smileys
            NumberOfStarsNumeric = new NumericUpDown();
            NumberOfStarsNumeric.Location = new Point(205, 0);
            NumberOfStarsNumeric.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            NumberOfStarsNumeric.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            NumberOfStarsNumeric.Name = "NumberOfStarsNumeric";
            NumberOfStarsNumeric.Size = new Size(120, 23);
            NumberOfStarsNumeric.TabIndex = 10;
            NumberOfStarsNumeric.Value = new decimal(new int[] { 5, 0, 0, 0 });
            NumberOfStarsNumeric.TabIndex = 4;

            //add fields to the form
            QuestionOptions.Controls.Add(NumberOfStarsLabel);
            QuestionOptions.Controls.Add(NumberOfStarsNumeric);
        }
        private void AddSliderOptions()
        {
            //label start value items
            //add a label next to the numeric field
            Label SliderStartValueLabel = new Label();
            SliderStartValueLabel.AutoSize = true;
            SliderStartValueLabel.Font = new Font("Segoe UI", 14.25F);
            SliderStartValueLabel.Location = new Point(0, 0);
            SliderStartValueLabel.Name = "SliderStartValue";
            SliderStartValueLabel.Size = new Size(45, 25);
            SliderStartValueLabel.TabIndex = 9;
            SliderStartValueLabel.Text = "Start value";
            //add a numeric field to specify a number for the smileys
            SliderStartValueNumeric = new NumericUpDown();
            SliderStartValueNumeric.Location = new Point(230, 0);
            SliderStartValueNumeric.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            SliderStartValueNumeric.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            SliderStartValueNumeric.Name = "SliderStartValueNumeric";
            SliderStartValueNumeric.Size = new Size(120, 23);
            SliderStartValueNumeric.TabIndex = 10;
            SliderStartValueNumeric.Value = new decimal(new int[] { 0, 0, 0, 0 });
            SliderStartValueNumeric.TabIndex = 4;

            //label end value items
            Label SliderEndValueLabel = new Label();
            SliderEndValueLabel.AutoSize = true;
            SliderEndValueLabel.Font = new Font("Segoe UI", 14.25F);
            SliderEndValueLabel.Location = new Point(0, 50);
            SliderEndValueLabel.Name = "SliderEndValue";
            SliderEndValueLabel.Size = new Size(45, 25);
            SliderEndValueLabel.TabIndex = 11;
            SliderEndValueLabel.Text = "End value";
            //add a numeric field to specify a number for the smileys
            SliderEndValueNumeric = new NumericUpDown();
            SliderEndValueNumeric.Location = new Point(230, 50);
            SliderEndValueNumeric.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            SliderEndValueNumeric.Minimum = new decimal(new int[] { 50, 0, 0, 0 });
            SliderEndValueNumeric.Name = "SliderEndValueNumeric";
            SliderEndValueNumeric.Size = new Size(120, 23);
            SliderEndValueNumeric.TabIndex = 12;
            SliderEndValueNumeric.Value = new decimal(new int[] { 100, 0, 0, 0 });
            SliderEndValueNumeric.TabIndex = 5;

            //label end value items
            Label SliderStartValueCaptionLabel = new Label();
            SliderStartValueCaptionLabel.AutoSize = true;
            SliderStartValueCaptionLabel.Font = new Font("Segoe UI", 14.25F);
            SliderStartValueCaptionLabel.Location = new Point(0, 100);
            SliderStartValueCaptionLabel.Name = "SliderStartValueCaptionLabels";
            SliderStartValueCaptionLabel.Size = new Size(45, 25);
            SliderStartValueCaptionLabel.TabIndex = 13;
            SliderStartValueCaptionLabel.Text = "Start value caption";
            //add a numeric field to specify a number for the smileys
            SliderStartValueCaptionText = new TextBox();
            SliderStartValueCaptionText.Location = new Point(230, 100);
            SliderStartValueCaptionText.Name = "SliderStartValueCaptionText";
            SliderStartValueCaptionText.Size = new Size(120, 23);
            SliderStartValueCaptionText.TabIndex = 14;
            SliderStartValueCaptionText.Text = "Min";
            SliderStartValueCaptionLabel.TabIndex = 6;

            //label end value items
            Label SliderEndValueCaptionLabel = new Label();
            SliderEndValueCaptionLabel.AutoSize = true;
            SliderEndValueCaptionLabel.Font = new Font("Segoe UI", 14.25F);
            SliderEndValueCaptionLabel.Location = new Point(0, 150);
            SliderEndValueCaptionLabel.Name = "SliderEndValueCaptionLabel";
            SliderEndValueCaptionLabel.Size = new Size(45, 25);
            SliderEndValueCaptionLabel.TabIndex = 13;
            SliderEndValueCaptionLabel.Text = "End value caption";
            //add a numeric field to specify a number for the smileys
            SliderEndValueCaptionText = new TextBox();
            SliderEndValueCaptionText.Location = new Point(230, 150);
            SliderEndValueCaptionText.Name = "SliderEndValueCaptionText";
            SliderEndValueCaptionText.Size = new Size(120, 23);
            SliderEndValueCaptionText.TabIndex = 14;
            SliderEndValueCaptionText.Text = "Max";
            SliderEndValueCaptionText.TabIndex = 7;

            QuestionOptions.Controls.Add(SliderStartValueLabel);
            QuestionOptions.Controls.Add(SliderStartValueNumeric);
            QuestionOptions.Controls.Add(SliderEndValueLabel);
            QuestionOptions.Controls.Add(SliderEndValueNumeric);
            QuestionOptions.Controls.Add(SliderStartValueCaptionLabel);
            QuestionOptions.Controls.Add(SliderStartValueCaptionText);
            QuestionOptions.Controls.Add(SliderEndValueCaptionLabel);
            QuestionOptions.Controls.Add(SliderEndValueCaptionText);
        }
        private void AddSmileysOptions()
        {
            //add a label next to the numeric field
            Label NumberOfSmileysLabel = new Label();
            NumberOfSmileysLabel.AutoSize = true;
            NumberOfSmileysLabel.Font = new Font("Segoe UI", 14.25F);
            NumberOfSmileysLabel.Location = new Point(0, 0);
            NumberOfSmileysLabel.Name = "NumberOfSmileysLabel";
            NumberOfSmileysLabel.Size = new Size(45, 25);
            NumberOfSmileysLabel.TabIndex = 9;
            NumberOfSmileysLabel.Text = "Number of Smileys";

            //add a numeric field to specify a number for the smileys
            NumberOfSmileysNumeric = new NumericUpDown();
            NumberOfSmileysNumeric.Location = new Point(237, 0);
            NumberOfSmileysNumeric.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            NumberOfSmileysNumeric.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            NumberOfSmileysNumeric.Name = "NumberOfSmileysNumeric";
            NumberOfSmileysNumeric.Size = new Size(120, 23);
            NumberOfSmileysNumeric.TabIndex = 10;
            NumberOfSmileysNumeric.Value = new decimal(new int[] { 2, 0, 0, 0 });
            NumberOfSmileysNumeric.TabIndex = 4;

            //add fields to the form
            QuestionOptions.Controls.Add(NumberOfSmileysLabel);
            QuestionOptions.Controls.Add(NumberOfSmileysNumeric);
        }

        private void AddEditQuestion_FormClosing(object sender, FormClosingEventArgs e)
        {
            QuestionOperations.OperationOngoing = false;
        }
    }
}

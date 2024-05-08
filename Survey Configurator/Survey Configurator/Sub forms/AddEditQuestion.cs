using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Logic;
using Microsoft.Data.SqlClient;
using DatabaseLayer.models;


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
            TitleLabel.Text = "Add Question";
            Text = "Add";
            Add.Text = "Add";

        }

        public AddEditQuestion(int questionId)
        {
            InitializeComponent();
            QuestionId = questionId;
            Text = "Edit";
            TitleLabel.Text = "Edit Question";
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
                    DataRow generalQuestionData = QuestionOperations.GetQuestionData(QuestionId);
                    //extract question data an add it to UI
                    QuestionTextBox.Text = generalQuestionData["Q_text"].ToString();
                    QuestionOrderNumeric.Value = (int)generalQuestionData["Q_order"];
                    QuestionTypeComboBox.SelectedItem = generalQuestionData["Q_type"];

                    //based on the combobox value further data about the question should be obtained and added to UI
                    string QuestionType = generalQuestionData["Q_type"].ToString();
                    DataRow questionSpecificData = QuestionOperations.GetQuestionSpecificData(QuestionId, QuestionType);
                    switch (QuestionType)
                    {
                        //for each case Get the info and downcast it and assign it to its respective field
                        case "Smiley":
                            NumberOfSmileysNumeric.Value = (int)questionSpecificData["Num_of_faces"];
                            break;
                        case "Stars":
                            NumberOfStarsNumeric.Value = (int)questionSpecificData["Num_of_stars"];
                            break;
                        case "Slider":
                            SliderStartValueNumeric.Value = (int)questionSpecificData["Start_value"];
                            SliderEndValueNumeric.Value = (int)questionSpecificData["End_value"];
                            SliderStartValueCaptionText.Text = questionSpecificData["Start_value_caption"].ToString();
                            SliderEndValueCaptionText.Text = questionSpecificData["End_value_caption"].ToString();
                            break;
                    }
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
                    //add question to db and interface
                    switch (QuestionTypeComboBox.Text)
                    {
                        //decied whether to add or edit question based on the Clicked button text
                        case "Stars":
                            StarsQuestion starsData = new StarsQuestion(QuestionTextBox.Text, (int)QuestionOrderNumeric.Value, (int)NumberOfStarsNumeric.Value);
                            if (Add.Text.Equals("Add"))
                                QuestionOperations.AddQuestion(starsData);
                            else
                                QuestionOperations.UpdateQuestion(QuestionId, starsData);
                            break;
                        case "Slider":
                            SliderQuestion sliderData = new SliderQuestion(QuestionTextBox.Text, (int)QuestionOrderNumeric.Value,
                                (int)SliderStartValueNumeric.Value, (int)SliderEndValueNumeric.Value,
                                SliderStartValueCaptionText.Text, SliderEndValueCaptionText.Text);
                            if (Add.Text.Equals("Add"))
                                QuestionOperations.AddQuestion(sliderData);
                            else
                                QuestionOperations.UpdateQuestion(QuestionId, sliderData);
                            break;
                        case "Smiley":
                            SmileyQuestion smileyData = new SmileyQuestion(QuestionTextBox.Text, (int)QuestionOrderNumeric.Value, (int)NumberOfSmileysNumeric.Value);
                            if (Add.Text.Equals("Add"))
                                QuestionOperations.AddQuestion(smileyData);
                            else
                                QuestionOperations.UpdateQuestion(QuestionId, smileyData);
                            break;
                    }
                    //show success message
                    MessageBox.Show("Question has been added successfully!", "Operation successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    //close form
                    Close();
                }
                else
                {
                    //some fields are empty or have wrong inputs
                    string missingFieldsMessage = "";
                    if (QuestionTextBox.Text.Length == 0 && QuestionTypeComboBox.SelectedItem == null)
                    {
                        missingFieldsMessage += "Question text, Question type ";
                    }
                    else if (QuestionTextBox.Text.Length == 0)
                    {
                        missingFieldsMessage += "Question text";
                    }
                    else
                    {
                        missingFieldsMessage += "Question type";
                    }
                    MessageBox.Show($"The following fields must have proper values: {missingFieldsMessage}", "Missing fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            DialogResult cancelCreateQuestion = MessageBox.Show("Any changes made won't be saved.", "Cancel Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cancelCreateQuestion == DialogResult.Yes)
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

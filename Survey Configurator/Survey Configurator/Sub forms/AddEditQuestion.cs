using QuestionServices;
using SharedResources.models;
using SharedResources;

namespace Survey_Configurator.Sub_forms
{
    public partial class AddEditQuestion : Form
    {
        //use constants and enumssss
        private static int QuestionId;
        private static string Operation;
        public AddEditQuestion()
        {
            try 
            {
                InitializeComponent();
                Text = "Add";
                AddEditLabel.Text = "Add Question";
                OperationButton.Text = "Add";
                Operation = "Add";
                OperationButton.Click += AddButton_Click;
            }
            catch(Exception ex)
            {
                UtilityMethods.LogError(ex);
                MessageBox.Show("An UnExpected error occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        public AddEditQuestion(int pQuestionId)
        {
            try 
            { 
                InitializeComponent();
                QuestionId = pQuestionId;
                Text = "Edit";
                AddEditLabel.Text = "Edit Question";
                OperationButton.Text = "Edit";
                Operation = "Edit";
                OperationButton.Click += EditButton_Click;
            }
            catch(Exception ex)
            {
                UtilityMethods.LogError(ex);
                MessageBox.Show("An UnExpected error occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void AddEdit_Load(object sender, EventArgs e)
        {
            try
            {
                //to prevent any interruption in adding/updating
                QuestionOperations.OperationOngoing = true;
                //encapsulate the following code into another function to make things look simpler
                //check if the operation is edit and fill the fields with selected question data
                if (Operation == "Edit")
                {
                    Question tGeneralQuestionData = QuestionOperations.GetQuestionData(QuestionId);
                    //extract question data an add it to UI
                    QuestionTextBox.Text = tGeneralQuestionData.Text;
                    QuestionOrderNumeric.Value = tGeneralQuestionData.Order;
                    QuestionTypeComboBox.SelectedItem = tGeneralQuestionData.Type;

                    //based on the combobox value further data about the question should be obtained and added to UI
                    Question tQuestionSpecificData=null;
                    OperationResult tQuestionSpecificDataResult = QuestionOperations.GetQuestionSpecificData(QuestionId, ref tQuestionSpecificData);
                    if (tQuestionSpecificDataResult.IsSuccess)
                    {
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
                    }
                    else
                    {
                        MessageBox.Show("An Unkown error occure", "Unkown error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Close();
                    }
                }
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
                    string tQuestionText = QuestionTextBox.Text;
                    int tQuestionOrder = (int)QuestionOrderNumeric.Value;
                    //get the enum value of the question type
                    QuestionType tQuestionType = (QuestionType)QuestionTypeComboBox.SelectedItem;
                    //encapsulate obtained data in a question object
                    Question tNewQuestionData = new Question(tQuestionText, tQuestionOrder, tQuestionType);

                    OperationResult tQuestionAddedResult=null;
                    //add question to db and interface
                    switch (tQuestionType)
                    {
                        //decied whether to add or edit question based on the Clicked button text
                        case QuestionType.Stars:
                            StarsQuestion tStarsData = new StarsQuestion(tNewQuestionData,(int)NumberOfStarsNumeric.Value);
                            tQuestionAddedResult = QuestionOperations.AddQuestion(tStarsData);
                            break;
                        case QuestionType.Smiley:
                            SmileyQuestion tSmileyData = new SmileyQuestion(tNewQuestionData, (int)NumberOfSmileysNumeric.Value);
                            tQuestionAddedResult = QuestionOperations.AddQuestion(tSmileyData);
                            break;
                        case QuestionType.Slider:
                            SliderQuestion tSliderData = new SliderQuestion(tNewQuestionData,
                                (int)SliderStartValueNumeric.Value, (int)SliderEndValueNumeric.Value,
                                SliderStartValueCaptionText.Text, SliderEndValueCaptionText.Text);
                                tQuestionAddedResult = QuestionOperations.AddQuestion(tSliderData);
                            break;
                    }
                    if (tQuestionAddedResult!=null && !tQuestionAddedResult.IsSuccess)
                    {
                        MessageBox.Show("An error occured while adding the question", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
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
                    string tQuestionText = QuestionTextBox.Text;
                    int tQuestionOrder = (int)QuestionOrderNumeric.Value;
                    //get the enum value of the question type
                    QuestionType tQuestionType = (QuestionType)QuestionTypeComboBox.SelectedItem;
                    //encapsulate obtained data in a question object
                    Question tNewQuestionData = new Question(QuestionId, tQuestionText, tQuestionOrder, tQuestionType);
                    OperationResult tQuestionUpdatedResult = null;
                    //add question to db and interface
                    switch (tQuestionType)
                    {
                        //send question data to logic layer to be updated
                        case QuestionType.Stars:
                            StarsQuestion tStarsData = new StarsQuestion(tNewQuestionData, (int)NumberOfStarsNumeric.Value);
                            tQuestionUpdatedResult = QuestionOperations.UpdateQuestion(tStarsData);
                            break;
                        case QuestionType.Smiley:
                            SmileyQuestion tSmileyData = new SmileyQuestion(tNewQuestionData, (int)NumberOfSmileysNumeric.Value);
                            tQuestionUpdatedResult = QuestionOperations.UpdateQuestion(tSmileyData);
                            break;
                        case QuestionType.Slider:
                            SliderQuestion tSliderData = new SliderQuestion(tNewQuestionData,
                                (int)SliderStartValueNumeric.Value, (int)SliderEndValueNumeric.Value,
                                SliderStartValueCaptionText.Text, SliderEndValueCaptionText.Text);
                            tQuestionUpdatedResult = QuestionOperations.UpdateQuestion(tSliderData);
                            break;
                        
                    }
                    if (tQuestionUpdatedResult != null && !tQuestionUpdatedResult.IsSuccess)
                    {
                        MessageBox.Show("An error occured while adding the question", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
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
            try 
            { 
                DialogResult tCancelCreateQuestion = MessageBox.Show("Any changes made won't be saved.", "Cancel Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (tCancelCreateQuestion == DialogResult.Yes)
                {
                    Close();
                }
            }
            catch(Exception ex) 
            { 
                UtilityMethods.LogError(ex);
                MessageBox.Show("An Unexpected error occured", "Error");
            }
        }

        private void QuestionTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try { 
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
            }catch(Exception ex)
            {
                UtilityMethods.LogError(ex);
                MessageBox.Show("Question type error", "Type error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddStarsOptions()
        {
            try 
            { 
                StarsQuestionOptionsPanel.Show();
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                MessageBox.Show("An unexpected error occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AddSmileysOptions()
        {
            try
            {
                SmileyQuestionOptionsPanel.Show();
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                MessageBox.Show("An unexpected error occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AddSliderOptions()
        {
            try 
            {
                SliderQuestionOptionsPanel.Show();
            }
            catch(Exception ex)
            {
                UtilityMethods.LogError(ex);
                MessageBox.Show("An unexpected error occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}

        private void HideQuesitonOptionsPanel()
        {
            try { 
                StarsQuestionOptionsPanel.Hide();
                SmileyQuestionOptionsPanel.Hide();
                SliderQuestionOptionsPanel.Hide();
            }
            catch(Exception ex)
            {
                UtilityMethods.LogError (ex);
                MessageBox.Show("An unexpected error occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddEditQuestion_FormClosing(object sender, FormClosingEventArgs e)
        {
            try { 
            QuestionOperations.OperationOngoing = false;
            }
            catch(Exception ex) 
            { 
                UtilityMethods.LogError(ex);
                MessageBox.Show("An unexpected error occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

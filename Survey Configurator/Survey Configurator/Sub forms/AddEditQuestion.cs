using QuestionServices;
using SharedResources.models;
using SharedResources;
using Survey_Configurator.Custom_controls;

namespace Survey_Configurator.Sub_forms
{
    public partial class AddEditQuestion : Form
    {
        /// <summary>
        /// this class is responsible for instantiating the form to add and edit
        /// questions, it has two constructors a default one for the add operation
        /// and another for the edit operation which requires the Id of the question
        /// to be edited
        /// </summary>


        //the id of the question to be edited
        private static int mQuestionId;
        //current operation add/edit
        private static string mOperation;

        //user-control questions objects
        private static StarsQuestionOptions mStarsQuestionOptionsPanel;
        private static SmileyQuestionOptions mSmileyQuestionOptionsPanel;
        private static SliderQuestionOptions mSliderQuestionOptionsPanel;
        //location of the question options panel
        private Point mQuestionOptionsPanelLocation = new Point(12, 85);

        /// <summary>
        /// add operation constructor, assign the AddButton click function
        /// for the button click event
        /// </summary>
        public AddEditQuestion()
        {
            try
            {
                InitializeComponent();
                Text = GlobalStrings.AddQuestion;
                OperationButton.Text = GlobalStrings.AddOperation;
                mOperation = GlobalStrings.AddOperation;
                OperationButton.Click += AddButton_Click;
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                MainScreen.ShowDefaultErrorMessage();
            }
        }

        /// <summary>
        /// edit operation constructor assign the editButton click function
        /// for the button click event
        /// </summary>
        /// <param name="pQuestionId"></param>
        public AddEditQuestion(int pQuestionId)
        {
            try
            {
                InitializeComponent();
                mQuestionId = pQuestionId;
                Text = GlobalStrings.EditQuestion;
                OperationButton.Text = GlobalStrings.EditOperation;
                mOperation = GlobalStrings.EditOperation;
                OperationButton.Click += EditButton_Click;
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                MainScreen.ShowDefaultErrorMessage();
            }
        }

        /// <summary>
        /// works on the load event for this form, sets the Opeartion ongoing property to true to
        /// prevent any change in the data while adding or editing and if the operation porperty is 
        /// Edit it gets the data of the selected data to change it
        /// the current version of the data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddEdit_Load(object sender, EventArgs e)
        {
            try
            {
                //to prevent any interruption in adding/updating
                QuestionOperations.mOperationOngoing = true;
                //check if the operation is edit and fill the fields with selected question data
                if (mOperation == GlobalStrings.EditOperation)
                {
                    Question tGeneralQuestionData = QuestionOperations.GetQuestionData(mQuestionId);
                    //extract question data an add it to UI
                    QuestionTextBox.Text = tGeneralQuestionData.Text;
                    QuestionOrderNumeric.Value = tGeneralQuestionData.Order;
                    QuestionTypeComboBox.SelectedIndex = (int)tGeneralQuestionData.Type;

                    //based on the combobox value further data about the question should be obtained and added to UI

                    Question tQuestionSpecificData = null;

                    //gets the specific data of the question to show it in the form
                    OperationResult tQuestionSpecificDataResult = QuestionOperations.GetQuestionSpecificData(mQuestionId, ref tQuestionSpecificData);
                    if (tQuestionSpecificDataResult.IsSuccess)
                    {
                        switch (tQuestionSpecificData.Type)
                        {
                            //for each case Get the info and downcast it and assign it to its respective field
                            case eQuestionType.Stars:
                                mStarsQuestionOptionsPanel.NumberOfStarsNumeric.Value = ((StarsQuestion)tQuestionSpecificData).NumberOfStars;
                                break;
                            case eQuestionType.Smiley:
                                mSmileyQuestionOptionsPanel.NumberOfSmileysNumeric.Value = ((SmileyQuestion)tQuestionSpecificData).NumberOfSmileyFaces;
                                break;
                            case eQuestionType.Slider:
                                mSliderQuestionOptionsPanel.SliderStartValueNumeric.Value = ((SliderQuestion)tQuestionSpecificData).StartValue;
                                mSliderQuestionOptionsPanel.SliderEndValueNumeric.Value = ((SliderQuestion)tQuestionSpecificData).EndValue;
                                mSliderQuestionOptionsPanel.SliderStartValueCaptionText.Text = ((SliderQuestion)tQuestionSpecificData).StartValueCaption;
                                mSliderQuestionOptionsPanel.SliderEndValueCaptionText.Text = ((SliderQuestion)tQuestionSpecificData).EndValueCaption;
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show(tQuestionSpecificDataResult.mErrorMessage, tQuestionSpecificDataResult.mError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                MainScreen.ShowDefaultErrorMessage();
                Close();
            }
        }

        /// <summary>
        /// Add a new Question object to the database,
        /// check if all the fields are filled with appropriate values,
        /// sends the question data to be added to database
        /// in case of faliure recieves a failure response
        /// to show a message of failure in adding the question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all general fields are filled properly
                if (QuestionTextBox.Text.Length != 0 &&
                    QuestionTypeComboBox.SelectedItem != null)
                {
                    //extract the question data from the form Controls
                    string tQuestionText = QuestionTextBox.Text;
                    int tQuestionOrder = (int)QuestionOrderNumeric.Value;
                    //get the enum value of the question type
                    eQuestionType tQuestionType = (eQuestionType)QuestionTypeComboBox.SelectedIndex;
                    //encapsulate obtained data in a question object
                    Question tNewQuestionData = new Question(tQuestionText, tQuestionOrder, tQuestionType);

                        
                        //check validity of input
                    bool tIsInputValid = ValidateInput(tQuestionType);
                    if (tIsInputValid) 
                    { 
                            OperationResult tQuestionAddedResult = null;
                        //add question to db and interface
                        switch (tQuestionType)
                        {
                            //create a question object based on the chosen type and add it to the database
                            case eQuestionType.Stars:
                                StarsQuestion tStarsData = new StarsQuestion(tNewQuestionData, (int)mStarsQuestionOptionsPanel.NumberOfStarsNumeric.Value);
                                tQuestionAddedResult = QuestionOperations.AddQuestion(tStarsData);
                                break;
                            case eQuestionType.Smiley:
                                SmileyQuestion tSmileyData = new SmileyQuestion(tNewQuestionData, (int)mSmileyQuestionOptionsPanel.NumberOfSmileysNumeric.Value);
                                tQuestionAddedResult = QuestionOperations.AddQuestion(tSmileyData);
                                break;
                            case eQuestionType.Slider:
                                SliderQuestion tSliderData = new SliderQuestion(
                                    tNewQuestionData,
                                    (int)mSliderQuestionOptionsPanel.SliderStartValueNumeric.Value,
                                    (int)mSliderQuestionOptionsPanel.SliderEndValueNumeric.Value,
                                    mSliderQuestionOptionsPanel.SliderStartValueCaptionText.Text,
                                    mSliderQuestionOptionsPanel.SliderEndValueCaptionText.Text);
                                tQuestionAddedResult = QuestionOperations.AddQuestion(tSliderData);
                                break;
                        }

                        if (tQuestionAddedResult != null && !tQuestionAddedResult.IsSuccess)
                        {
                            MessageBox.Show(tQuestionAddedResult.mErrorMessage, tQuestionAddedResult.mError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        //close form
                        Close();
                    }
                }
                else
                {
                    //some fields are empty or have wrong inputs
                    string tMissingFieldsMessage = "";
                    if (QuestionTextBox.Text.Length == 0 && QuestionTypeComboBox.SelectedItem == null)
                    {
                        tMissingFieldsMessage += $"{GlobalStrings.QuestionText}, {GlobalStrings.QuestionType}";
                    }
                    else if (QuestionTextBox.Text.Length == 0)
                    {
                        tMissingFieldsMessage += GlobalStrings.QuestionText;
                    }
                    else
                    {
                        tMissingFieldsMessage += GlobalStrings.QuestionType;
                    }
                    MessageBox.Show($"{GlobalStrings.MissingFields} {tMissingFieldsMessage}", GlobalStrings.RequiredFields, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                MainScreen.ShowDefaultErrorMessage();
                Close();
            }
        }

        /// <summary>
        /// Edit an existing Question object data,
        /// check if all the fields are filled with appropriate values,
        /// sends the new question data to be added to database
        /// in case of faliure recieves a failure response
        /// to show a message of failure in editing the question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    eQuestionType tQuestionType = (eQuestionType)QuestionTypeComboBox.SelectedIndex;
                    //encapsulate obtained data in a question object
                    Question tNewQuestionData = new Question(mQuestionId, tQuestionText, tQuestionOrder, tQuestionType);

                    bool tIsInputValid = ValidateInput(tQuestionType);
                    if (tIsInputValid) { 
                    OperationResult tQuestionUpdatedResult = null;
                    //add question to db and interface

                    switch (tQuestionType)
                    {
                        //send question data to be updated
                        case eQuestionType.Stars:
                            StarsQuestion tStarsData = new StarsQuestion(tNewQuestionData, (int)mStarsQuestionOptionsPanel.NumberOfStarsNumeric.Value);
                            tQuestionUpdatedResult = QuestionOperations.UpdateQuestion(tStarsData);
                            break;
                        case eQuestionType.Smiley:
                            SmileyQuestion tSmileyData = new SmileyQuestion(tNewQuestionData, (int)mSmileyQuestionOptionsPanel.NumberOfSmileysNumeric.Value);
                            tQuestionUpdatedResult = QuestionOperations.UpdateQuestion(tSmileyData);
                            break;
                        case eQuestionType.Slider:
                            SliderQuestion tSliderData = new SliderQuestion(
                                tNewQuestionData,
                                (int)mSliderQuestionOptionsPanel.SliderStartValueNumeric.Value,
                                (int)mSliderQuestionOptionsPanel.SliderEndValueNumeric.Value,
                                mSliderQuestionOptionsPanel.SliderStartValueCaptionText.Text,
                                mSliderQuestionOptionsPanel.SliderEndValueCaptionText.Text);
                            tQuestionUpdatedResult = QuestionOperations.UpdateQuestion(tSliderData);
                            break;

                    }
                        if (tQuestionUpdatedResult != null && !tQuestionUpdatedResult.IsSuccess)
                        {
                            MessageBox.Show(tQuestionUpdatedResult.mErrorMessage, tQuestionUpdatedResult.mError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        Close();
                    }
                }
                else
                {
                    //some fields are empty or have wrong inputs
                    string tMissingFieldsMessage = "";
                    if (QuestionTextBox.Text.Length == 0 && QuestionTypeComboBox.SelectedItem == null)
                    {
                        tMissingFieldsMessage += $"{GlobalStrings.QuestionText}, {GlobalStrings.QuestionType}";
                    }
                    else if (QuestionTextBox.Text.Length == 0)
                    {
                        tMissingFieldsMessage += GlobalStrings.QuestionText;
                    }
                    else
                    {
                        tMissingFieldsMessage += GlobalStrings.QuestionType;
                    }
                    MessageBox.Show($"{GlobalStrings.MissingFields} {tMissingFieldsMessage}", GlobalStrings.RequiredFields, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                MainScreen.ShowDefaultErrorMessage();
                Close();
            }
        }

        /// <summary>
        /// check whether the user clicked on this button by mistake
        /// if not close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult tCancelCreateQuestion = MessageBox.Show(GlobalStrings.CancelOperation, GlobalStrings.CancelOperationTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (tCancelCreateQuestion == DialogResult.Yes)
                {
                    Close();
                }
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                MainScreen.ShowDefaultErrorMessage();
            }
        }

        /// <summary>
        /// show the Question optoins control relevant to the chosen question type
        /// gets the question type based on the index of the chosen question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuestionTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                HideQuesitonOptionsPanel();
                eQuestionType tSelectedItemValue = (eQuestionType)QuestionTypeComboBox.SelectedIndex;
                switch (tSelectedItemValue)
                {
                    case eQuestionType.Stars:
                        AddStarsOptions();
                        break;
                    case eQuestionType.Slider:
                        AddSliderOptions();
                        break;
                    case eQuestionType.Smiley:
                        AddSmileysOptions();
                        break;
                }
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                MainScreen.ShowDefaultErrorMessage();
            }
        }

        /// <summary>
        /// the purpopse of this function is to set the operationOngoin property to true
        /// to allow changes to database to be reflected to the ListView control data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddEditQuestion_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                QuestionOperations.mOperationOngoing = false;
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                MainScreen.ShowDefaultErrorMessage();
            }
        }

        #region quesitons options panel control options

        /// <summary>
        /// each one of these functions shows control component
        /// relevant to the chosen question type
        /// </summary>

        private void AddStarsOptions()
        {
            try
            {
                mStarsQuestionOptionsPanel = new StarsQuestionOptions();
                mStarsQuestionOptionsPanel.Location = mQuestionOptionsPanelLocation;
                TypeInformationGroupBox.Controls.Add(mStarsQuestionOptionsPanel);
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                MainScreen.ShowDefaultErrorMessage();
            }
        }
        private void AddSmileysOptions()
        {
            try
            {
                mSmileyQuestionOptionsPanel = new SmileyQuestionOptions();
                mSmileyQuestionOptionsPanel.Location = mQuestionOptionsPanelLocation;
                TypeInformationGroupBox.Controls.Add(mSmileyQuestionOptionsPanel);
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                MainScreen.ShowDefaultErrorMessage();
            }
        }
        private void AddSliderOptions()
        {
            try
            {
                mSliderQuestionOptionsPanel = new SliderQuestionOptions();
                mSliderQuestionOptionsPanel.Location = mQuestionOptionsPanelLocation;
                TypeInformationGroupBox.Controls.Add(mSliderQuestionOptionsPanel);
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                MainScreen.ShowDefaultErrorMessage();
            }
        }
        private void HideQuesitonOptionsPanel()
        {
            try
            {
                TypeInformationGroupBox.Controls.Remove(mStarsQuestionOptionsPanel);
                TypeInformationGroupBox.Controls.Remove(mSmileyQuestionOptionsPanel);
                TypeInformationGroupBox.Controls.Remove(mSliderQuestionOptionsPanel);
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                MainScreen.ShowDefaultErrorMessage();
            }
        }

        #endregion

        #region class utility functions

        /// <summary>
        /// this function peroforms general validation on the given question type
        /// </summary>
        /// <param name="tQuestionType">type of the question</param>
        /// <returns></returns>
        private static bool ValidateInput(eQuestionType tQuestionType)
        {
            bool tInputIsValid = true;
            switch (tQuestionType)
            {
                case eQuestionType.Slider:
                    tInputIsValid = ValidateSliderQuestionData();
                    break;
            }

            return tInputIsValid;
        }

        /// <summary>
        /// validation function for the slider question type, insures that the min value is smaller than max value
        /// </summary>
        /// <returns></returns>
        private static bool ValidateSliderQuestionData()
        {
            if (mSliderQuestionOptionsPanel.SliderStartValueNumeric.Value > mSliderQuestionOptionsPanel.SliderEndValueNumeric.Value)
            {
                MessageBox.Show(GlobalStrings.SliderInvalidInput, GlobalStrings.InvalidInputTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        #endregion
    }
}

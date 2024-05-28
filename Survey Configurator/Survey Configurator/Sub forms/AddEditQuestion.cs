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
        private static int QuestionId;
        //current operation add/edit
        private static string Operation;

        //user-control questions objects
        private StarsQuestionOptions StarsQuestionOptionsPanel;
        private SmileyQuestionOptions SmileyQuestionOptionsPanel;
        private SliderQuestionOptions SliderQuestionOptionsPanel;
        //location of the question options panel
        private Point QuestionOptionsPanelLocation = new Point(12, 175);

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
                Operation = GlobalStrings.AddOperation;
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
                QuestionId = pQuestionId;
                Text = GlobalStrings.EditQuestion;
                OperationButton.Text = GlobalStrings.EditOperation;
                Operation = GlobalStrings.EditOperation;
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
                QuestionOperations.OperationOngoing = true;
                //check if the operation is edit and fill the fields with selected question data
                if (Operation == GlobalStrings.EditOperation)
                {
                    Question tGeneralQuestionData = QuestionOperations.GetQuestionData(QuestionId);
                    //extract question data an add it to UI
                    QuestionTextBox.Text = tGeneralQuestionData.Text;
                    QuestionOrderNumeric.Value = tGeneralQuestionData.Order;
                    QuestionTypeComboBox.SelectedIndex = (int) tGeneralQuestionData.Type;

                    //based on the combobox value further data about the question should be obtained and added to UI

                    Question tQuestionSpecificData = null;

                    //gets the specific data of the question to show it in the form
                    OperationResult tQuestionSpecificDataResult = QuestionOperations.GetQuestionSpecificData(QuestionId, ref tQuestionSpecificData);
                    if (tQuestionSpecificDataResult.IsSuccess)
                    {
                        switch (tQuestionSpecificData.Type)
                        {
                            //for each case Get the info and downcast it and assign it to its respective field
                            case QuestionType.Stars:
                                StarsQuestionOptionsPanel.NumberOfStarsNumeric.Value = ((StarsQuestion)tQuestionSpecificData).NumberOfStars;
                                break;
                            case QuestionType.Smiley:
                                SmileyQuestionOptionsPanel.NumberOfSmileysNumeric.Value = ((SmileyQuestion)tQuestionSpecificData).NumberOfSmileyFaces;
                                break;
                            case QuestionType.Slider:
                                SliderQuestionOptionsPanel.SliderStartValueNumeric.Value = ((SliderQuestion)tQuestionSpecificData).StartValue;
                                SliderQuestionOptionsPanel.SliderEndValueNumeric.Value = ((SliderQuestion)tQuestionSpecificData).EndValue;
                                SliderQuestionOptionsPanel.SliderStartValueCaptionText.Text = ((SliderQuestion)tQuestionSpecificData).StartValueCaption;
                                SliderQuestionOptionsPanel.SliderEndValueCaptionText.Text = ((SliderQuestion)tQuestionSpecificData).EndValueCaption;
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show(tQuestionSpecificDataResult.ErrorMessage, tQuestionSpecificDataResult.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    QuestionType tQuestionType = (QuestionType)QuestionTypeComboBox.SelectedIndex;
                    //encapsulate obtained data in a question object
                    Question tNewQuestionData = new Question(tQuestionText, tQuestionOrder, tQuestionType);

                    OperationResult tQuestionAddedResult = null;
                    //add question to db and interface
                    switch (tQuestionType)
                    {
                        //create a question object based on the chosen type and add it to the database
                        case QuestionType.Stars:
                            StarsQuestion tStarsData = new StarsQuestion(tNewQuestionData, (int)StarsQuestionOptionsPanel.NumberOfStarsNumeric.Value);
                            tQuestionAddedResult = QuestionOperations.AddQuestion(tStarsData);
                            break;
                        case QuestionType.Smiley:
                            SmileyQuestion tSmileyData = new SmileyQuestion(tNewQuestionData, (int)SmileyQuestionOptionsPanel.NumberOfSmileysNumeric.Value);
                            tQuestionAddedResult = QuestionOperations.AddQuestion(tSmileyData);
                            break;
                        case QuestionType.Slider:
                            SliderQuestion tSliderData = new SliderQuestion(
                                tNewQuestionData,
                                (int)SliderQuestionOptionsPanel.SliderStartValueNumeric.Value,
                                (int)SliderQuestionOptionsPanel.SliderEndValueNumeric.Value,
                                SliderQuestionOptionsPanel.SliderStartValueCaptionText.Text,
                                SliderQuestionOptionsPanel.SliderEndValueCaptionText.Text);
                            tQuestionAddedResult = QuestionOperations.AddQuestion(tSliderData);
                            break;
                    }
                    if (tQuestionAddedResult != null && !tQuestionAddedResult.IsSuccess)
                    {
                        MessageBox.Show(tQuestionAddedResult.ErrorMessage, tQuestionAddedResult.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    QuestionType tQuestionType = (QuestionType)QuestionTypeComboBox.SelectedIndex;
                    //encapsulate obtained data in a question object
                    Question tNewQuestionData = new Question(QuestionId, tQuestionText, tQuestionOrder, tQuestionType);

                    OperationResult tQuestionUpdatedResult = null;
                    //add question to db and interface
                    switch (tQuestionType)
                    {
                        //send question data to be updated
                        case QuestionType.Stars:
                            StarsQuestion tStarsData = new StarsQuestion(tNewQuestionData, (int)StarsQuestionOptionsPanel.NumberOfStarsNumeric.Value);
                            tQuestionUpdatedResult = QuestionOperations.UpdateQuestion(tStarsData);
                            break;
                        case QuestionType.Smiley:
                            SmileyQuestion tSmileyData = new SmileyQuestion(tNewQuestionData, (int)SmileyQuestionOptionsPanel.NumberOfSmileysNumeric.Value);
                            tQuestionUpdatedResult = QuestionOperations.UpdateQuestion(tSmileyData);
                            break;
                        case QuestionType.Slider:
                            SliderQuestion tSliderData = new SliderQuestion(
                                tNewQuestionData,
                                (int)SliderQuestionOptionsPanel.SliderStartValueNumeric.Value,
                                (int)SliderQuestionOptionsPanel.SliderEndValueNumeric.Value,
                                SliderQuestionOptionsPanel.SliderStartValueCaptionText.Text,
                                SliderQuestionOptionsPanel.SliderEndValueCaptionText.Text);
                            tQuestionUpdatedResult = QuestionOperations.UpdateQuestion(tSliderData);
                            break;

                    }
                    if (tQuestionUpdatedResult != null && !tQuestionUpdatedResult.IsSuccess)
                    {
                        MessageBox.Show(tQuestionUpdatedResult.ErrorMessage, tQuestionUpdatedResult.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    Close();
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
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QuestionTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                HideQuesitonOptionsPanel();
                QuestionType tSelectedItemValue = (QuestionType)QuestionTypeComboBox.SelectedIndex;
                switch (tSelectedItemValue)
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
                QuestionOperations.OperationOngoing = false;
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
                //StarsQuestionOptionsPanel.Show();
                StarsQuestionOptionsPanel = new StarsQuestionOptions();
                StarsQuestionOptionsPanel.Location = QuestionOptionsPanelLocation;
                Controls.Add(StarsQuestionOptionsPanel);
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
                SmileyQuestionOptionsPanel= new SmileyQuestionOptions();
                SmileyQuestionOptionsPanel.Location = QuestionOptionsPanelLocation;
                Controls.Add(SmileyQuestionOptionsPanel);
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
                SliderQuestionOptionsPanel= new SliderQuestionOptions();
                SliderQuestionOptionsPanel.Location = QuestionOptionsPanelLocation;
                Controls.Add(SliderQuestionOptionsPanel);
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
                Controls.Remove(StarsQuestionOptionsPanel);
                Controls.Remove(SmileyQuestionOptionsPanel);
                Controls.Remove(SliderQuestionOptionsPanel);
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                MainScreen.ShowDefaultErrorMessage();
            }
        }

        #endregion
    }
}

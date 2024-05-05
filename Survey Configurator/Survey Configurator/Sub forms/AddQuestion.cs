using DatabaseLayer.models;
using Logic;
using System;
using System.Windows.Forms;

namespace Survey_Configurator.Sub_forms
{
    public partial class AddQuestion : Form
    {
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

        public AddQuestion()
        {
            InitializeComponent();
        }

        private void AddQuestion_Load(object sender, EventArgs e)
        {
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            //check if all fields are filled properly
            if (QuestionTextBox.Text.Length != 0 &&
                QuestionTypeComboBox.SelectedItem != null)
            {
                //validate the type-specific fields
                //add question to db and interface
                switch (QuestionTypeComboBox.Text)
                {
                    case "Stars":
                        QuestionOperations.AddQuestion(new StarsQuestion(QuestionTextBox.Text, (int)QuestionOrderNumeric.Value,(int)NumberOfStarsNumeric.Value));
                        break;
                    case "Slider":
                        QuestionOperations.AddQuestion(new SliderQuestion(QuestionTextBox.Text, (int)QuestionOrderNumeric.Value,
                            (int)SliderStartValueNumeric.Value, (int)SliderEndValueNumeric.Value,
                            SliderStartValueCaptionText.Text, SliderEndValueCaptionText.Text));
                        break;
                    case "Smiley":
                        QuestionOperations.AddQuestion(new SmileyQuestion(QuestionTextBox.Text, (int)QuestionOrderNumeric.Value, (int)NumberOfSmileysNumeric.Value));
                        break;
                }
                //should i check the result of the add question function ?

                //show success message
                MessageBox.Show("Question has been added successfully!", "Operation successful", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                //close form
                this.Close();
            }
            else
            {
                //show dialouge box indicating an error in filling fields
                //show the missing fields ?
                MessageBox.Show("All fields must have proper values", "Missing fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult cancelCreateQuestion = MessageBox.Show("Are you sure you want to cancel this questions?", "Cancel Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cancelCreateQuestion == DialogResult.Yes)
            {
                this.Close();
            }

        }

        private void QuestionTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            QuestionOptions.Controls.Clear();
            switch (QuestionTypeComboBox.SelectedItem?.ToString()) 
            {
                case "Slider":
                    addSliderOptions();
                    break;
                case "Smiley":
                    addSmileysOptions();
                    break;
                case "Stars":
                    addStarsOptions();
                    break;
            }
        }
    
        private void addStarsOptions()
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
            NumberOfStarsNumeric.Location = new Point(200, 0);
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
        private void addSliderOptions()
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
            SliderStartValueNumeric.Location = new Point(200, 0);
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
            SliderEndValueLabel.Location = new Point(0, 40);
            SliderEndValueLabel.Name = "SliderEndValue";
            SliderEndValueLabel.Size = new Size(45, 25);
            SliderEndValueLabel.TabIndex = 11;
            SliderEndValueLabel.Text = "End value";
            //add a numeric field to specify a number for the smileys
            SliderEndValueNumeric = new NumericUpDown();
            SliderEndValueNumeric.Location = new Point(200, 40);
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
            SliderStartValueCaptionLabel.Location = new Point(0, 80);
            SliderStartValueCaptionLabel.Name = "SliderStartValueCaptionLabels";
            SliderStartValueCaptionLabel.Size = new Size(45, 25);
            SliderStartValueCaptionLabel.TabIndex = 13;
            SliderStartValueCaptionLabel.Text = "Start value caption";
            //add a numeric field to specify a number for the smileys
            SliderStartValueCaptionText = new TextBox();
            SliderStartValueCaptionText.Location = new Point(200, 80);
            SliderStartValueCaptionText.Name = "SliderStartValueCaptionText";
            SliderStartValueCaptionText.Size = new Size(120, 23);
            SliderStartValueCaptionText.TabIndex = 14;
            SliderStartValueCaptionText.Text = "";

            //label end value items
            Label SliderEndValueCaptionLabel = new Label();
            SliderEndValueCaptionLabel.AutoSize = true;
            SliderEndValueCaptionLabel.Font = new Font("Segoe UI", 14.25F);
            SliderEndValueCaptionLabel.Location = new Point(0, 120);
            SliderEndValueCaptionLabel.Name = "SliderEndValueCaptionLabel";
            SliderEndValueCaptionLabel.Size = new Size(45, 25);
            SliderEndValueCaptionLabel.TabIndex = 13;
            SliderEndValueCaptionLabel.Text = "End value caption";
            //add a numeric field to specify a number for the smileys
            SliderEndValueCaptionText = new TextBox();
            SliderEndValueCaptionText.Location = new Point(200, 120);
            SliderEndValueCaptionText.Name = "SliderEndValueCaptionText";
            SliderEndValueCaptionText.Size = new Size(120, 23);
            SliderEndValueCaptionText.TabIndex = 14;
            SliderEndValueCaptionText.Text = "";

            QuestionOptions.Controls.Add(SliderStartValueLabel);
            QuestionOptions.Controls.Add(SliderStartValueNumeric);
            QuestionOptions.Controls.Add(SliderEndValueLabel);
            QuestionOptions.Controls.Add(SliderEndValueNumeric);
            QuestionOptions.Controls.Add(SliderStartValueCaptionLabel);
            QuestionOptions.Controls.Add(SliderStartValueCaptionText);
            QuestionOptions.Controls.Add(SliderEndValueCaptionLabel);
            QuestionOptions.Controls.Add(SliderEndValueCaptionText);
        }
        private void addSmileysOptions()
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
            NumberOfSmileysNumeric.Location = new Point(200, 0);
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
    }
}

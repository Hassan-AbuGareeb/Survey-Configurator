using System;

namespace QuestionDB.models
{
    public class SliderQuestion : Question
    {
        public string Text { get; set; }
        public int Order { get; set; }
        public int StartValue { get; set; }
        public int EndValue { get; set; }
        public string StartValueCaption { get; set; }
        public string EndValueCaption { get; set; }

        public SliderQuestion(string pText, int pOrder, int pStartValue=0, int pEndValue=100, string pStartCaption="Min", string pEndCaption ="Max" ) : base(pText, pOrder)
        {
            StartValue = pStartValue;
            EndValue = pEndValue;
            StartValueCaption = pStartCaption;
            EndValueCaption = pEndCaption;
        }


    }
}

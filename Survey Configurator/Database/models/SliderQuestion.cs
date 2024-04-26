using System;

namespace Database.models
{
    public class SliderQuestion : Question
    {
        public string Text { get; set; }
        public int Order { get; set; }
        public int StartValue { get; set; }
        public int EndValue { get; set; }
        public string StartValueCaption { get; set; }
        public string EndValueCaption { get; set; }

        public SliderQuestion(string text, int order, int startValue=0, int endValue=100, string startCaption="Min", string endCaption ="Max" ) : base(text, order)
        {
            StartValue = startValue;
            EndValue = endValue;
            StartValueCaption = startCaption;
            EndValueCaption = endCaption;
        }


    }
}

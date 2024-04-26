using System;

namespace Database.models
{
    public class StarsQuestion : Question
    {
        public string Text { get; set; }
        public int Order { get; set; }
        public int NumberOfStars { get; set; }

        public StarsQuestion(string text, int order, int numberOfStars = 0) : base(text, order)
        {
            NumberOfStars = numberOfStars;
        }
    }
}

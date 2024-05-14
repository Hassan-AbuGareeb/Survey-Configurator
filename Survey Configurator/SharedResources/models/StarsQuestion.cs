using System;

namespace SharedResources.models
{
    public class StarsQuestion : Question
    {
        public string Text { get; set; }
        public int Order { get; set; }
        public int NumberOfStars { get; set; }

        public StarsQuestion(string pText, int pOrder, int pNumberOfStars = 5) : base(pText, pOrder)
        {
            NumberOfStars = pNumberOfStars;
        }
    }
}

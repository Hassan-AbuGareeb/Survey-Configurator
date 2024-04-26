using System;

namespace Database.models
{
    public class SmileyQuestion : Question
    {
        public string Text { get; set; }
        public int Order { get; set; }
        public int NumberOfSmileyFaces { get; set; }

        public SmileyQuestion(string text, int order, int numberOfSmileyFaces = 2) : base(text, order)
        {
            NumberOfSmileyFaces = numberOfSmileyFaces;
        }
    }
}

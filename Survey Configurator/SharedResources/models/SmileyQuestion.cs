using System;

namespace SharedResources.models
{
    public class SmileyQuestion : Question
    {
        public string Text { get; set; }
        public int Order { get; set; }
        public int NumberOfSmileyFaces { get; set; }

        public SmileyQuestion(string pText, int pOrder, int pNumberOfSmileyFaces = 2) : base(pText, pOrder)
        {
            NumberOfSmileyFaces = pNumberOfSmileyFaces;
        }
    }
}

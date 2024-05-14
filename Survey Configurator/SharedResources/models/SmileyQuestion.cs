using System;

namespace SharedResources.models
{
    public class SmileyQuestion : Question
    {
        public string Text { get; set; }
        public int Order { get; set; }
        public int NumberOfSmileyFaces { get; set; }

        public SmileyQuestion(int pId, string pText, int pOrder, int pNumberOfSmileyFaces = 2) : base(pId, pText, pOrder, QuestionType.Smiley)
        {
            NumberOfSmileyFaces = pNumberOfSmileyFaces;
        }
    }
}

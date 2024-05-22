using System;

namespace SharedResources.models
{


    public  class Question
    {
        /// <summary>
        /// main model for the Question object used in this application
        /// </summary>

        public int Id { get; set; }
        public string Text { get; set; }
        public int Order {  get; set; }
        public QuestionType Type { get; set; }


        public Question(int pId, string pText, int pOrder, QuestionType pType)
        {
            Id = pId;
            Text = pText;
            Order = pOrder;
            Type = pType;
        }
        public Question( string pText, int pOrder, QuestionType pType)
        {
            Text = pText;
            Order = pOrder;
            Type = pType;
        }

    }
}

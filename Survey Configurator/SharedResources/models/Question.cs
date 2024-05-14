using System;

namespace SharedResources.models
{


    public  class Question
    {
        //can't be initialized until the record for the quesiton is creatd in the database
        public int Id { get; set; }
        public string Text { get; set; }
        public int Order {  get; set; }
        public QuestionType Type { get; set; }


        public Question (int pId, string pText, int pOrder, QuestionType pType)
        {
            Id = pId;
            Text = pText;
            Order = pOrder;
            Type = pType;
        }

    }
}

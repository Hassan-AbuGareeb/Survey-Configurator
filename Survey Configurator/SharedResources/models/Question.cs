using System;


namespace SharedResources.models
{


    public abstract class Question
    {
        //can't be initialized until the record for the quesiton is creatd in the database
        public int Id { get; set; }
        public string Text { get; set; }
        public int Order {  get; set; }

        //question type here
        public int Type { get; set; }


        public Question (string pText, int pOrder)
        {
            Text = pText;
            Order = pOrder;
        }

    }
}

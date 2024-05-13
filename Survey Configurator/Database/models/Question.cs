using System;


namespace QuestionDB.models
{


    public abstract class Question
    {
        public string Text { get; set; }
        public int Order {  get; set; }

        public Question (string text, int order)
        {
            Text = text;
            Order = order;
        }

    }
}

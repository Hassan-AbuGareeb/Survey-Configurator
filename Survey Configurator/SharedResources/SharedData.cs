using System;
using System.Collections.Generic;

namespace SharedResources
{
    public enum QuestionType
    {
        Stars=0,
        Smiley=1,
        Slider=2
    }
     public class SharedData
    {
        int value = 0;
        QuestionType nono = (QuestionType)0;
    }
}

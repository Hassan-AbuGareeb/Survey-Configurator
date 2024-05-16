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
        public const QuestionType cStarsType = QuestionType.Stars;
        public const QuestionType cSmileyType = QuestionType.Smiley;
        public const QuestionType cSliderType = QuestionType.Slider;


    }
}

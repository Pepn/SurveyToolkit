using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurveyToolkit
{
    [CreateAssetMenu(fileName = "SliderQuestion", menuName = "SurveyToolkit/SliderQuestion")]
    public class SliderQuestionData : QuestionData
    {
        public string upperBoundText, lowerBoundText;
    }
}
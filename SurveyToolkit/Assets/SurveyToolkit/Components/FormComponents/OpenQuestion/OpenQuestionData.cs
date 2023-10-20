using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurveyToolkit
{
    [CreateAssetMenu(fileName = "OpenQuestion", menuName = "SurveyToolkit/OpenQuestion")]
    public class OpenQuestionData : QuestionData
    {
        public string answer;
    }
}
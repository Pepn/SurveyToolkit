using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurveyToolkit
{
    [CreateAssetMenu(fileName = "MultipleChoiceCheckBox", menuName = "SurveyToolkit/MultipleChoiceCheckBox")]
    public class MultipleChoiceCheckBoxQuestionData : QuestionData
    {
        public List<string> Answers = new List<string>();

    }
}
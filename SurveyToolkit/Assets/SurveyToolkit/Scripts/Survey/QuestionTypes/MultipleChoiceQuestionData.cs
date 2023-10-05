using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MCQ", menuName = "SurveyToolkit/MultipleChoiceQuestion")]
public class MultipleChoiceQuestionData : QuestionData
{
    public List<string> Answers = new List<string>();

}

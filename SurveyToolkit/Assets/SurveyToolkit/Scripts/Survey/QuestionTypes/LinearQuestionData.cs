using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LinearQuestion", menuName = "SurveyToolkit/LinearQuestion")]
public class LinearQuestionData : QuestionData
{
    public List<string> Answers = new List<string>();

}

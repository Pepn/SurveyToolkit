using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurveyToolkit
{
    [CreateAssetMenu(fileName = "GridQuestion", menuName = "SurveyToolkit/GridQuestion")]
    public class GridQuestionData : QuestionData
    {
        public List<string> Questions = new List<string>();
        public List<string> ColumnNames = new List<string>();

    }
}
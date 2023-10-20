using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SurveyToolkit
{
    public abstract class QuestionData : FormObjectData
    {
        [SerializeField] public GameObject QuestionPrefab;
        public string question;
        public bool required = true;
    }
}
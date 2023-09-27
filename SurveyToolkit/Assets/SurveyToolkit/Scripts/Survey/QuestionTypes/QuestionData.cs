using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class QuestionData : FormObjectData
{
    [SerializeField] public GameObject QuestionPrefab;
    public string question;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InfoForm", menuName = "SurveyToolkit/InfoForm")]
public class InfoFormData : FormObjectData
{
    public GameObject InfoFormPrefab;
    public string title;
    [TextArea(5, 10)]
    public string info;
}

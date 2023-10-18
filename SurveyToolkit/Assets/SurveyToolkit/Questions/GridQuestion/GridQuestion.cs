using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class GridQuestion : Question
{
    [SerializeField] protected GameObject rowPrefab;
    [SerializeField] public GameObject TopRowPrefab;
    public override string GetAnswer()
    {
        string answers = "";
        foreach(Transform row in answerObject.transform)
        {
            //skip the first question row
            if (row.GetComponentInChildren<Toggle>())
            {
                string column = GetRowAnswer(row);
                answers += $"{column},";   
            }
        }

        //remove last comma
        return answers.Remove(answers.Length - 1, 1);
    }

    private string GetRowAnswer(Transform row)
    {
        int index = 0;
        for(int i = 0; i < row.childCount; ++i)
        {
            //skip the questionpart
            if (row.GetChild(i).GetComponentInChildren<Toggle>() == null)
                continue;

            if(row.GetChild(i).GetComponentInChildren<Toggle>().isOn)
            {
                return (GetData() as GridQuestionData).ColumnNames[index].Replace(',','/');
            }
            else if(!row.GetChild(i).GetComponentInChildren<Toggle>().isOn)
            {
                index++;
            }
        }

        //Debug.LogError("Found no Toggle on in this row..");
        return "";
    }

    public override List<GameObject> GetInCompletedForms()
    {
        List<GameObject> inCompleteObjects = new List<GameObject>();
        foreach (Transform row in answerObject.transform)
        {
            //skip the first question row
            if (row.GetComponentInChildren<Toggle>())
            {
                if (GetRowAnswer(row) == "")
                {
                    inCompleteObjects.Add(row.gameObject);
                }
            }
        }

        return inCompleteObjects;
    }

    public override string GetQuestion()
    {
        string s = "";
        foreach (string question in (GetData() as GridQuestionData).Questions)
        {
            s += $"[{question.Replace(',', '/')}],";
        }

        return s.Remove(s.Length - 1, 1); //idk remove weird suffix
    }

    void Start()
    {
        questionObject.GetComponent<TextMeshProUGUI>().text = GetData().question + this.AddRequiredOptionalStar;

        //toprow of columns
        GameObject topRowObj = Instantiate(TopRowPrefab, answerObject.transform);
        int index = 0;
        foreach(TextMeshProUGUI text in topRowObj.GetComponentsInChildren<TextMeshProUGUI>())
        {
            text.text = (GetData() as GridQuestionData).ColumnNames[index];
            index++;
        }

        foreach (string question in (GetData() as GridQuestionData).Questions)
        {
            GameObject rowObj = Instantiate(rowPrefab, answerObject.transform);

            //set togglegroups per row
            foreach(Toggle t in rowObj.GetComponentsInChildren<Toggle>())
            {
                t.group = rowObj.GetComponent<ToggleGroup>();
            }

            rowObj.GetComponentInChildren<TextMeshProUGUI>().text = question;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

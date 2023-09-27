using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OpenQuestion : Question
{
    private TMP_InputField answerField;

    // Start is called before the first frame update
    void Start()
    {
        questionObject.GetComponent<TextMeshProUGUI>().text = (GetData() as OpenQuestionData).question;

        answerField = GetComponentInChildren<TMP_InputField>();
    }

    //public override MultipleChoiceQuestionData GetData() => (MultipleChoiceQuestionData) base.GetData();

    // Update is called once per frame
    void Update()
    {
        
    }

    public override string GetAnswer()
    {
        return answerField.text.Replace(',', '/');
    }

    public override string GetQuestion()
    {
        return GetData().question.Replace(',','/');
    }

    public override List<GameObject> GetInCompletedForms() 
    {
        List<GameObject> inCompleteObjects = new List<GameObject>();
        if (GetAnswer() == "")
        {
            inCompleteObjects.Add(this.gameObject);
        }
        return inCompleteObjects;
    }


}

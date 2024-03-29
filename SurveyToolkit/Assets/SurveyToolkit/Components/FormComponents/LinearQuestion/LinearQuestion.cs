using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

namespace SurveyToolkit
{
    public class LinearQuestion : Question
    {
        [SerializeField] protected GameObject optionPrefab;
        public override string GetAnswer()
        {
            var answer = from gameobj in answerObject.transform.Cast<Transform>().Select(t => t.gameObject)
                         where gameobj.GetComponentInChildren<Toggle>().isOn == true
                         select gameobj;

            //return empty string if not found
            if (answer.ToList().Count == 0)
                return "";
            else
            {
                return answer.ToList()[0].GetComponentInChildren<TextMeshProUGUI>().text;
            }
        }

        public override string GetQuestion()
        {
            return GetData().question;
        }

        void Start()
        {
            questionObject.GetComponent<TextMeshProUGUI>().text = GetData().question + this.AddRequiredOptionalStar;
            foreach (string answer in (GetData() as LinearQuestionData).Answers)
            {
                GameObject optionObj = Instantiate(optionPrefab, answerObject.transform);
                optionObj.GetComponentInChildren<Toggle>().group = answerObject.GetComponent<ToggleGroup>();
                optionObj.GetComponentInChildren<TextMeshProUGUI>().text = answer;
            }
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
}
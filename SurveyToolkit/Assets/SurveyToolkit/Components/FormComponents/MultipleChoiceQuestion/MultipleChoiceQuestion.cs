using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SurveyToolkit
{
    public class MultipleChoiceQuestion : Question
    {
        [SerializeField] protected GameObject optionPrefab;

        // Start is called before the first frame update
        void Start()
        {
            questionObject.GetComponent<TextMeshProUGUI>().text = GetData().question + this.AddRequiredOptionalStar;
            foreach (string answer in (GetData() as MultipleChoiceQuestionData).Answers)
            {
                GameObject optionObj = Instantiate(optionPrefab, answerObject.transform);
                optionObj.GetComponentInChildren<Toggle>().group = answerObject.GetComponent<ToggleGroup>();
                optionObj.GetComponentInChildren<TextMeshProUGUI>().text = answer;
            }
        }

        public override string GetAnswer()
        {
            string answer = "";
            foreach (Toggle t in answerObject.GetComponentsInChildren<Toggle>())
            {
                if (t.isOn)
                {
                    answer += t.gameObject.GetComponentInChildren<TextMeshProUGUI>().text;
                }
            }
            return answer;
        }

        public override string GetQuestion()
        {
            return GetData().question.Replace(',', '/');
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
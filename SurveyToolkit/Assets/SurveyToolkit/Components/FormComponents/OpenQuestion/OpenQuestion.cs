using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SurveyToolkit
{
    public class OpenQuestion : Question
    {
        private TMP_InputField answerField;

        // Start is called before the first frame update
        void Start()
        {
            questionObject.GetComponent<TextMeshProUGUI>().text = (GetData() as OpenQuestionData).question + this.AddRequiredOptionalStar;

            answerField = GetComponentInChildren<TMP_InputField>();
        }

        /// <summary>
        /// Remove comma's as these mess up the csv file
        /// </summary>
        /// <returns></returns>
        public override string GetAnswer()
        {
            return answerField.text.Replace(',', '/');
        }

        /// <summary>
        /// Remove comma's as these mess up the csv file
        /// </summary>
        /// <returns></returns>
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
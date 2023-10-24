using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

namespace SurveyToolkit
{
    public class MultipleChoiceCheckBoxQuestion : Question
    {
        [SerializeField] protected GameObject optionPrefab;

        void Start()
        {
            questionObject.GetComponent<TextMeshProUGUI>().text = GetData().question + this.AddRequiredOptionalStar;
            foreach (string answer in (GetData() as MultipleChoiceCheckBoxQuestionData).Answers)
            {
                GameObject optionObj = Instantiate(optionPrefab, answerObject.transform);
                optionObj.GetComponentInChildren<TextMeshProUGUI>().text = answer;
            }
        }

        /// <summary>
        /// Report for each multiple choice checkbox if its checked or not
        /// </summary>
        /// <returns></returns>
        public override string GetAnswer()
        {
            string answer = "";
            foreach (Toggle t in answerObject.GetComponentsInChildren<Toggle>())
            {
                answer += $"{t.isOn.ToString()},";
            }
            return answer.Remove(answer.Length - 1);
        }

        /// <summary>
        /// To make sure the number of output columns remain equal add all the options to the output and just
        /// indicate with true of false if the checkbox was selected.
        /// </summary>
        /// <returns></returns>
        public override string GetQuestion()
        {
            string questions = "";
            foreach (string answer in (GetData() as MultipleChoiceCheckBoxQuestionData).Answers)
            {
                questions += $"{GetData().question.Replace(',', '/')}[{answer.Replace(',', '/')}],";
            }

            return questions.Remove(questions.Length - 1);
        }

        /// <summary>
        /// If required; atleast one options must be checked (so true)
        /// </summary>
        /// <returns></returns>
        public override List<GameObject> GetInCompletedForms()
        {
            List<GameObject> inCompleteObjects = new List<GameObject>();
            if (!GetAnswer().Contains(true.ToString()))
            {
                inCompleteObjects.Add(this.gameObject);
            }
            return inCompleteObjects;
        }
    }
}
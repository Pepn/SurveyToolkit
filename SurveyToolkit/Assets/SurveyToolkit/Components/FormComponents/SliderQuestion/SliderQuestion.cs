using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SurveyToolkit
{
    public class SliderQuestion : Question
    {
        private Slider answerSlider;

        [SerializeField] TextMeshProUGUI lowerBoundText, upperBoundText;

        // Start is called before the first frame update
        void Start()
        {
            questionObject.GetComponent<TextMeshProUGUI>().text = (GetData() as SliderQuestionData).question + this.AddRequiredOptionalStar;

            answerSlider = GetComponentInChildren<Slider>();
            lowerBoundText.text = (GetData() as SliderQuestionData).lowerBoundText;
            upperBoundText.text = (GetData() as SliderQuestionData).upperBoundText;
        }

        public override string GetAnswer()
        {
            return answerSlider.value.ToString();
        }

        public override string GetQuestion()
        {
            return GetData().question.Replace(',', '/');
        }

        public override List<GameObject> GetInCompletedForms()
        {
            List<GameObject> inCompleteObjects = new List<GameObject>();
            
            // future update check if the slider was clicked on

            return inCompleteObjects;
        }
    }
}
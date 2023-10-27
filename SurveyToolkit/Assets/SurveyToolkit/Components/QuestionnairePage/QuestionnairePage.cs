using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace SurveyToolkit
{
    /// <summary>
    /// Defines a single page for the survey manager, is built of formObjects.
    /// </summary>
    public class QuestionnairePage : MonoBehaviour
    {
        private SurveyManager surveyManager;
        public List<FormObject> formObjects { get; private set; } = new List<FormObject>();
        public List<FormObjectData> formData = new List<FormObjectData>();
        private Transform container;
        private List<GameObject> inCompleteQuestions = new List<GameObject>();
        private List<Color> inCompleteQuestionsDefaultColor = new List<Color>();

        public UnityEvent OnPageShow;

        private void Awake()
        {
            container = transform.GetChild(0).transform;
            surveyManager = FindObjectOfType<SurveyManager>();
        }

        void Start()
        {
            LoadPage();
            OnPageShow?.Invoke();
        }

        // loads all the questions and instantiates the correct prefabs
        public void LoadPage()
        {
            foreach (FormObjectData formData in formData)
            {
                if (formData is QuestionData)
                {
                    QuestionData qd = formData as QuestionData;
                    Question q = Instantiate(qd.QuestionPrefab, container).GetComponent<Question>();
                    q.SetData(qd);
                    formObjects.Add(q);
                }

                if (formData is InfoFormData)
                {
                    InfoFormData ifd = formData as InfoFormData;
                    InfoForm infoForm = Instantiate(ifd.InfoFormPrefab, container).GetComponent<InfoForm>();
                    infoForm.SetData(ifd);
                    formObjects.Add(infoForm);
                }

                if (formData is SubmitFormData)
                {
                    SubmitFormData sfd = formData as SubmitFormData;
                    SubmitForm submitForm = Instantiate(sfd.InfoFormPrefab, container).GetComponent<SubmitForm>();
                    formObjects.Add(submitForm);

                    submitForm.transform.SetAsLastSibling();
                    submitForm.GetComponentInChildren<Button>()?.onClick.AddListener(() => SubmitPage(submitForm));

                    SetSubmitFormData(submitForm);
                }
            }
        }

        // Rebuilds the layout; fixes some weird autoscaling issues.
        void Update()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponentInChildren<Transform>() as RectTransform);
        }

        // automatically update Submit button text based on the number of pages left
        void SetSubmitFormData(SubmitForm submitForm)
        {
            if(surveyManager.Pages.IndexOf(this) < surveyManager.Pages.Count - 2)
            {
                submitForm.title.text = "Continue on the next Page..";
                submitForm.buttonText.text = "Next Page";
            }
            else
            {
                submitForm.title.text = "Survey Complete!";
                submitForm.buttonText.text = "Submit";
            }
        }

        /// <summary>
        /// Checks if the questions are filled in and goes to the nextpage
        /// </summary>
        /// <param name="submitForm"></param>
        void SubmitPage(SubmitForm submitForm)
        {
            // update the form colors to their default
            ResetFormColors();

            // get all forms that are required
            foreach (Question q in formObjects.OfType<Question>())
            {
                // skip non required questions
                if (!q.GetData().required)
                {
                    continue;
                }

                inCompleteQuestions.AddRange(q.GetInCompletedForms());
            }

            // color them red
            if (surveyManager.MustCompleteAllQuestions || inCompleteQuestions.Count != 0)
            {
                foreach (GameObject obj in inCompleteQuestions)
                {
                    inCompleteQuestionsDefaultColor.Add(obj.GetComponent<Image>().color);
                    obj.GetComponent<Image>().color = new Color(0.8018868f, 0.1927287f, 0.1626469f, 0.7294118f);
                }

                submitForm.GetComponentInChildren<TextMeshProUGUI>().text = "Please fill in the remaining parts.";
                return;
            }

            surveyManager.GoNext(this);
        }

        private void ResetFormColors()
        {
            //remove red color from colored objects
            for (int i = 0; i < inCompleteQuestions.Count; ++i)
            {
                inCompleteQuestions[i].GetComponent<Image>().color = inCompleteQuestionsDefaultColor[i];
            }

            inCompleteQuestions.Clear();
            inCompleteQuestionsDefaultColor.Clear();
        }
    }
}

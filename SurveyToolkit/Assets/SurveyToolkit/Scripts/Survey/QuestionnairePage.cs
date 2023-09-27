using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Text;
using System;
using UnityEngine.UI;
using System.IO;
using System.Net;
using TMPro;

public class QuestionnairePage : MonoBehaviour
{
    private SurveyManager surveyManager;
    public List<FormObject> formObjects = new List<FormObject>();
    public List<FormObjectData> formData = new List<FormObjectData>();
    [SerializeField] private Transform container;
    [SerializeField] private GameObject submitForm;

    private List<GameObject> inCompleteQuestions = new List<GameObject>();
    private List<Color> inCompleteQuestionsDefaultColor = new List<Color>();

    void Start()
    {
        surveyManager = transform.parent.GetComponent<SurveyManager>();
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
        }

        submitForm.transform.SetAsLastSibling();

        if (surveyManager == null)
            return;

        submitForm.GetComponentInChildren<Button>()?.onClick.AddListener(() => SubmitPage());
        UpdateSubmitForm();

    }

    // Update is called once per frame
    void Update()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponentInChildren<Transform>() as RectTransform);
    }

    void UpdateSubmitForm()
    {
        if(surveyManager.Pages.IndexOf(this) < surveyManager.Pages.Count - 2)
        {
            submitForm.GetComponentInChildren<TextMeshProUGUI>().text = "Continue on the next Page..";
            submitForm.GetComponentInChildren<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "Next Page";
        }
    }

    void SubmitPage()
    {
        ResetFormColors();

        foreach (Question q in formObjects.OfType<Question>())
        {
            inCompleteQuestions.AddRange(q.GetInCompletedForms());
        }

        if (surveyManager.MustCompleteAllQuestions && inCompleteQuestions.Count != 0)
        {
            foreach (GameObject obj in inCompleteQuestions)
            {
                inCompleteQuestionsDefaultColor.Add(obj.GetComponent<Image>().color);
                obj.GetComponent<Image>().color = new Color(0.8018868f, 0.1927287f, 0.1626469f, 0.7294118f);
            }

            submitForm.GetComponentInChildren<TextMeshProUGUI>().text = "Please fill in the remaining parts.";
            return;
        }

        //Debug.Log("Go next apge.. ");
        surveyManager.GoNextPage(this);
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

using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Text;
using System;
using System.IO;

namespace SurveyToolkit
{
    [ExecuteAlways]
    public class SurveyManager : MonoBehaviour
    {
        public bool MustCompleteAllQuestions = false;
        [SerializeField] bool UploadToServer;
        [SerializeField] string UploadLink;

        public List<QuestionnairePage> Pages { get; private set; }

        // Start is called before the first frame update
        void Awake()
        {
            LoadSurvey();
        }

        /// <summary>
        /// Finds the QuestionnairePages in the children and create references. Disable all pages except the first.
        /// </summary>
        public void LoadSurvey()
        {
            Pages = new List<QuestionnairePage>();

            //load pages from editor
            foreach (QuestionnairePage page in transform.GetComponentsInChildren<QuestionnairePage>(true))
            {
                Pages.Add(page);
            }

            //extra safety turning everything off except first page
            for (int i = 0; i < Pages.Count; ++i)
            {
                if (i == 0)
                    Pages[i].gameObject.SetActive(true);
                else
                    Pages[i].gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Goes to the next page or submits the questionnaire if this was the last page.
        /// </summary>
        /// <param name="page"></param>
        public void GoNext(QuestionnairePage page)
        {
            int currentIndex = Pages.IndexOf(page);
            if (currentIndex < Pages.Count - 1)
            {
                page.gameObject.SetActive(false);
                Pages[currentIndex + 1].gameObject.SetActive(true);
            }
            else
            {
                SubmitQuestionnaire();
            }
        }

        void SubmitQuestionnaire()
        {
            //if correctly filled in
            string file = SaveStringToFile();

            if (UploadToServer)
            {
                DataUploader.Instance.uploadLink = UploadLink;
                DataUploader.Instance.UploadFile(file);
            }
        }

        private string SaveStringToFile()
        {
            string filename = $"id_.csv";
            string path = Path.Combine(Application.persistentDataPath, filename);
            File.WriteAllText(path, ToCSV());

            return path;
        }

        /// <summary>
        /// Converts the survey questions to a single row CSV file.
        /// </summary>
        /// <returns></returns>
        public string ToCSV()
        {
            StringBuilder results = new StringBuilder();

            List<FormObject> formObjects = new List<FormObject>();
            foreach (QuestionnairePage page in Pages)
            {
                formObjects.AddRange(page.formObjects);
            }

            //top row
            results.Append($"Timestamp,");
            foreach (Question q in formObjects.OfType<Question>())
            {
                results.Append($"{q.GetQuestion()},");
            }
            //results.Append("\n");
            results.Clear();
            //datam
            results.Append($"{DateTime.Now},");
            foreach (Question q in formObjects.OfType<Question>())
            {
                results.Append($"{q.GetAnswer()},");
            }

            return results.ToString();
        }
    }
}


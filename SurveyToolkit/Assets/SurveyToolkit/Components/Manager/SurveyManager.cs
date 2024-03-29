using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Text;
using System;
using System.IO;

namespace SurveyToolkit
{
    public class SurveyManager : MonoBehaviour
    {
        // if true all questions are mandatory
        [field: SerializeField] public bool MustCompleteAllQuestions { get; private set; } = false;

        [SerializeField] bool uploadToServer;

        public List<QuestionnairePage> Pages { get; private set; }

        private DataUploader uploader;

        // Start is called before the first frame update
        void Awake()
        {
            uploader = GetComponent<DataUploader>();
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

            // if last to one we submit everything (last page just shows information)
            if (currentIndex < Pages.Count - 1)
            {
                SubmitQuestionnaire();
            }
        }

        void SubmitQuestionnaire()
        {
            //if correctly filled in
            string file = SaveStringToFile();

            if (uploadToServer)
            {
                if(uploader == null)
                {
                    Debug.LogWarning("No uploader found, make sure to add a DataUploader to this gameobject.");
                }
                else
                {
                    uploader.UploadFile(file);
                }
            }
        }

        private string SaveStringToFile()
        {
            string filename = $"id_{System.Guid.NewGuid()}_{DateTime.UtcNow.ToString("yyyy-dd-M--HH-mm-ss")}.csv";
            string path = Path.Combine(Application.persistentDataPath, filename);
            File.WriteAllText(path, ToCSV());

            return path;
        }

        /// <summary>
        /// Converts the survey questions to a CSV file. One row for the questions, and the 2nd row for the results.
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
            results.Append("\n");
            //results.Clear();
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


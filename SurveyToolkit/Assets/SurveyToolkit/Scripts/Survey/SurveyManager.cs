using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Text;
using System;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class SurveyManager : MonoBehaviour
{
    public bool MustCompleteAllQuestions = false;
    [SerializeField] bool UploadToServer;
    [SerializeField] string UploadLink;

    [field: SerializeField] public List<QuestionnairePage> Pages { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        //load pages from editor
        foreach(QuestionnairePage page in transform.GetComponentsInChildren<QuestionnairePage>(true))
        {
            Pages.Add(page);
        }

        //extra safety turning everything off except first page
        for(int i = 0; i < Pages.Count; ++i)
        {
            if (i == 0)
                Pages[i].gameObject.SetActive(true);
            else
                Pages[i].gameObject.SetActive(false);
        }
    }

    //public string GetHashedIP()
    //{
    //    // Getting host name
    //    string host = Dns.GetHostName();
    //
    //    // Getting ip address using host name
    //    IPHostEntry ip = Dns.GetHostEntry(host);
    //    string ipStr = ip.AddressList[0].ToString();
    //    ipStr = ipStr.GetHashCode().ToString();
    //
    //    return ipStr;
    //}

    // Update is called once per frame
    void Update()
    {

    }

    public void GoNextPage(QuestionnairePage page)
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

    public string ToCSV()
    {
        StringBuilder results = new StringBuilder();

        List<FormObject> formObjects = new List<FormObject>();
        foreach(QuestionnairePage page in Pages)
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
        results.Append($"{ DateTime.Now},");
        foreach (Question q in formObjects.OfType<Question>())
        {
            results.Append($"{q.GetAnswer()},");
        }

        return results.ToString();
    }


}


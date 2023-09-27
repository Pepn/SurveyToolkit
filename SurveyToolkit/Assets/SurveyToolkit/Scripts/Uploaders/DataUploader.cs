using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class DataUploader : Singleton<DataUploader>
{
    [Tooltip("Full link to getFile.php")]
    public string uploadLink;
    public enum UploadStatus
    {
        notStarted,
        started,
        successful,
        error,
        completed
    }
    public UploadStatus uploadStatus;

    public void Start()
    {
        uploadStatus = UploadStatus.notStarted;

        //UploadFile();
    }

    public void UploadFile(string file)
    {
        //Debug.Log("Start uploading File..");
        StartCoroutine(Upload(file));
    }

    IEnumerator Upload(string file)
    {
        uploadStatus = UploadStatus.started;

        string path = file;// Path.Combine(Application.persistentDataPath, "results", "Test.csv");
        //Debug.Log("Uploading File to Path: " + path);
        WWWForm form = new WWWForm();
        byte[] dataFile = File.ReadAllBytes(path);
        form.AddBinaryData("dataFile", dataFile, System.IO.Path.GetFileName(path));
        UnityWebRequest req = UnityWebRequest.Post(uploadLink, form);
        yield return req.SendWebRequest();

        uploadStatus = UploadStatus.completed;

        Debug.Log("SERVER: " + req.downloadHandler.text); // server response

        if (req.result == UnityWebRequest.Result.ProtocolError || req.result == UnityWebRequest.Result.ConnectionError || !(req.downloadHandler.text.Contains("FILE OK")))
            uploadStatus = UploadStatus.error;
        else
            uploadStatus = UploadStatus.successful;

        yield break;
    }
}
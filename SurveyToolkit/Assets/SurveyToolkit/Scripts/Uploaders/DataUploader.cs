using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Data uploader, takes a path to a file and tries to upload it to the uploadLink.
/// </summary>
public class DataUploader : Singleton<DataUploader>
{
    [Tooltip("Full link to getFile.php")]
    public string uploadLink;

    public void UploadFile(string file)
    {
        StartCoroutine(Upload(file));
    }

    IEnumerator Upload(string file)
    {
        string path = file;
        Debug.Log("Uploading File from Path: " + path);
        WWWForm form = new WWWForm();
        byte[] dataFile = File.ReadAllBytes(path);
        form.AddBinaryData("dataFile", dataFile, System.IO.Path.GetFileName(path));
        UnityWebRequest req = UnityWebRequest.Post(uploadLink, form);

        yield return req.SendWebRequest();

        Debug.Log("Server Response: " + req.downloadHandler.text); 

        if (req.result == UnityWebRequest.Result.ProtocolError || req.result == UnityWebRequest.Result.ConnectionError || !(req.downloadHandler.text.Contains("FILE OK")))
        {
            Debug.Log("File Upload Succesful!");
        }
        else
        {
            Debug.Log("File Upload Failed..");
        }

        yield break;
    }
}
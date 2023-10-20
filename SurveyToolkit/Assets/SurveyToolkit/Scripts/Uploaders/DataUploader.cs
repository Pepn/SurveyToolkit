using System.Collections;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

namespace SurveyToolkit
{
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

            // set password (unsafe)
            req.SetRequestHeader("AUTH", "XZ89O6RfnsxKFHxERJ3aKh2BDXrb0dKB");

            yield return req.SendWebRequest();

            Debug.Log("Server Response: " + req.downloadHandler.text);

            if (req.result == UnityWebRequest.Result.ProtocolError || req.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("File Upload Failed..");
            }
            else
            {
                Debug.Log("File Upload Succesful!");
            }

            yield break;
        }
    }
}

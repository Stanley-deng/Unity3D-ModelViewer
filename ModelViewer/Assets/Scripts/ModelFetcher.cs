using System.Collections;
using UnityEngine;
using System;

using UnityEngine.Networking;
using System.IO;

public class ModelFetcher : MonoBehaviour {

    // this is a temp method as in the future Download3DModel should be called by the teams client 
    void Update() {
        if (Input.GetKeyDown("t")) {
            Download3DModel();
        }
    }

    //public void Download3DModel(string hid)
    public void Download3DModel() {
        //string url = $"https://organsegmentation-storageaccessor-app.azurewebsites.net/api/v1/holograms/{hid}/download";
        // current hardcoded url    
        string url = "https://organsegmentation-storageaccessor-app.azurewebsites.net/api/v1/holograms/04bcb170b8c9846c1830a29b4b465bbd/download";
        string fileName = "test.glb";

        StartCoroutine(DownloadFile(url, fileName));
    }

    private IEnumerator DownloadFile(string url, string filePath) {
        UnityWebRequest webRequest = UnityWebRequest.Get(url);

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.Success) {
            byte[] content = webRequest.downloadHandler.data;

            string persistentPath = "./Assets/Resources/Models/" + filePath;
            File.WriteAllBytes(persistentPath, content);

            Debug.Log("File downloaded successfully.");

            //LoadModel(persistentPath);

            // Display the file path and file size
            FileInfo fileInfo = new FileInfo(persistentPath);
            Debug.Log($"File path: {fileInfo.FullName}");
            Debug.Log($"File size: {fileInfo.Length} bytes");
            ConvertGLBToString(persistentPath); // for debugging purposes, should be removed in the future  
        } else {
            Debug.Log($"Failed to download file. Error: {webRequest.error}");
        }
    }

    // for debugging purposes, should be removed in the future  
    private void ConvertGLBToString(string filePath) {
        if (File.Exists(filePath)) {
            byte[] glbBytes = File.ReadAllBytes(filePath);
            string glbString = Convert.ToBase64String(glbBytes);
            Debug.Log("GLB length as Base64 string:\n" + glbString.Length.ToString());
        } else {
            Debug.LogError("GLB file not found: " + filePath);
        }
    }
}

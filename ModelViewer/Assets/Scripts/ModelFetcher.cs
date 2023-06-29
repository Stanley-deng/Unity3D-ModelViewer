using System.Collections;
using UnityEngine;
using System;

using UnityEngine.Networking;
using System.IO;
using Siccity.GLTFUtility;

public class ModelFetcher : MonoBehaviour {
    // Config
    [SerializeField] float modelScale = 0.03f;

    // Params
    String fileName = "test.glb";
    String persistentPath;


    // Cache
    ModelRotationController controller;

    public void Start() {
        controller = GetComponent<ModelRotationController>();
        persistentPath = Application.persistentDataPath;
        Debug.Log(persistentPath);
    }

    //public void Download3DModel(string hid)
    public void Download3DModel() {
        //string url = $"https://organsegmentation-storageaccessor-app.azurewebsites.net/api/v1/holograms/{hid}/download";
        //fileName = hid
        // current hardcoded url    
        string url = "https://organsegmentation-storageaccessor-app.azurewebsites.net/api/v1/holograms/04bcb170b8c9846c1830a29b4b465bbd/download";
   
        StartCoroutine(DownloadFile(url));
    }

    private IEnumerator DownloadFile(string url) {
        String fullPath = Path.Combine(persistentPath, fileName);

        UnityWebRequest webRequest = UnityWebRequest.Get(url);

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.Success) {
            byte[] content = webRequest.downloadHandler.data;

            File.WriteAllBytes(fullPath, content);

            Debug.Log("File downloaded successfully.");

            // Display the file path and file size
            FileInfo fileInfo = new FileInfo(fullPath);
            Debug.Log($"File path: {fileInfo.FullName}");
            Debug.Log($"File size: {fileInfo.Length} bytes");
            ConvertGLBToString(fullPath); // for debugging purposes, should be removed in the future  
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

    public void CreateObject() {
        String fullPath = Path.Combine(persistentPath, fileName);

        // Load the GLB file from the Resources folder
        GameObject targetModel = Importer.LoadFromFile(fullPath);

        // Check if the Model was loaded successfully
        if (targetModel == null) {
            Debug.LogError("Failed to load object from Resources folder.");
            return;
        }



        // Destroy Current Target Model
        Destroy(GameObject.Find("Target Model"));
        controller.target = targetModel;

        // Set Model Properties
        targetModel.name = "Target Model";
        targetModel.transform.localScale *= modelScale;


        // Set this as Parent of Model
        targetModel.transform.parent = transform;
    }

    // this is a temp method as in the future Download3DModel should be called by the teams client 
    void Update() {
        if (Input.GetKeyDown("t")) {
            Download3DModel();
        }
        if (Input.GetKeyDown("y")) {
            CreateObject();
        }
    }
}

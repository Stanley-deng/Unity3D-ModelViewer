using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

using UnityEngine.Networking;
using System.IO;
using Dummiesman;
using System.Runtime.InteropServices;

public class ModelFetcher : MonoBehaviour {

    // Config
    [SerializeField] float modelScale = 0.03f;
    //[SerializeField] public GameObject LoadingScreen;
    [SerializeField] Image ProgressBar;
    [SerializeField] Image Background;
    [SerializeField] GameObject LoadingText;

    // Params
    string fileName;
    string persistentPath;
    string fullPath;

    string hid;


    // Cache
    ModelRotationController controller;

    //JS Interface
    [DllImport("__Internal")]
    private static extern int SyncDownload(string pHid);
    [DllImport("__Internal")]
    private static extern int SyncLoad(string pHid);

    public void Start() {
        controller = GetComponent<ModelRotationController>();
        persistentPath = Application.persistentDataPath;
        Debug.Log(persistentPath);

        //Placeholder Hardcode
        hid = "070576dc360a3621a2c1e256b4118a71";
    }

    public void Download3DModel(string pHid = null) {
        // Check hid argument and update
        if (pHid != null)
            hid = pHid;

        // Sync this action through LiveShare
        // try {
        //     SyncDownload(hid);
        // } catch { }

        string url = $"https://organsegmentation-storageaccessor-app.azurewebsites.net/api/v1/holograms/{hid}/download"; 
        fileName = $"{hid}.obj";
        fullPath = Path.Combine(persistentPath, fileName);

        StartCoroutine(DownloadFile(url));
    }

    private IEnumerator DownloadFile(string url) {

        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        ProgressBar.fillAmount = 0.1f;
        yield return webRequest.SendWebRequest();
        ProgressBar.fillAmount = 0.2f;

        if (webRequest.result == UnityWebRequest.Result.Success) {
            byte[] content = webRequest.downloadHandler.data;
            ProgressBar.fillAmount = 0.4f;
            File.WriteAllBytes(fullPath, content);
            ProgressBar.fillAmount = 0.6f;
            Debug.Log("File downloaded successfully.");

            // Display the file path and file size
            FileInfo fileInfo = new FileInfo(fullPath);
            ProgressBar.fillAmount = 0.8f;
            Debug.Log($"File path: {fileInfo.FullName}");
            Debug.Log($"File size: {fileInfo.Length} bytes");
        } else {
            Debug.Log($"Failed to download file. Error: {webRequest.error}");
        }

        LoadModel();
        ProgressBar.fillAmount = 1.0f;

        Destroy(ProgressBar);
        Destroy(Background);
        Destroy(LoadingText);
    }

    

    public void LoadModel(string pHid = null) {
        // Check hid argument and update
        if (pHid != null)
            hid = pHid;

        // Sync this action through LiveShare
        // try {
        //     SyncLoad(hid);
        // } catch { }

        // Update filename to current hid
        fileName = $"{hid}.obj";

        string fullPath = Path.Combine(persistentPath, fileName);

        // Load the OBJ file from the Resources folder
        GameObject targetModel = new OBJLoader().Load(fullPath);

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
            LoadModel();
        }
    }
}

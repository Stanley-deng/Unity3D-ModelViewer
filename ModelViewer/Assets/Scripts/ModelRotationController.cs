using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Runtime.InteropServices;
using System;

//using UnityEngine.Networking;
//using System.IO;

public class ModelRotationController : MonoBehaviour {

    // Config
    [SerializeField] float speed = 0.2f;
    [SerializeField] Vector3 defaultRotation = Vector3.zero;

    // Params
    bool isRotating = false;


    // Cache
    [DllImport("__Internal")]
    private static extern int UpdateCurrentRotation(float x, float y, float z);
    [DllImport("__Internal")]
    private static extern void JSConsoleLog(string str);

    public void ResetRotation() {
        transform.DORotate(defaultRotation, speed, RotateMode.FastBeyond360);
    }

    public void SyncRotation(string jsonRotation) {
        var targetRotation = JsonUtility.FromJson<Vector3>(jsonRotation);
        transform.DORotate(targetRotation, speed, RotateMode.FastBeyond360);

        float[] e = new float[] { targetRotation.x, targetRotation.y, targetRotation.z };
    }

    public void Rotate90(string direction) {
        if (isRotating) {
            return;
        }

        switch (direction) {
            case "up":
                StartCoroutine(Rotate(new Vector3(90, 0, 0)));
                break;
            case "down":
                StartCoroutine(Rotate(new Vector3(-90, 0, 0)));
                break;
            case "left":
                StartCoroutine(Rotate(new Vector3(0, 90, 0)));
                break;
            case "right":
                StartCoroutine(Rotate(new Vector3(0, -90, 0)));
                break;
            case "clock":
                StartCoroutine(Rotate(new Vector3(0, 0, 90)));
                break;
            case "cClock":
                StartCoroutine(Rotate(new Vector3(0, 0, -90)));
                break;
        }

    }

    private IEnumerator Rotate(Vector3 v) {
        isRotating = true;
        Tween myTween = transform.DORotate(v, speed, RotateMode.WorldAxisAdd).SetRelative();
        yield return myTween.WaitForCompletion();
        isRotating = false;

        Vector3 r = transform.localEulerAngles;
        float[] e = new float[] { r.x, r.y, r.z };
        UpdateCurrentRotation(r.x, r.y, r.z);
    }


    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown("w")) {
            Rotate90("up");
        } else if (Input.GetKeyDown("s")) {
            Rotate90("down");
        } else if (Input.GetKeyDown("a")) {
            Rotate90("left");
        } else if (Input.GetKeyDown("d")) {
            Rotate90("right");
        } else if (Input.GetKeyDown("q")) {
            Rotate90("clock");
        } else if (Input.GetKeyDown("e")) {
            Rotate90("cclock");
        } else if (Input.GetKeyDown("space")) {
            ResetRotation();
        } //else if (Input.GetKeyDown("t")) {
        //     get3DModel.Download3DModel();
        // }
    }

    // public void Download3DModel()
    // {
    //     string url = "https://organsegmentation-storageaccessor-app.azurewebsites.net/api/v1/holograms/1867f18b7783921f6d80ab30020596c6/download";
    //     string downloadPath = "downloaded-file.glb";
    //     //string filePath = @"C:\UCL\ms_project\downloaded-file.glb";

    //     StartCoroutine(DownloadFile(url, downloadPath));
    // }

    // private IEnumerator DownloadFile(string url, string filePath)
    // {
    //     UnityWebRequest webRequest = UnityWebRequest.Get(url);

    //     yield return webRequest.SendWebRequest();

    //     if (webRequest.result == UnityWebRequest.Result.Success)
    //     {
    //         byte[] content = webRequest.downloadHandler.data;

    //         string persistentPath = Path.Combine(Application.persistentDataPath, filePath);
    //         File.WriteAllBytes(persistentPath, content);

    //         Debug.Log("File downloaded successfully.");

    //         // Display the file path and file size
    //         FileInfo fileInfo = new FileInfo(persistentPath);
    //         Debug.Log($"File path: {fileInfo.FullName}");
    //         Debug.Log($"File size: {fileInfo.Length} bytes");
    //         ConvertGLBToString(persistentPath);
    //     }
    //     else
    //     {
    //         Debug.Log($"Failed to download file. Error: {webRequest.error}");
    //     }
    // }

    // private void ConvertGLBToString(string filePath)
    // {
    //     if (File.Exists(filePath))
    //     {
    //         string content = ""
    //         byte[] glbBytes = File.ReadAllBytes(filePath);
    //         string glbString = Convert.ToBase64String(glbBytes);
    //         Debug.Log("GLB length as Base64 string:\n" + glbString.Length.ToString());
    //     }
    //     else
    //     {
    //         Debug.LogError("GLB file not found: " + filePath);
    //     }
    // }
}

using Siccity.GLTFUtility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Downloaded
public class ModelLoader : MonoBehaviour {
    // Params
    [SerializeField] float modelScale = 0.03f;

    // Cache
    ModelRotationController controller;

    public void Start() {
        controller = GetComponent<ModelRotationController>();
    }


    public void CreateObject() {
        // Load the GLB file from the Resources folder
        GameObject targetModel = Resources.Load<GameObject>("Models/test");
        GameObject modelInstance = Instantiate(targetModel);

        // Check if the Model was loaded successfully
        if (targetModel == null) {
            Debug.LogError("Failed to load object from Resources folder.");
            return;
        }

        

        // Destroy Current Target Model
        Destroy(GameObject.Find("Target Model"));
        controller.target = modelInstance;

        // Set Model Properties
        modelInstance.name = "Target Model";
        modelInstance.transform.localScale *= modelScale;


        // Set this as Parent of Model
        modelInstance.transform.parent = transform;
    }

    void Update() {
        if (Input.GetKeyDown("y")) {
            CreateObject();
        }
    }
}


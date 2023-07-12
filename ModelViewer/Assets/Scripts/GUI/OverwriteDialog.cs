using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverwriteDialog : MonoBehaviour {
    // Config
    [SerializeField] ButtonHandler buttonHandler;

    // Params
    private int index;
    public int Index { get => index; set => index = value; }

    public void OnConfirmButtonPressed() {
        Debug.Log("Confirm Pressed");
        buttonHandler.SetSaveActive(false);
        buttonHandler.HandleOverwrite();
        Destroy(gameObject);
    }

    public void OnCancelButtonPressed() {
        Debug.Log("Cancel Pressed");
        buttonHandler.SetSaveActive(false);
        Destroy(gameObject);
    }
}

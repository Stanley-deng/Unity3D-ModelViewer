using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaypointButton : MonoBehaviour {
    // Config
    [SerializeField] ButtonHandler buttonHandler;

    // Params
    private int index;
    private Vector3 rotation;

    public int Index { get => index; set => index = value; }
    public Vector3 Rotation { get => rotation; set => rotation = value; }

    public void OnClick() {
        buttonHandler.OnWaypointButtonPressed(Index);
    }
}

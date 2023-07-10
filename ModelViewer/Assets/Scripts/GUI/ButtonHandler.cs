using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonHandler : MonoBehaviour {
    // Config
    [SerializeField] WaypointButton waypointButtonPrefab;
    [SerializeField] OverwriteDialog overwriteDialogPrefab;
    [SerializeField] Button addButtonPrefab;
    [SerializeField] Button saveButton;

    [SerializeField] int maxWaypoints;
    

    // Params
    bool saveActive = false;
    int waypointCount = 0;
    int currentWaypoint = 0;
    Button addButton;


    private void SetSaveActive(bool state) {
        saveActive = state;

        if (saveActive) {
            saveButton.GetComponent<Image>().color = Color.yellow;
        } else {
            saveButton.GetComponent<Image>().color = Color.white;
        }
    }

    public void OnSaveButtonPressed() {
        // If Save mode is active, cancel it
        if (saveActive) {
            SetSaveActive(false);
            Destroy(GameObject.Find($"Add {waypointCount + 1}"));
            return;
        }

        // Activate the Save mode state
        SetSaveActive(true);

        // Check if maxSaves is exceeded
        if (waypointCount >= maxWaypoints) {
            return;
        }

        // Spawn the Add Button
        addButton = Instantiate(addButtonPrefab);
        addButton.transform.SetParent(saveButton.transform);

        // Position Add Button relative to current waypoint count
        Vector3 posSpacing = new Vector3(0, -74, 0);
        Vector3 finalPos = posSpacing * (waypointCount + 1);
        addButton.transform.localPosition = finalPos;

        // Name Button
        addButton.name = $"Add {waypointCount + 1}";
    }

    public void OnWaypointButtonPressed(int index) {
        currentWaypoint = index;

        if (saveActive) {
            HandleOverwrite();
        }

        Debug.Log(index);
    }

    public void HandleOverwrite() {
        GameObject parentWaypoint = GameObject.Find($"Waypoint {currentWaypoint}");

        OverwriteDialog newDialog = Instantiate(overwriteDialogPrefab);
        newDialog.Index = waypointCount;
        newDialog.transform.SetParent(parentWaypoint.transform);
        newDialog.transform.position = parentWaypoint.transform.position + new Vector3(-111,50,0);
        newDialog.name = $"Overwrite {currentWaypoint}";
    }

    public void OnAddButtonPressed() {
        // Deactivate Save Mode
        SetSaveActive(false);
        // Increment Number of Waypoints
        waypointCount += 1;

        // Replace Add Button by Waypoint
        WaypointButton newWaypoint = Instantiate(waypointButtonPrefab);
        newWaypoint.Index = waypointCount;
        newWaypoint.GetComponentInChildren<TMP_Text>().text = (waypointCount).ToString();
        newWaypoint.transform.position = addButton.transform.position;
        newWaypoint.transform.SetParent(saveButton.transform);
        newWaypoint.name = $"Waypoint {waypointCount}";


        Destroy(GameObject.Find($"Add {waypointCount}"));
    }
}

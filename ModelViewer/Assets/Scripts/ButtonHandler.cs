using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonHandler : MonoBehaviour {
    // Config
    [SerializeField] Button waypointButtonPrefab;
    [SerializeField] Button addButtonPrefab;
    [SerializeField] Button saveButton;
    

    // Params
    public int waypointCount = 0;
    bool saveActive = false;
    Button addButton;


    // Start is called before the first frame update
    void Start() {

    }

    public void OnSaveButtonPressed() {
        // If Save mode is active, cancel it
        if (saveActive) {
            saveActive = false;
            Destroy(GameObject.Find($"Add {waypointCount + 1}"));
            return;
        }

        // Activate the Save mode state
        saveActive = true;

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

    public void OnWaypointButtonPressed() {
        
    }

    public void OnAddButtonPressed() {
        // Deactivate Save Mode
        saveActive = false;
        // Increment NUmber of Waypoints
        waypointCount += 1;

        // Replace Add Button by Waypoint
        Button newWaypoint = Instantiate(waypointButtonPrefab);
        newWaypoint.GetComponentInChildren<TMP_Text>().text = (waypointCount).ToString();
        newWaypoint.transform.position = addButton.transform.position;
        newWaypoint.transform.SetParent(saveButton.transform);
        newWaypoint.name = $"Waypoint {waypointCount}";

        Destroy(GameObject.Find($"Add {waypointCount}"));

    }
}

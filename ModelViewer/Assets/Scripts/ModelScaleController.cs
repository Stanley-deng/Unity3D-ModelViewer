using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class ModelScaleController : MonoBehaviour
{
    // Config
    // The speed of scaler when using keys
    [SerializeField] float speed = 5f;
    // The speed of scaler when using multi-touch
    // Default scale
    [SerializeField] Vector3 defaultScale = new Vector3(250f, 250f, 250f);

    // Get the transform of target object
    public GameObject target;
    Transform Transform { get { return target.transform; } }

    void ResetScale()
    {
        Transform.localScale = defaultScale;
    }

    void ChangeScale(float delta)
    {
        Transform.localScale += new Vector3(delta, delta, delta);
    }


    void Update()
    {
        // Use keyboad to control the scale
    if (Input.GetKey(KeyCode.Space))
        {
            ResetScale();
        }
    }

    void OnGUI() {
        ChangeScale(speed * Input.mouseScrollDelta.y);
    }
}


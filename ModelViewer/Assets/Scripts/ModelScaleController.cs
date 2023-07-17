using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class ModelScaleController : MonoBehaviour
{
    // Config
    // The speed of scaler when using keys
    [SerializeField] float speed = 5f;
    // Default scale
    [SerializeField] Vector3 defaultScale = new Vector3(250f, 250f, 250f);
    // Get the transform of target object
    [SerializeField] GameObject target;
    Transform Transform { get { return Target.transform; } }

    public GameObject Target { get => target; set { target = value; defaultScale = target.transform.lossyScale; } }

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

    void ChangeScaleRelative(float delta) {
        float percentageChange = delta / 100;
        delta = 1 + percentageChange;
        Transform.localScale = Vector3.Scale(Transform.localScale, new Vector3(delta, delta, delta));
    }


    void OnGUI() {
        ChangeScaleRelative(speed * Input.mouseScrollDelta.y);
    }
}


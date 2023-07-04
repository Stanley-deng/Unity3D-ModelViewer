using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModelScaleController : MonoBehaviour
{
    // Config
    // The speed of scaler when using keys
    [SerializeField] float scaleSpeed = 1f;
    // The speed of scaler when using multi-touch
    [SerializeField] float pinchSpeed = 0.5f;
    // Default scale
    [SerializeField] Vector3 defaultScale = new Vector3(250f, 250f, 250f);

    // Get the transform of target object
    public GameObject target;
    Transform Transform { get { return target.transform; } }


    float mPrevY = 0f;
    float mPosDelta = 0f;


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
        if (Input.GetKey(KeyCode.UpArrow))
        {
            ChangeScale(scaleSpeed);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            ChangeScale(-scaleSpeed);
        }
        else if (Input.GetKey(KeyCode.R))
        {
            ResetScale();
        }

        // Right click and move to control the scale
        if (Input.GetMouseButton(1))
        {
            mPosDelta = Input.mousePosition.y - mPrevY;
            ChangeScale(pinchSpeed * mPosDelta);
        }

        // Update previous position of mouse
        mPrevY = Input.mousePosition.y;

    }
}


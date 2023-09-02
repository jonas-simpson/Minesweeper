using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class UISafeAreaHandler : UIBehaviour
{
    RectTransform panel;

    public delegate void OnDeviceOrientationChange();
    public static OnDeviceOrientationChange onDeviceOrientationChange;

    private bool pendingOrientationChange = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        panel = GetComponent<RectTransform>();

        //onDeviceOrientationChange = OnRectTransformDimensionsChange;
    }

    // Update is called once per frame
    void Update()
    {
        Rect area = Screen.safeArea;

        //Pixel size  in screen space of the whole screen
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);

        //for testing purposes
        if (Application.isEditor && Input.GetButton("Jump"))
        {
            //Use the notch properties of the iphone xs max
            if (Screen.height > Screen.width)
            {
                //Portrait
                area = new Rect(0f, 0.038f, 1f, 0.913f);
            }
            else
            {
                //Landscape
                area = new Rect(0.049f, 0.051f, 0.902f, 0.949f);
            }

            panel.anchorMin = area.position;
            panel.anchorMax = (area.position + area.size);

            return;
        }

        //Set anchors to percentages of the screen used
        panel.anchorMin = area.position / screenSize;
        panel.anchorMax = (area.position + area.size) / screenSize;
    }

    // protected override void OnRectTransformDimensionsChange()
    // {
    //     if (!pendingOrientationChange)
    //     {
    //         //Debug.Log("On Rect Transform Dimensions Change!");
    //         pendingOrientationChange = true;
    //         // onDeviceOrientationChange?.Invoke();
    //         Invoke("ResetPending", Time.deltaTime);
    //     }
    // }

    protected override void OnRectTransformDimensionsChange()
    {
        CancelInvoke("ResetPending");
        Invoke("ResetPending", Time.deltaTime);
    }

    private void ResetPending()
    {
        onDeviceOrientationChange?.Invoke();
    }
}

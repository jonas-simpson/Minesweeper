using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleOrientationHandler : MonoBehaviour
{
    //public UISafeAreaHandler safeAreaHandler;
    [Header("Orientation parents in canvas")]
    public Transform portraitContainer;
    public Transform landscapeContainer;

    [Header("Canvas elements to be reparented depending on orientation")]
    public Transform puzzleInfo;
    public Transform puzzleBoardContainer;
    public Transform puzzleButtons;

    // Start is called before the first frame update
    void Start()
    {
        UISafeAreaHandler.onDeviceOrientationChange += ReceiveOrientationChangeRequest;
    }

    // Update is called once per frame
    void Update() { }

    private void ReceiveOrientationChangeRequest()
    {
        Debug.Log("Orientation Change Request Received!");
        if (Screen.height > Screen.width)
        {
            //Portrait
            puzzleInfo.SetParent(portraitContainer);
            puzzleBoardContainer.SetParent(portraitContainer);
            puzzleButtons.SetParent(portraitContainer);
        }
        else
        {
            //Landscape
            puzzleInfo.SetParent(landscapeContainer);
            puzzleBoardContainer.SetParent(landscapeContainer);
            puzzleButtons.SetParent(landscapeContainer);
        }
    }
}

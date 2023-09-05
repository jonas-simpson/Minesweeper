using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class TileBehaviour
    : MonoBehaviour,
        IPointerDownHandler,
        IPointerUpHandler,
        IPointerExitHandler
{
    public RectTransform rectTransform;
    private Image myimage;
    public bool isTouching = false;
    public bool isRevealed = false;
    public bool isMarked = false;

    // Start is called before the first frame update
    void Start()
    {
        myimage = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update() { }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isRevealed)
        {
            myimage.color = Color.gray;
            isTouching = true;

            //Check if we are touching in 0.5 seconds
            CancelInvoke("CheckForHold");
            Invoke("CheckForHold", 0.5f);
        }

        Debug.Log("Mouse down on " + gameObject.name);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CancelInvoke("CheckForHold");

        if (!isMarked && !isRevealed)
        {
            if (isTouching)
            {
                //Valid touch released in less than <0.5 seconds
                myimage.color = Color.green;
                isRevealed = true;
            }
        }
        else if (isMarked && isTouching)
        {
            myimage.color = Color.red;
        }

        isTouching = false;
        Debug.Log("Mouse up on " + gameObject.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isTouching)
        {
            if (!isMarked)
            {
                myimage.color = Color.white;
            }
            else
            {
                myimage.color = Color.red;
            }
        }
        isTouching = false;
    }

    void CheckForHold()
    {
        if (isTouching)
        {
            //User is deliberately holding this tile. Mark as held before release
            if (!isMarked)
            {
                myimage.color = Color.red;
                isMarked = true;
            }
            else
            {
                myimage.color = Color.white;
                isMarked = false;
            }
            isTouching = false;
        }
    }
}

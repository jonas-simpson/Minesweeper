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
    private bool isTouching = false;

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
        myimage.color = Color.gray;
        isTouching = true;

        //Check if we are touching in 0.5 seconds
        Invoke("CheckForHold", 0.5f);
        Debug.Log("Mouse down");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isTouching)
        {
            //Valid touch released in less than <0.5 seconds
            myimage.color = Color.green;
            CancelInvoke("CheckForHold");
            isTouching = false;
        }
        Debug.Log("Mouse up");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //User has deliberately moved their finger off tile while pressing. Canceling touch
        if (isTouching)
        {
            isTouching = false;
            myimage.color = Color.white;
        }
    }

    void CheckForHold()
    {
        if (isTouching)
        {
            //User is deliberately holding this tile. Mark as held before release
            myimage.color = Color.red;
            isTouching = false;
        }
    }
}

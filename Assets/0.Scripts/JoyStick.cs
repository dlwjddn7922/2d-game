using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : Singleton<JoyStick>
{
    public GameObject smallStick;
    public GameObject bgStick;
    Vector3 stickFirstPosition;
    public Vector3 joyVec;
    float stickRadius;
    Vector3 joyStickFirstPosition;
    public bool isPlayerMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        stickRadius = bgStick.gameObject.GetComponent<RectTransform>().sizeDelta.y / 2;
        joyStickFirstPosition = bgStick.transform.position;
    }
    public void OnPointDown()
    {
        bgStick.transform.position = Input.mousePosition;
        smallStick.transform.position = Input.mousePosition;
        stickFirstPosition = Input.mousePosition;
        //setTirgger
    }
    public void OnDrag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector3 DragPosition = pointerEventData.position;
        joyVec = (DragPosition - stickFirstPosition).normalized;
        isPlayerMoving = true;
        float stickDistance = Vector3.Distance(DragPosition, stickFirstPosition);
        if (stickDistance < stickRadius)
        {
            smallStick.transform.position = stickFirstPosition + joyVec * stickDistance;
        }
        else
        {
            smallStick.transform.position = stickFirstPosition + joyVec * stickRadius;
        }
    }
    public void OnDrop()
    {
        joyVec = Vector3.zero;
        bgStick.transform.position = joyStickFirstPosition;
        smallStick.transform.position = joyStickFirstPosition;
        isPlayerMoving = false;
        //setTrigger
    }
    void Update()
    {
        
    }
}

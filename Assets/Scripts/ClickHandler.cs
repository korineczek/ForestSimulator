using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Abstract class that contains event click handling that can be inherited in controller classes if necessary.
/// </summary>
public abstract class ClickHandler : MonoBehaviour
{

    public void PointerClickHandler(BaseEventData data)
    {
        PointerEventData pointerEventData = data as PointerEventData;
        GameObject pressed = pointerEventData.pointerPress;

        //Left click
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            if (pointerEventData.clickCount == 1)
            {
                //OnLeftSingleClick(pressed);
            }
            else if (pointerEventData.clickCount == 2)
            {
                //OnLeftDoubleClick(pressed);
            }
        }
        //Right click
        else if (pointerEventData.button == PointerEventData.InputButton.Right)
        {
            if (pointerEventData.clickCount == 1)
            {
                OnRightSingleClick(pressed);
                
            }
            else if (pointerEventData.clickCount == 2)
            {
                ///OnRightDoubleClick(pressed);
            }
        }
        //Middle click
        else if (pointerEventData.button == PointerEventData.InputButton.Middle)
        {
            if (pointerEventData.clickCount == 1)
            {
                //OnMiddleSingleClick(pressed);
            }
            else if (pointerEventData.clickCount == 2)
            {
                //OnMiddleDoubleClick(pressed);
            }
        }
    }

    public abstract void OnRightSingleClick(GameObject pressed);
}

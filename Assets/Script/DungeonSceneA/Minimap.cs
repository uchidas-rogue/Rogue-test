using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    private RectTransform minimapRect;
    private bool isPickup;
    /// <summary>
    /// offsetMin > 0 offsetMax > 1 , 0(left,bottom)1(right,-top)
    /// </summary>
    private Vector2[] miniSize = { new Vector2 (1435, 450), new Vector2 (0, -370) };
    private Vector2[] pickupSize = { new Vector2 (0, 0), new Vector2 (0, 0) };

    void Start ()
    {
        minimapRect = GetComponent<RectTransform> ();
    }

    private void ChangeMapSize ()
    {
        if (isPickup)
        {
            minimapRect.offsetMin = pickupSize[0];
            minimapRect.offsetMax = pickupSize[1];
        }
        else
        {
            minimapRect.offsetMin = miniSize[0];
            minimapRect.offsetMax = miniSize[1];
        }
    }

    public void OnClicked ()
    {
        isPickup = !isPickup;
        ChangeMapSize ();
    }
}
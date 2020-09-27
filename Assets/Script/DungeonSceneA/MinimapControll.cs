using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MinimapControll : MonoBehaviour
{
    private RectTransform minimapRect;
    private bool isPickup;
    /// <summary>
    /// offsetMin > 0 offsetMax > 1 , 0(left,bottom)1(right,-top)
    /// </summary>
    private Vector2[] miniSize = { new Vector2 (1435, 450), new Vector2 (0, -370) };
    private Vector2[] pickupSize = { new Vector2 (0, 0), new Vector2 (0, 0) };

    private TextMeshProUGUI mapText ;

    void Awake ()
    {
        minimapRect = GetComponent<RectTransform> ();
        mapText = transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void SetmapText(string mapString)
    {
        mapText.text = mapString;
    }

    private void ChangeMapSize ()
    {
        if (isPickup)
        {
            minimapRect.offsetMin = pickupSize[0];
            minimapRect.offsetMax = pickupSize[1];
            mapText.fontSize = (float)70;
        }
        else
        {
            minimapRect.offsetMin = miniSize[0];
            minimapRect.offsetMax = miniSize[1];
            mapText.fontSize = (float)20;
        }
    }

    public void OnClicked ()
    {
        isPickup = !isPickup;
        ChangeMapSize ();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICriticalLoadDisplay : MonoBehaviour
{
    public TextMeshProUGUI percentageDisplay;
    public NationalData natData;
    public Image barFillImage;
    private float fillImageWidth;

    private void Awake()
    {
        fillImageWidth = barFillImage.rectTransform.sizeDelta.x;
        TurnController.DisplayActionSelectionScreen += DisplayPercentage;
    }

    private void OnDestroy()
    {
        TurnController.DisplayActionSelectionScreen -= DisplayPercentage;
    }

    public void DisplayPercentage(Sector sector)
    {
        percentageDisplay.text = Mathf.RoundToInt(natData._NationalItem._N2000_Below_Critical).ToString() + "%";

        barFillImage.rectTransform.sizeDelta = new Vector2(natData._NationalItem._N2000_Below_Critical / 100 * fillImageWidth, barFillImage.rectTransform.sizeDelta.y);
    }
}
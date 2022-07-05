using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIHappinessDisplay : MonoBehaviour
{
    public TextMeshProUGUI percentageDisplay;
    public NationalData natData;
    public Image barFillImage;
    private float fillImageWidth;

    private void Awake()
    {
        fillImageWidth = barFillImage.rectTransform.sizeDelta.x;
        TurnController.DisplayActionSelectionScreen += DisplayHappiness;
    }

    private void OnDestroy()
    {
        TurnController.DisplayActionSelectionScreen -= DisplayHappiness;
    }

    private void DisplayHappiness(Sector sector)
    {
        percentageDisplay.text = Mathf.RoundToInt(natData._NationalItem._Happiness).ToString();
        barFillImage.rectTransform.sizeDelta = new Vector2(natData._NationalItem._Happiness / 100 * fillImageWidth, barFillImage.rectTransform.sizeDelta.y);
    }
}

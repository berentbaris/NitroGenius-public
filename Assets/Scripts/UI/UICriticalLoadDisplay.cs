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
        Role_selection_script.StartGame += OnGameStart;
    }

    private void OnDestroy()
    {
        Role_selection_script.StartGame -= OnGameStart;
    }

    private void OnGameStart(Sector sector)
    {
        DisplayPercentage();
    }

    public void DisplayPercentage()
    {
        percentageDisplay.text = Mathf.RoundToInt(natData._NationalItem._N2000_Below_Critical).ToString() + "%";

        barFillImage.rectTransform.sizeDelta = new Vector2(natData._NationalItem._N2000_Below_Critical / 100 * fillImageWidth, barFillImage.rectTransform.sizeDelta.y);
    }
}
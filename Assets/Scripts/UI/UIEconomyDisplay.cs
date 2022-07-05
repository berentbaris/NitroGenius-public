using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIEconomyDisplay : MonoBehaviour
{
    public TextMeshProUGUI economyText;
    public NationalData natData;
    public SectorList sectorList;
    private Sector selectedSector;

    private void Awake()
    {
        TurnController.DisplayActionSelectionScreen += Display_Economy;
    }

    private void OnDestroy()
    {
        TurnController.DisplayActionSelectionScreen -= Display_Economy;
    }

    private void Display_Economy(Sector sector)
    {
        economyText.gameObject.SetActive(true);
        float percentage = natData._NationalItem._Economic_Factor * 3.33333333333f;
        economyText.text = "The Economy: " + Mathf.RoundToInt(percentage);
    }
}

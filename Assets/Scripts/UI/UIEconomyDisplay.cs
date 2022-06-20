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
        Role_selection_script.StartGame += OnStartGame;
    }

    private void OnDestroy()
    {
        Role_selection_script.StartGame -= OnStartGame;
    }

    private void OnStartGame(Sector sector)
    {
        Display_Economy();
    }

    public void Display_Economy()
    {
        economyText.gameObject.SetActive(true);
        float percentage = natData._NationalItem._Economic_Factor * 3.33333333333f;
        economyText.text = "The Economy: " + Mathf.RoundToInt(percentage);
    }
}

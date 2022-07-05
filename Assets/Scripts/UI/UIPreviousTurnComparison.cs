using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPreviousTurnComparison : MonoBehaviour
{
    public Sector selectedSector;
    public IntVariable turn;
    public TextMeshProUGUI N2OComparison;
    public TextMeshProUGUI NH3Comparison;
    public TextMeshProUGUI NoxComparison;

    private void Awake()
    {
        TurnController.DisplayActionSelectionScreen += CalculatePreviousTurnComparison;
    }

    private void OnDestroy()
    {
        TurnController.DisplayActionSelectionScreen -= CalculatePreviousTurnComparison;
    }

    public void CalculatePreviousTurnComparison(Sector sector)
    {
        selectedSector = sector;
        CalculateN2O();
        CalculateNH3();
        CalculateNox();
    }

    private void CalculateN2O()
    {
        if (selectedSector.N2O_Emissions == 0)
        {
            N2OComparison.text = "Your N2O emissions: 0%";
            return;
        }

        float previousTurnN2O = selectedSector.N20Record[0];

        if (selectedSector.N2O_Emissions >= previousTurnN2O)
        {
            float percentage = ((selectedSector.N2O_Emissions - previousTurnN2O) / previousTurnN2O) * 100;
            N2OComparison.text = "Your N2O emissions: +" + Mathf.RoundToInt(percentage) + "%";
        }

        else if (selectedSector.N2O_Emissions < previousTurnN2O)
        {
            float percentage = ((previousTurnN2O - selectedSector.N2O_Emissions) / previousTurnN2O) * 100;
            N2OComparison.text = "Your N2O emissions: -" + Mathf.RoundToInt(percentage) + "%";
        }
    }

    private void CalculateNH3()
    {
        if(selectedSector.NH3_Emissions == 0)
        {
            NH3Comparison.text = "Your NH3 emissions: 0";
            return;
        }

        float previousTurnNH3 = selectedSector.NH3Record[0];

        if (selectedSector.NH3_Emissions >= previousTurnNH3)
        {
            float percentage = ((selectedSector.NH3_Emissions - previousTurnNH3) / previousTurnNH3) * 100;
            NH3Comparison.text = "Your NH3 emissions: +" + Mathf.RoundToInt(percentage) + "%";
        }

        else if (selectedSector.NH3_Emissions < previousTurnNH3)
        {
            float percentage = ((previousTurnNH3 - selectedSector.NH3_Emissions) / previousTurnNH3) * 100;
            NH3Comparison.text = "Your NH3 emissions: -" + Mathf.RoundToInt(percentage) + "%";
        }
    }

    private void CalculateNox()
    {
        if (selectedSector.Nox_Emissions == 0)
        {
            NoxComparison.text = "Your NOx emissions: 0";
            return;
        }

        float previousTurnNOX = selectedSector.NoxRecord[0];

        if (selectedSector.Nox_Emissions >= previousTurnNOX)
        {
            float percentage = ((selectedSector.Nox_Emissions - previousTurnNOX) / previousTurnNOX) * 100;
            NoxComparison.text = "Your NOx emissions: +" + Mathf.RoundToInt(percentage) + "%";
        }

        else if (selectedSector.Nox_Emissions < previousTurnNOX)
        {
            float percentage = ((previousTurnNOX - selectedSector.Nox_Emissions) / previousTurnNOX) * 100;
            NoxComparison.text = "Your NOx emissions: -" + Mathf.RoundToInt(percentage) + "%";
        }
    }
}
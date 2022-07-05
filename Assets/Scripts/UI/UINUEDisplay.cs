using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UINUEDisplay : MonoBehaviour
{
    private Sector selectedSector;
    public SectorList sectorList;
    public TextMeshProUGUI NUEText;

    private void Awake()
    {
        TurnController.DisplayActionSelectionScreen += Display_NUE;
    }

    private void OnDestroy()
    {
        TurnController.DisplayActionSelectionScreen -= Display_NUE;
    }

    public void Display_NUE(Sector sector)
    {
        selectedSector = sector;

        if (selectedSector == sectorList.list[1])
        {
            NUEText.gameObject.SetActive(true);

            if (selectedSector.NUE == selectedSector.NUERecord[0])
            {
                NUEText.text = "N Use Efficiency: " + 0 + "%";
            }
            else if (selectedSector.NUE > selectedSector.NUERecord[0])
            {
                float percentage = ((selectedSector.NUE - selectedSector.NUERecord[0]) / selectedSector.NUERecord[0]) * 100;
                NUEText.text = "N Use Efficiency: +" + Mathf.RoundToInt(percentage) + "%";
            }
            else if (selectedSector.NUE < selectedSector.NUERecord[0])
            {
                float percentage = ((selectedSector.NUERecord[0] - selectedSector.NUE) / selectedSector.NUERecord[0]) * 100;
                NUEText.text = "N Use Efficiency: -" + Mathf.RoundToInt(percentage) + "%";
            }
        }
        else
        {
            NUEText.gameObject.SetActive(false);
        }
    }
}

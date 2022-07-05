using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIProfitsDisplay : MonoBehaviour
{
    private Sector selectedSector;
    public SectorList sectorList;
    public TextMeshProUGUI profitsText;

    private void Awake()
    {
        TurnController.DisplayActionSelectionScreen += Display_Profits;
    }

    private void OnDestroy()
    {
        TurnController.DisplayActionSelectionScreen -= Display_Profits;
    }

    public void Display_Profits(Sector sector)
    {
        selectedSector = sector;

        if (selectedSector != sectorList.list[3] && selectedSector != sectorList.list[2])
        {
            profitsText.gameObject.SetActive(true);

            if (selectedSector.Product_Volume_Total == selectedSector.ProductVolumeTotalRecord[0])
            {
                profitsText.text = "Your Production: " + 0 + "%";
            }
            else if (selectedSector.Product_Volume_Total > selectedSector.ProductVolumeTotalRecord[0])
            {
                float percentage = ((selectedSector.Product_Volume_Total - selectedSector.ProductVolumeTotalRecord[0]) / selectedSector.ProductVolumeTotalRecord[0]) * 100;
                profitsText.text = "Your Production: +" + Mathf.RoundToInt(percentage) + "%";
            }
            else if (selectedSector.Product_Volume_Total < selectedSector.ProductVolumeTotalRecord[0])
            {
                float percentage = ((selectedSector.ProductVolumeTotalRecord[0] - selectedSector.Product_Volume_Total) / selectedSector.ProductVolumeTotalRecord[0]) * 100;
                profitsText.text = "Your Production: -" + Mathf.RoundToInt(percentage) + "%";
            }
        }
        else
        {
            profitsText.gameObject.SetActive(false);
        }
    }
}

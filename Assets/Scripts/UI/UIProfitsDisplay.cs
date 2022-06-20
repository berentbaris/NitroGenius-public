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
        Role_selection_script.StartGame += OnStartGame;
    }

    private void OnDestroy()
    {
        Role_selection_script.StartGame -= OnStartGame;
    }

    private void OnStartGame(Sector sector)
    {
        selectedSector = sector;
        Display_Profits();
    }

    public void Display_Profits()
    {
        if (selectedSector != sectorList.list[3] && selectedSector != sectorList.list[2])
        {
            profitsText.gameObject.SetActive(true);

            if (selectedSector.Product_Volume_Total == selectedSector.ProductVolumeTotalRecord[0])
            {
                profitsText.text = "Your Profits: " + 0 + "%";
            }
            else if (selectedSector.Product_Volume_Total > selectedSector.ProductVolumeTotalRecord[0])
            {
                float percentage = ((selectedSector.Product_Volume_Total - selectedSector.ProductVolumeTotalRecord[0]) / selectedSector.ProductVolumeTotalRecord[0]) * 100;
                profitsText.text = "Your Profits: +" + Mathf.RoundToInt(percentage) + "%";
            }
            else if (selectedSector.Product_Volume_Total < selectedSector.ProductVolumeTotalRecord[0])
            {
                float percentage = ((selectedSector.ProductVolumeTotalRecord[0] - selectedSector.Product_Volume_Total) / selectedSector.ProductVolumeTotalRecord[0]) * 100;
                profitsText.text = "Your Profits: -" + Mathf.RoundToInt(percentage) + "%";
            }
        }
        else
        {
            profitsText.gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIGameEndScreen : MonoBehaviour
{
    public TextMeshProUGUI mainText;
    public NationalData natData;
    public SectorList sectorList;
    private Sector selectedSector;
    public TextMeshProUGUI budgetText;
    public TextMeshProUGUI imageText;
    public TextMeshProUGUI GDPText;
    public TextMeshProUGUI happinessText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI NUEText;
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        Role_selection_script.StartGame += GetSelectedSector;
    }

    private void OnDestroy()
    {
        Role_selection_script.StartGame -= GetSelectedSector;
    }

    public void GetSelectedSector(Sector sector)
    {
        selectedSector = sector;
    }

    public void OnGameEnd()
    {

        if (natData._NationalItem._N2000_Below_Critical >= 74)
        {
            mainText.text = "Congratulations, you win! You brought " + Mathf.RoundToInt(natData._NationalItem._N2000_Below_Critical) + "% of nitrogen-sensitive Natura 2000 areas back below the critical deposition loads. The national target for 2035 was 74%.";
        }
        else
        {
            mainText.text = "Unfortunately, you have failed to meet the 2035 national nitrogen target. You brought " + Mathf.Floor(natData._NationalItem._N2000_Below_Critical) + "% of nitrogen-sensitive Natura 2000 areas back below the critical deposition loads. The national target for 2035 was 74%.";
        }

        //budget text
        if (selectedSector != sectorList.list[3] && selectedSector != sectorList.list[2])
        {
            budgetText.gameObject.SetActive(true);

            if (selectedSector.ProductVolumeTotalRecord[13] == selectedSector.ProductVolumeTotalRecord[0])
            {
                budgetText.text = "-   Your profits remained the same";
            }
            else if (selectedSector.ProductVolumeTotalRecord[13] > selectedSector.ProductVolumeTotalRecord[0])
            {
                float percentage = ((selectedSector.ProductVolumeTotalRecord[13] - selectedSector.ProductVolumeTotalRecord[0]) / selectedSector.ProductVolumeTotalRecord[0]) * 100;
                budgetText.text = "-   You increased your role's profits by " + Mathf.RoundToInt(percentage) + "%";
            }
            else if (selectedSector.ProductVolumeTotalRecord[13] < selectedSector.ProductVolumeTotalRecord[0])
            {
                float percentage = ((selectedSector.ProductVolumeTotalRecord[0] - selectedSector.ProductVolumeTotalRecord[13]) / selectedSector.ProductVolumeTotalRecord[0]) * 100;
                budgetText.text = "-   You decreased your role's profits by " + Mathf.RoundToInt(percentage) + "%";
            }
        }
        else
        {
            budgetText.gameObject.SetActive(false);
        }

        //NUE text
        if (selectedSector == sectorList.list[1])
        {
            NUEText.gameObject.SetActive(true);

            if (selectedSector.NUERecord[13] == 0.01428571f)
            {
                NUEText.text = "-   Your N Use Efficiency remained the same";
            }
            else if (selectedSector.NUERecord[13] > 0.01428571f)
            {
                float percentage = ((selectedSector.NUERecord[13] - 0.01428571f) / 0.01428571f) * 100;
                NUEText.text = "-   You increased your N Use Efficiency by " + Mathf.RoundToInt(percentage) + "%";
            }
            else if (selectedSector.NUERecord[13] < 0.01428571f)
            {
                float percentage = ((0.01428571f - selectedSector.NUERecord[13]) / 0.01428571f) * 100;
                NUEText.text = "-   You decreased your N Use Efficiency by " + Mathf.RoundToInt(percentage) + "%";
            }
        }
        else
        {
            NUEText.gameObject.SetActive(false);
        }

        //health text
        if (selectedSector == sectorList.list[2])
        {
            healthText.gameObject.SetActive(true);

            if (natData._NationalItem._Health_Factor >= 20)
            {
                float percentage = ((natData._NationalItem._Health_Factor - 20) / 40) * 100;
                healthText.text = "-   You increased Public Health by " + Mathf.RoundToInt(percentage) + "%";
            }
            else
            {
                float percentage = ((20 - natData._NationalItem._Health_Factor) / 40) * 100;
                budgetText.text = "-   You decreased Public Health by " + Mathf.RoundToInt(percentage) + "%";
            }
        }
        else
        {
            healthText.gameObject.SetActive(false);
        }

        //image text;
        if (selectedSector != sectorList.list[2])
        {
            imageText.gameObject.SetActive(true);
            if (selectedSector.Image > 10.49f)
            {
                imageText.text = "-   You increased your role's image";
            }
            if (selectedSector.Image < 9.5f)
            {
                imageText.text = "-   You decreased your role's image";
            }
            if (selectedSector.Image < 10.5f && selectedSector.Image > 9.49f)
            {
                imageText.text = "-   You maintained your role's image";
            }
        }
        else
        {
            imageText.gameObject.SetActive(false);
        }

        //GDP per capita text
        if (natData._NationalItem._GDP_Per_Capita == natData._NationalItem.GDP_Per_Capita_Record[0])
        {
            GDPText.text = "-   GDP Per Capita remained the same";
        }
        else if (natData._NationalItem._GDP_Per_Capita > natData._NationalItem.GDP_Per_Capita_Record[0])
        {
            float percentage = ((natData._NationalItem._GDP_Per_Capita - natData._NationalItem.GDP_Per_Capita_Record[0]) / natData._NationalItem.GDP_Per_Capita_Record[0]) * 100;
            GDPText.text = "-   You increased GDP Per Capita by " + Mathf.RoundToInt(percentage) + "%";
        }
        else if(natData._NationalItem._GDP_Per_Capita < natData._NationalItem.GDP_Per_Capita_Record[0])
        {
            float percentage = ((natData._NationalItem.GDP_Per_Capita_Record[0] -natData._NationalItem._GDP_Per_Capita) / natData._NationalItem.GDP_Per_Capita_Record[0]) * 100;
            GDPText.text = "-   You decreased GDP Per Capita by " + Mathf.RoundToInt(percentage) + "%";
        }

        //happiness text
        if (natData._NationalItem._Happiness == natData._NationalItem.Happiness_Record[0])
        {
            happinessText.text = "-   Happiness remained the same";
        }
        else if (natData._NationalItem._Happiness > natData._NationalItem.Happiness_Record[0])
        {
            float percentage = ((natData._NationalItem._Happiness - natData._NationalItem.Happiness_Record[0]) / natData._NationalItem.Happiness_Record[0]) * 100;
            happinessText.text = "-   You increased citizen happiness by " + Mathf.RoundToInt(percentage) + "%";
        }
        else if (natData._NationalItem._Happiness < natData._NationalItem.Happiness_Record[0])
        {
            float percentage = ((natData._NationalItem.Happiness_Record[0] - natData._NationalItem._Happiness) / natData._NationalItem.Happiness_Record[0]) * 100;
            happinessText.text = "-   You decreased citizen happiness by " + Mathf.RoundToInt(percentage) + "%";
        }
        //score text
        if (selectedSector != sectorList.list[3] && selectedSector != sectorList.list[2])
        {
	        natData._NationalItem._Score = (((natData._NationalItem._N2000_Below_Critical - 25) / (75 - 25)) * (40 - 0) + 0) + (((natData._NationalItem._Happiness - 0) / (100 - 0)) * (20 - 0) + 0) + (((natData._NationalItem._Economic_Factor - 0) / (30 - 0)) * (20 - 0) + 0) + ((((((selectedSector.ProductVolumeTotalRecord[13] - selectedSector.ProductVolumeTotalRecord[1]) / selectedSector.ProductVolumeTotalRecord[1]) * 100) + 100) / (100 + 100)) * (10 - 0) + 0) + (((selectedSector.Image - 8) / (12 - 8)) * (10 - 0) + 0);
            scoreText.text = "Your score is " + Mathf.RoundToInt(natData._NationalItem._Score);
        }
        if (selectedSector == sectorList.list[3])
        {
	        natData._NationalItem._Score = (((natData._NationalItem._N2000_Below_Critical - 25) / (75 - 25)) * (40 - 0) + 0) + (((natData._NationalItem._Happiness - 0) / (100 - 0)) * (20 - 0) + 0) + (((natData._NationalItem._Economic_Factor - 0) / (30 - 0)) * (20 - 0) + 0) + (((selectedSector.Image - 8) / (12 - 8)) * (20 - 0) + 0);
            scoreText.text = "Your score is " + Mathf.RoundToInt(natData._NationalItem._Score);
        }
        if (selectedSector == sectorList.list[2])
        {		    
            natData._NationalItem._Score = (((natData._NationalItem._N2000_Below_Critical - 25) / (75 - 25)) * (40 - 0) + 0) + (((natData._NationalItem._Happiness - 0) / (100 - 0)) * (30 - 0) + 0) + (((natData._NationalItem._Economic_Factor - 0) / (30 - 0)) * (30 - 0) + 0);
            scoreText.text = "Your score is " + Mathf.RoundToInt(natData._NationalItem._Score);        
        }
    }

    public void OnPlayAgainButtonPress()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
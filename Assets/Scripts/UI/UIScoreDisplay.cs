using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.EventSystems;

public class UIScoreDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Sector selectedSector;
    public SectorList sectorList;
    public NationalData natData;
    public TextMeshProUGUI scoreText;
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
        Display_Score();
    }
    public void Display_Score()
    {
        if (selectedSector != sectorList.list[3] && selectedSector != sectorList.list[2])
        {
            scoreText.text = Mathf.RoundToInt(natData._NationalItem._Score).ToString();
        }
        if (selectedSector == sectorList.list[3])
        {
            scoreText.text = Mathf.RoundToInt(natData._NationalItem._Score).ToString();
        }
        if (selectedSector == sectorList.list[2])
        {		    
            scoreText.text = Mathf.RoundToInt(natData._NationalItem._Score).ToString();        
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.Show("Your score", "Score");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }
}

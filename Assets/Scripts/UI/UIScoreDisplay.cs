using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.EventSystems;

public class UIScoreDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        TurnController.DisplayActionSelectionScreen += Display_Score;
    }

    private void OnDestroy()
    {
        TurnController.DisplayActionSelectionScreen -= Display_Score;
    }

    public void Display_Score(Sector sector)
    {
        scoreText.text = Mathf.RoundToInt(sector.score).ToString();
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

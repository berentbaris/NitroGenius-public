using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIRemainingActionsDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public List<Image> circleImages = new List<Image>();
    public Sprite circleSprite;
    public Sprite checkSprite;

    private void Awake()
    {
        TurnController.DisplayActionSelectionScreen += RefreshDisplay;
        UIChoice.ChoiceSelected += RefreshDisplay;
        UIChoice.ChoiceUnselected += RefreshDisplay;
    }

    private void OnDestroy()
    {
        TurnController.DisplayActionSelectionScreen -= RefreshDisplay;
        UIChoice.ChoiceSelected -= RefreshDisplay;
        UIChoice.ChoiceUnselected -= RefreshDisplay;
    }

    private void RefreshDisplay(Sector sector)
    {
        for (int i = 0; i < circleImages.Count; i++)
        {
            if (i < sector.Action_Limit_Per_Turn)
            {
                circleImages[i].sprite = circleSprite;
            }
            else
            {
                circleImages[i].sprite = checkSprite;
            }
        }
    }

    private void RefreshDisplay(Action action)
    {
        for (int i = 0; i < circleImages.Count; i++)
        {
            if (i < action._Belonging_Sector.Action_Limit_Per_Turn)
            {
                circleImages[i].sprite = circleSprite;
            }
            else
            {
                circleImages[i].sprite = checkSprite;
            }
        }
    }

    public void ResetSprites()
    {
        foreach (Image image in circleImages)
        {
            image.sprite = circleSprite;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.Show("Your remaining actions for this turn", "Action Points");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }
}
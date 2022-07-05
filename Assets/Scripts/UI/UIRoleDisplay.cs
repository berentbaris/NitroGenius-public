using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIRoleDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    public Image background;
    public Image icon;
    private Sector currentSector;
    private void Awake()
    {
        TurnController.DisplayActionSelectionScreen += ChangeRoleDisplay;
    }

    private void OnDestroy()
    {
        TurnController.DisplayActionSelectionScreen -= ChangeRoleDisplay;
    }

    private void ChangeRoleDisplay(Sector sector)
    {
        background.color = sector.color.Value;
        icon.sprite = sector.Icon;
        currentSector = sector;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.Show(currentSector.Concerns, currentSector.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }
}
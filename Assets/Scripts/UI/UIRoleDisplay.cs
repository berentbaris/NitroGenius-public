using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIRoleDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    public Image background;
    public Image icon;
    private Sector selectedSector;

    private void Awake()
    {
        Role_selection_script.StartGame += ChangeRoleDisplay;
    }

    private void OnDestroy()
    {
        Role_selection_script.StartGame -= ChangeRoleDisplay;
    }

    public void ChangeRoleDisplay(Sector sector)
    {
        background.color = sector.color.Value;
        icon.sprite = sector.Icon;
        selectedSector = sector;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.Show(selectedSector.Concerns, selectedSector.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }
}
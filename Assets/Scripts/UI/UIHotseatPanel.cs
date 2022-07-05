using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHotseatPanel : MonoBehaviour
{
    private CanvasGroup thisCangroup;
    public List<TMP_Dropdown> dropdownList = new List<TMP_Dropdown>();
    public IntVariable gamemode;
    public Button startButton;

    private void Awake()
    {
        thisCangroup = GetComponent<CanvasGroup>();
    }

    public void OpenHotSeatPanel()
    {
        thisCangroup.alpha = 1;
        thisCangroup.interactable = true;
        thisCangroup.blocksRaycasts = true;
        gamemode.SetValue(1);

        foreach (TMP_Dropdown dropdown in dropdownList)
        {
            dropdown.value = 2;
        }
        OnDropDownValueChange();
    }

    public void OnDropDownValueChange()
    {
        startButton.interactable = false;

        int numberOfPlayers = 0;
        foreach (TMP_Dropdown dropdown in dropdownList)
        {
            if (dropdown.value == 0)
            {
                numberOfPlayers++;
            }
        }

        if (numberOfPlayers >= 2)
        {
            startButton.interactable = true;
        }
    }

    public void CloseHotSeatPanel()
    {
        thisCangroup.alpha = 0;
        thisCangroup.interactable = false;
        thisCangroup.blocksRaycasts = false;
    }
}

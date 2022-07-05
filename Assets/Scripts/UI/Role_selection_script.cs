using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ScriptableEvents.Events;

public class Role_selection_script : MonoBehaviour
{
    public SimpleScriptableEvent StartGameEvent;
    public Sector selected_sector;
    public Button Start_Game;
    public TextMeshProUGUI Name_text;
    public TextMeshProUGUI Description_text;
    public TextMeshProUGUI Concerns_text;
    public TextMeshProUGUI Concerns_Title_text;
    public Event gamestart;
    public IntVariable currentGameMode;
    private CanvasGroup RoleSelectioncanGroup;

    private void Awake()
    {
        RoleSelectioncanGroup = GetComponent<CanvasGroup>();
        UIRoleSelectorButton.SelectionChange += Selection_change;
    }

    private void OnDestroy()
    {
        UIRoleSelectorButton.SelectionChange -= Selection_change;
    }

    public void ActivateRoleSelectionScreen()
    {
        if (currentGameMode.Value == 0)
        {
            RoleSelectioncanGroup.alpha = 1;
            RoleSelectioncanGroup.interactable = true;
            RoleSelectioncanGroup.blocksRaycasts = true;
        }
        else
        {
            StartGameEvent.Raise();
            gameObject.SetActive(false);
        }
    }

    public void Selection_change(Sector selection)
    {
        selected_sector = selection;
        Start_Game.interactable = true;
        Name_text.text = selection.name;
        Description_text.text = selection.Description;
        Concerns_text.text = selection.Concerns;
        Concerns_Title_text.text = "Concerns:";
    }

    void Start()
    {
        Start_Game.interactable = false;
        Name_text.text = "";
        Description_text.text = "";
        Concerns_text.text = "";
        Concerns_Title_text.text = "";
    }

    public void OnStartGameButtonPress()
    {
        StartGameEvent.Raise();
    }
}
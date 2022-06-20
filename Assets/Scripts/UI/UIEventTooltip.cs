using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIEventTooltip : MonoBehaviour
{
    public TextMeshProUGUI ChoiceNameText;
    public TextMeshProUGUI ChoiceDescriptionText;
    public TextMeshProUGUI ChoiceEffectsText;
    public List<GameObject> ConsequenceDisplays = new List<GameObject>();


    private void Awake()
    {
        UIEvent.EventHovered += DisplayEventTooltip;
        UIEvent.EventUnHovered += OnEventUnHovered;
        ClearTooltip();
    }

    private void OnDestroy()
    {
        UIEvent.EventHovered -= DisplayEventTooltip;
        UIEvent.EventUnHovered -= OnEventUnHovered;
    }
    private void DisplayEventTooltip(Action action)
    {
        this.gameObject.SetActive(true);
        ChoiceNameText.text = action._Name;
        ChoiceDescriptionText.text = action._Description;
        //ChoiceEffectsText.text = action.sdfdfsdfsds
        foreach (GameObject gameobject in ConsequenceDisplays)
        {
            gameobject.SetActive(false);
        }

        int numbOfConsequences = 0;
        
        if (action._NH3 != "")
        {
            ConsequenceDisplays[numbOfConsequences].SetActive(true);
            ConsequenceDisplays[numbOfConsequences].GetComponent<UIConsequenceTooltip>().DisplayConsequence(1, action._NH3);
            numbOfConsequences++;
        }
        if (action._Nox != "")
        {
            ConsequenceDisplays[numbOfConsequences].SetActive(true);
            ConsequenceDisplays[numbOfConsequences].GetComponent<UIConsequenceTooltip>().DisplayConsequence(2, action._Nox);
            numbOfConsequences++;
        }
        if (action._N2O != "")
        {
            ConsequenceDisplays[numbOfConsequences].SetActive(true);
            ConsequenceDisplays[numbOfConsequences].GetComponent<UIConsequenceTooltip>().DisplayConsequence(3, action._N2O);
            numbOfConsequences++;
        }
    }


    private void OnEventUnHovered(Action action)
    {
        ClearTooltip();
    }

    public void ClearTooltip()
    {
        this.gameObject.SetActive(false);
        ChoiceNameText.text = "";
        ChoiceDescriptionText.text = "";
        ChoiceEffectsText.text = "";
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

public class UIChoice : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static event Action<Action> ChoiceSelected;
    public static event Action<Action> ChoiceUnselected;
    public static event Action<Action> ChoiceHovered;
    public static event Action<Action> ChoiceUnHovered;

    public Action belongingChoice;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI costText;
    public Toggle toggle;
    public Image BgImage;
    public Image circleImage;
    private Color originalColor;
    [SerializeField] private Color greenColor;
    [SerializeField] private Color highlightColor;
    private float fadeDuration = 0.3f;
    public Sound ChoiceSelectedSound;
    public Sound ChoiceUnselectedSound;

    private void Awake()
    {
        ChoiceSelected += OnChoiceSelected;
        ChoiceUnselected += OnChoiceSelected;
        originalColor = BgImage.color;
    }

    private void OnDestroy()
    {
        ChoiceSelected -= OnChoiceSelected;
        ChoiceUnselected -= OnChoiceSelected;
    }

    private void OnChoiceSelected(Action action)
    {
        RefreshDisplay();
    }

    public void SetChoice(Action action)
    {
        belongingChoice = action;
        nameText.text = belongingChoice._Name;

        if (belongingChoice._Cost == 0)
        {
            costText.text = "";
        }
        else if (belongingChoice._Cost < 0)
        {
            float absoluteValue = Mathf.Abs(belongingChoice._Cost);
            costText.text = "€ +" + absoluteValue;
        }
        else
        {
            costText.text = "€ " + belongingChoice._Cost.ToString();
        }
        RefreshDisplay();
    }

    private void RefreshDisplay()
    {   
        if (toggle.isOn)
        {
            return;
        }

        else if (belongingChoice._Cost > belongingChoice._Belonging_Sector.Budget || belongingChoice._Belonging_Sector.Action_Limit_Per_Turn == 0)
        {
            toggle.interactable = false;
            nameText.DOColor(Color.gray, fadeDuration);
            costText.DOColor(Color.gray, fadeDuration);
            circleImage.DOColor(Color.gray, fadeDuration);
        }
        else
        {
            toggle.interactable = true;
            nameText.DOColor(Color.white, fadeDuration);
            costText.DOColor(Color.white, fadeDuration);
            circleImage.DOColor(Color.white, fadeDuration);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ChoiceHovered(belongingChoice);
        if (toggle.interactable == true && toggle.isOn == false)
        {
            BgImage.DOColor(highlightColor, 0.1f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ChoiceUnHovered(belongingChoice);
        if (toggle.isOn == false)
        {
            BgImage.DOColor(originalColor, 0.1f);
        }
    }

    public void OnToggleClick()
    {
        if (toggle.isOn)
        {
            BgImage.DOColor(greenColor, fadeDuration);
            belongingChoice._Belonging_Sector.ChoiceSelected(belongingChoice);
            ChoiceSelected(belongingChoice);
            ChoiceSelectedSound.PlaySound();
            return;
        }

        else if (toggle.isOn == false)
        {
            BgImage.DOColor(originalColor, fadeDuration);
            belongingChoice._Belonging_Sector.ChoiceUnselected(belongingChoice);
            ChoiceUnselected(belongingChoice);
            ChoiceUnselectedSound.PlaySound();
            return;
        }
    }
}
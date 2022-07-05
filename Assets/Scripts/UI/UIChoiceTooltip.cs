using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIChoiceTooltip : MonoBehaviour
{
    public Image ChoiceImage;
    public TextMeshProUGUI ChoiceNameText;
    public TextMeshProUGUI ChoiceDescriptionText;
    public List<GameObject> ConsequenceDisplays = new List<GameObject>();
    private RectTransform thisRect;
    private Sequence hideSequence;
    private bool EaseInHappening = false;
    private float originalX;
    public float ExtendedX;

    private void Awake()
    {
        UIChoice.ChoiceHovered += DisplayTooltip;
        UIChoice.ChoiceUnHovered += OnChoiceUnHovered;
        thisRect = GetComponent<RectTransform>();
        originalX = thisRect.anchoredPosition.x;
    }

    private void OnDestroy()
    {
        UIChoice.ChoiceHovered -= DisplayTooltip;
        UIChoice.ChoiceUnHovered -= OnChoiceUnHovered;
    }

    private void DisplayTooltip(Action action)
    {
        if (EaseInHappening == false)
        {
            EaseInHappening = true;
            thisRect.DOAnchorPosX(ExtendedX, 0.3f).SetEase(Ease.OutBack).OnComplete(OnEaseInComplete);
        }
        hideSequence.Kill();
        hideSequence = null;
        ChoiceImage.sprite = action._Image_Sprite;
        ChoiceNameText.text = action._Name;
        ChoiceDescriptionText.text = action._Description;

        foreach (GameObject gameobject in ConsequenceDisplays)
        {
            gameobject.SetActive(false);
        }

        int numbOfConsequences = 0;

        if (action._Image != "")
        {
            ConsequenceDisplays[numbOfConsequences].SetActive(true);
            ConsequenceDisplays[numbOfConsequences].GetComponent<UIConsequenceTooltip>().DisplayConsequence(0, action._Image);
            numbOfConsequences++;
        }
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

    private void OnEaseInComplete()
    {
        EaseInHappening = false;
    }

    private void OnChoiceUnHovered(Action action)
    {
        ClearTooltip();
    }

    public void ClearTooltip()
    {
        if (thisRect.anchoredPosition.x != originalX && hideSequence == null)
        {
            hideSequence = DOTween.Sequence();
            hideSequence.Append(thisRect.DOAnchorPosX(originalX, 0.3f).SetEase(Ease.OutBack).SetDelay(0.5f));
        }
    }
}
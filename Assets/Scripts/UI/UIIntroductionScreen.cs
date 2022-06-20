using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIIntroductionScreen : MonoBehaviour
{
    public BoolVariable skipTutorial;
    public CanvasGroup IntroCanGroup;
    public CanvasGroup RoleSelectioncanGroup;

    public void OnIntroductionScreenActivate()
    {
        if (skipTutorial.Value == false)
        {
            IntroCanGroup.alpha = 1;
            IntroCanGroup.interactable = true;
            IntroCanGroup.blocksRaycasts = true;
        }
        else
        {
            RoleSelectioncanGroup.alpha = 1;
            RoleSelectioncanGroup.interactable = true;
            RoleSelectioncanGroup.blocksRaycasts = true;
        }
    }
}
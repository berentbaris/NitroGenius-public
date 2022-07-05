using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIIntroductionScreen : MonoBehaviour
{
    public BoolVariable TutorialEnabled;
    public CanvasGroup IntroCanGroup;
    public Role_selection_script roleSelection;

    public void OnIntroductionScreenActivate()
    {
        if (TutorialEnabled.Value == true)
        {
            IntroCanGroup.alpha = 1;
            IntroCanGroup.interactable = true;
            IntroCanGroup.blocksRaycasts = true;
        }
        else
        {
            roleSelection.ActivateRoleSelectionScreen();
        }
    }
}
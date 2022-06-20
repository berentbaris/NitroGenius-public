using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDetailsPanel : MonoBehaviour
{
    public CanvasGroup canvasgroup;

    public void OpenDetailsPanel()
    {
        canvasgroup.alpha = 1;
        canvasgroup.interactable = true;
        canvasgroup.blocksRaycasts = true;
    }

    public void CloseDetailsPanel()
    {
        canvasgroup.alpha = 0;
        canvasgroup.interactable = false;
        canvasgroup.blocksRaycasts = false;
    }
}
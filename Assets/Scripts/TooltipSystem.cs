using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem current;
    public Tooltip tooltip;


    private void Awake()
    {
        current = this;
    }

    public static void Show(string content, string header = "")
    {
        current.tooltip.SetText(content, header);
        current.tooltip.canGroup.DOFade(1, 0.2f);
    }

    public static void Hide()
    {
        current.tooltip.canGroup.DOFade(0, 0.2f);
    }
}
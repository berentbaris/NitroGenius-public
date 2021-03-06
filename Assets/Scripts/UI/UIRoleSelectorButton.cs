using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class UIRoleSelectorButton : MonoBehaviour
{
    public static event Action<Sector> SelectionChange;
    public Sector selectedSector;
    public Image bgCircle;
    public Image icon;
    private Color originalColour;
    private float fadetime = 0.2f;
    public Sound SectorSelectSound;
    public BoolVariable aiEnabledBool;

    private void Awake()
    {
        SelectionChange += OnSelectionChange;
        originalColour = icon.color;
    }

    private void OnDestroy()
    {
        SelectionChange -= OnSelectionChange;
    }

    public void OnSelect()
    {
        selectedSector.controllerAgent = Controller.Player;
        SelectionChange(selectedSector);
        icon.DOColor(Color.white, fadetime);
        this.transform.DOScale(new Vector3(1.2f, 1.2f, 1), fadetime).SetEase(Ease.OutElastic);
        SectorSelectSound.PlaySound();
    }

    private void OnSelectionChange(Sector sector)
    {
        if (sector != selectedSector)
        {
            icon.DOColor(originalColour, fadetime);
            this.transform.DOScale(new Vector3(1f, 1f, 1), fadetime).SetEase(Ease.OutElastic);

            if (aiEnabledBool.Value == true)
            {
                selectedSector.controllerAgent = Controller.AI;
            }
            else
            {
                selectedSector.controllerAgent = Controller.None;
            }
        }
    }
}
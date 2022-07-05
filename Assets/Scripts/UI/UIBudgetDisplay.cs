using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.EventSystems;

public class UIBudgetDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI BudgetDisplayText;
    private Sector selectedSector;
    private int localBudget;
    private float fadetime = 0.5f;

    private void Awake()
    {
        TurnController.DisplayActionSelectionScreen += DisplayBudget;
        UIChoice.ChoiceSelected += OnBudgetChange;
        UIChoice.ChoiceUnselected += OnBudgetChange;
    }

    private void OnDestroy()
    {
        TurnController.DisplayActionSelectionScreen -= DisplayBudget;
        UIChoice.ChoiceSelected -= OnBudgetChange;
        UIChoice.ChoiceUnselected -= OnBudgetChange;
    }

    public void OnBudgetChange(Action action)
    {
        StartCoroutine(CounterDisplayBudget());
    }

    private void DisplayBudget(Sector sector)
    {
        selectedSector = sector;
        localBudget = Mathf.RoundToInt(selectedSector.Budget);
        BudgetDisplayText.text = localBudget.ToString();
    }

    private IEnumerator CounterDisplayBudget()
    {
        float currentTime = 0f;
        while (currentTime <= fadetime)
        {
            float transitionAmount = Mathf.Lerp(localBudget, selectedSector.Budget, currentTime / fadetime);
            localBudget = Mathf.RoundToInt(transitionAmount);
            BudgetDisplayText.text = Mathf.RoundToInt(transitionAmount).ToString();
            currentTime += Time.deltaTime;
            yield return null;
        }
        localBudget = Mathf.RoundToInt(selectedSector.Budget);
        BudgetDisplayText.text = localBudget.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.Show("Your budget for this year", "Budget");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }
}
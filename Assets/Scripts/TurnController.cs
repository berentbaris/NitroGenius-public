using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableEvents.Events;
using UnityEngine.Events;
using UnityEngine.UI;

public class TurnController : MonoBehaviour
{
    public UnityEvent checkeventEvent;
    public IntVariable turn;
    public IntVariable currentYear;
    public FloatVariable turn_timer;
    public FloatVariable remainingTime;
    public List<float> turnTimerList = new List<float>();
    public UnityEvent turnEndEvent;
    private Coroutine timerCoroutine;
    private CanvasGroup canvasGroup;
	public SectorList sectorList;
    public SourceList sourceList;
    public ActivityData ActivityData;
    public NationalData NationalData;
    public ActionData ActionData;
    public DepositionData DepositionData;
    public AIController aiController;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        Role_selection_script.StartGame += OnStartGame;
    }

    private void OnDestroy()
    {
        Role_selection_script.StartGame -= OnStartGame;
    }

    private void OnStartGame(Sector sector)
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        timerCoroutine = StartCoroutine(TurnTimer());
    }

    public void StartTurn()
    {
        turn.SetValue(turn.Value + 1);
        currentYear.SetValue(currentYear.Value + 1);
        timerCoroutine = StartCoroutine(TurnTimer());
        ActionData.OnTurnChange(turn.Value);
        aiController.MakeAiDecisions();

        NationalData.RecordPreviousTurnValues();
        
        foreach (Sector sector in sectorList.list)
        {
            sector.RecordPreviousTurnValues();
        }

        NationalData.CalculateN2000Percentage();
    }

    private IEnumerator TurnTimer()
    {
        turn_timer.SetValue(turnTimerList[turn.Value]);
        remainingTime.SetValue(turn_timer.Value);
        while (remainingTime.Value > 0)
        {
            remainingTime.SetValue(remainingTime.Value - Time.deltaTime);
            yield return null;
        }
        OnTimerEnd();
    }

    private void OnTimerEnd()
    {
        foreach (Sector sector in sectorList.list)
        {
            sector.Display_Budget = 0;
            sector.Budget = sector.Budget / 4;
            sector.NH3_Emissions = 0;
            sector.Nox_Emissions = 0;
            sector.N2O_Emissions = 0;
            sector.Product_Volume_Total = 0;
            sector.Fertilizer_Input = 0;
            sector.Fertilizer_NH3_Total = 0;
            sector.NUE = 0;
        }
        foreach (Source source in sourceList.list)
        {
            source.NH3_Emissions = 0;
            source.Nox_Emissions = 0;
        }
        NationalData._NationalItem._National_Total_Deposition = 0;
        NationalData._NationalItem._National_NH3_Emissions = 0;
        NationalData._NationalItem._National_Nox_Emissions = 0;
        //The meat
        foreach (Sector sector in sectorList.list)
        {
            foreach (Action action in sector.SelectedChoices)
            {
                foreach (Effect effect in action._Effects)
                {
                    effect.ApplyEffect();
                }
            }
        }
        checkeventEvent.Invoke();
        foreach (Action action in sectorList.list[4].SelectedChoices)
        {
            foreach (Effect effect in action._Effects)
            {
                effect.ApplyEffect();
            }
        }
        foreach (Activity activity in ActivityData._ActivityItems)
        {
            activity.Calculate();
        }
        ActivityData.CalculateBudget();
        foreach (Sector sector in sectorList.list)
        {
            foreach (Action action in sector.SelectedChoices)
            {
                foreach (Effect effect in action._Effects)
                {
                    effect.ApplyEffectAfter();
                }
            }
        }
        foreach (Action action in sectorList.list[4].SelectedChoices)
        {
            foreach (Effect effect in action._Effects)
            {
                effect.ApplyEffectAfter();
            }
        }
        ActivityData.CalculateSectorEmissions();
        ActivityData.CalculateSourceEmissions();
        DepositionData.LocalizeEmissions();
        DepositionData.CalculateDeposition();
        NationalData.CalculateNationalDeposition();
        NationalData.CalculateN2000Percentage();
        NationalData.CalculateHappiness();
        NationalData.CalculateScore();
        turnEndEvent.Invoke();

        foreach (Sector sector in sectorList.list)
        {
            foreach (Action action in sector.SelectedChoices)
            {
            if (action._Repetivity == "ExecOnce")
                {
                    sector.Actions.Remove (action);
                }
                if (action._Repetivity == "Rotating")
                {
                    sector.Actions.Remove (action);
                    action._ExecutedTurn = turn.Value;
                }
            }
        } 
    }

    public void OnSubmitButtonPress()
    {
        StopCoroutine(timerCoroutine);
        OnTimerEnd();
    }
}

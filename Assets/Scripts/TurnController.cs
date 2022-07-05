using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableEvents.Events;
using UnityEngine.Events;
using UnityEngine.UI;

public class TurnController : MonoBehaviour
{
    public SimpleScriptableEvent checkeventEvent;
    public IntVariable turn;
    public IntVariable currentYear;
    public FloatVariable turn_timer;
    public FloatVariable remainingTime;
    public List<float> turnTimerList = new List<float>();
    public UnityEvent turnEndEvent;
    private Coroutine timerCoroutine;
	public SectorList sectorList;
    public SourceList sourceList;
    public ActivityData ActivityData;
    public NationalData NationalData;
    public ActionData ActionData;
    public DepositionData DepositionData;
    public AIController aiController;
    public static event Action<Sector> DisplayActionSelectionScreen;
    public List<Sector> playerSectors = new List<Sector>();
    private int currentPlayer = 0;

    public void OnStartGame()
    {
        timerCoroutine = StartCoroutine(TurnTimer());

        foreach (Sector sector in sectorList.list)
        {
            sector.SelectedChoices.Clear();
            sector.Action_Limit_Per_Turn = 3;
            if (sector.controllerAgent == Controller.Player)
            {
                playerSectors.Add(sector);
            }
        }
        aiController.MakeAiDecisions();
        DisplayActionSelectionScreen(playerSectors[currentPlayer]);
    }

    public void StartTurn()
    {
        turn.SetValue(turn.Value + 1);
        currentYear.SetValue(currentYear.Value + 1);
        timerCoroutine = StartCoroutine(TurnTimer());
        ActionData.OnTurnChange(turn.Value);

        foreach (Sector eachsector in sectorList.list)
        {
            eachsector.SelectedChoices.Clear();
            eachsector.Action_Limit_Per_Turn = 3;
        }

        DepositionData.LocalizeEmissions();
        DepositionData.CalculateDeposition();
        NationalData.CalculateN2000Percentage();

        DisplayActionSelectionScreen(playerSectors[0]);

        aiController.MakeAiDecisions();

        NationalData.RecordPreviousTurnValues();
        
        foreach (Sector sector in sectorList.list)
        {
            sector.RecordPreviousTurnValues();
        }
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
        OnActionsComplete();
    }

    private void OnTurnEnd()
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
            sector.Fertilizer_Output = 0;
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
        ActivityData.CalculateSectorEmissions();
        ActivityData.CalculateSourceEmissions();
        DepositionData.LocalizeEmissions();
        DepositionData.CalculateDeposition();
        NationalData.CalculateNationalDeposition();
        NationalData.CalculateN2000Percentage();
        NationalData.CalculateHappiness();
        NationalData.CalculateSectorScore();
        checkeventEvent.Raise();
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
        OnActionsComplete();
    }

    public void OnActionsComplete()
    {
        currentPlayer++;
        if (playerSectors.Count == currentPlayer)
        {
            OnTurnEnd();
            currentPlayer = 0;
            DisplayActionSelectionScreen(playerSectors[currentPlayer]);
        }
        else
        {
            DisplayActionSelectionScreen(playerSectors[currentPlayer]);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Sector : ScriptableObject
{
    public int ID;
    public string Description;
    public Controller controllerAgent;
    public string Concerns;
    public Sprite Icon;
    public ColorVariable color;
    public float Budget;
    public float Display_Budget;
    public List<float> DisplayBudgetRecord = new List<float>();
    public float Product_Volume_Total;
    public List<float> ProductVolumeTotalRecord = new List<float>();
    public float Image;
    public float Fertilizer_Input;
    public float Fertilizer_Output;
    public float NUE;
    public List<float> NUERecord = new List<float>();
    public float N2O_Emissions;
    public List<float> N20Record = new List<float>();
    public float NH3_Emissions;
    public List<float> NH3Record = new List<float>();
    public float Nox_Emissions;
    public List<float> NoxRecord = new List<float>();
    public float Action_Limit_Per_Turn;
    public float score;
    public float public_health;

    public List<Action> Actions = new List<Action>();
    public List<Action> SelectedChoices = new List<Action>();

    public void ChoiceSelected(Action choice)
    {
        Action_Limit_Per_Turn--;
        Budget -= choice._Cost;
        SelectedChoices.Add(choice);
    }

    public void ChoiceUnselected(Action choice)
    {
        Action_Limit_Per_Turn++;
        Budget += choice._Cost;
        SelectedChoices.Remove(choice);
    }

    public void ChangeController(int value)
    {
        switch (value)
        {
            case 0:
                controllerAgent = Controller.Player;
                break;
            case 1:
                controllerAgent = Controller.AI;
                break;
            case 2:
                controllerAgent = Controller.None;
                break;
            default:
                break;
        }
    }

    public void RecordPreviousTurnValues()
    {
        DisplayBudgetRecord.Add(Display_Budget);
        N20Record.Add(N2O_Emissions);
        NH3Record.Add(NH3_Emissions);
        NoxRecord.Add(Nox_Emissions);
        ProductVolumeTotalRecord.Add(Product_Volume_Total);
        NUERecord.Add(NUE);
    }

    public void ClearRecords()
    {
        DisplayBudgetRecord.Clear();
        N20Record.Clear();
        NH3Record.Clear();
        NoxRecord.Clear();
        ProductVolumeTotalRecord.Clear();
        NUERecord.Clear();
    }
}

public enum Controller
{
    Player,
    AI,
    None,
}
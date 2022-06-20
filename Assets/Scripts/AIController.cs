using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public SectorList sectorList;
    public AIData aiData;
    public ActionData actionData;
    public IntVariable currentYear;
    private Sector playerControlledSector;
    private List<Action> AiActionsThisTurn = new List<Action>();

    private void Awake()
    {
        Role_selection_script.StartGame += OnGameStart;
    }

    private void OnDestroy()
    {
        Role_selection_script.StartGame -= OnGameStart;
    }

    private void OnGameStart(Sector sector)
    {
        playerControlledSector = sector;
        MakeAiDecisions();
    }

    public void MakeAiDecisions()
    {
        AiActionsThisTurn.Clear();

        foreach (Sector sector in sectorList.list)
        {
            sector.SelectedChoices.Clear();
            sector.Action_Limit_Per_Turn = 3;
        }

        foreach (AI aiAction in aiData._AIItems)
        {
            if (aiAction.Year == currentYear.Value && sectorList.list[aiAction.Role_ID] != playerControlledSector)
            {
                AiActionsThisTurn.Add(actionData.GetAction(aiAction.Action_ID));
            }
        }

        foreach (Action action in AiActionsThisTurn)
        {
            action._Belonging_Sector.SelectedChoices.Add(action);
        }
    }
}
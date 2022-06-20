using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public Sector eventSector;
    public Sector governmentSector;
    public IntVariable currentYear;
    public NationalData natData;
    public ActionData ActionData;
    private Sector selectedSector;
    public SectorList sectorList;

    private void Awake()
    {
        Role_selection_script.StartGame += GetSelectedSector;
    }

    private void OnDestroy()
    {
        Role_selection_script.StartGame -= GetSelectedSector;
    }

    public void GetSelectedSector(Sector sector)
    {
        selectedSector = sector;
    }

    public void checkeventEvent()
    {
        if (currentYear.Value == 2024)
        {
            Action lawsuit_action = ActionData.GetAction(238);
            eventSector.SelectedChoices.Add(lawsuit_action);
        }
        if (currentYear.Value == 2025)
        {
            if (natData._NationalItem._N2000_Below_Critical >= 40)
            {
                Action on_target_2025 = ActionData.GetAction(248);
                if (eventSector.Actions.Contains(on_target_2025))
                {
                    eventSector.SelectedChoices.Add(on_target_2025);
                }
            }
            else
            {
                Action not_on_target_2025 = ActionData.GetAction(249);
                if (eventSector.Actions.Contains(not_on_target_2025))
                {
                    eventSector.SelectedChoices.Add(not_on_target_2025);
                }

            }
        }
        if (currentYear.Value == 2027)
        {
            if (natData._NationalItem._N2000_Below_Critical <= 40)
            {
                Action targeted_buyouts = ActionData.GetAction(252);
                if (eventSector.Actions.Contains(targeted_buyouts))
                {
                    eventSector.SelectedChoices.Add(targeted_buyouts);
                }
                Action gp_wins_action = ActionData.GetAction(239);
                if (eventSector.Actions.Contains(gp_wins_action))
                {
                    eventSector.SelectedChoices.Add(gp_wins_action);
                }
            }
            else
            {
                Action gp_loses_action = ActionData.GetAction(240);
                if (eventSector.Actions.Contains(gp_loses_action))
                {
                    eventSector.SelectedChoices.Add(gp_loses_action);
                }
            }
        }
        //2028 protests
        if (currentYear.Value == 2028)
        {
            Action targeted_buyouts = ActionData.GetAction(252);
            if (!eventSector.Actions.Contains(targeted_buyouts))
            {
                Action protests = ActionData.GetAction(253);
                if (eventSector.Actions.Contains(protests))
                {
                    eventSector.SelectedChoices.Add(protests);
                }
            }
        }
        //2030 target
        if (currentYear.Value == 2030)
        {
            
            if (natData._NationalItem._N2000_Below_Critical >= 50)
            {
                Action on_target_2030 = ActionData.GetAction(250);
                if (eventSector.Actions.Contains(on_target_2030))
                {
                    eventSector.SelectedChoices.Add(on_target_2030);
                }
            } 
            else
            {
                Action not_on_target_2030 = ActionData.GetAction(251);
                if (eventSector.Actions.Contains(not_on_target_2030))
                {
                    eventSector.SelectedChoices.Add(not_on_target_2030);
                }
            }
        }
        //housing crisis
        Action action = ActionData.GetAction(242);
        if (!governmentSector.Actions.Contains(action))
        {
            Action housing_crisis_action = ActionData.GetAction(243);
            if (eventSector.Actions.Contains(housing_crisis_action))
            {
                eventSector.SelectedChoices.Add(housing_crisis_action);
            }
        }
        //transnational deposition
        if (currentYear.Value == 2031)
        {
            Action foreign_deposition_action = ActionData.GetAction(241);
            if (eventSector.Actions.Contains(foreign_deposition_action))
            {
                eventSector.SelectedChoices.Add(foreign_deposition_action);
            }
        }
        //last push
        if (currentYear.Value == 2033)
        {
            if (selectedSector != sectorList.list[3])
            {
                if (natData._NationalItem._N2000_Below_Critical < 70)
                {
                    Action last_push = ActionData.GetAction(244);
                    if (governmentSector.Actions.Contains(last_push))
                    {
                        governmentSector.SelectedChoices.Add(last_push);
                    }
                }
            }
        }
        //2034 protests
        if (currentYear.Value == 2034)
        {
            Action targeted_buyouts_2 = ActionData.GetAction(252);
            Action last_push = ActionData.GetAction(244);
            if (!eventSector.Actions.Contains(targeted_buyouts_2))
            {
                    Action protests2034 = ActionData.GetAction(257);
                    if (eventSector.Actions.Contains(protests2034))
                    {
                        eventSector.SelectedChoices.Add(protests2034);
                    }
            }
        } 
    }
}
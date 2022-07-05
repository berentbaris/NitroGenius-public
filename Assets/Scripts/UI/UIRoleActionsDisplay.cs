using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRoleActionsDisplay : MonoBehaviour
{
    public BoolVariable TutorialEnabled;
    public CanvasGroup RoleActionsCanGroup;
    public CanvasGroup MaincanGroup;
    public UIChoiceList RoleActionsList;
    public SectorList sectorlist;
    public GameObject FullListRoleSelectorPrefab;
    public Transform parentTransform;
    public UIChoiceList choicelist;

    public void OnRoleActionsScreenActivate()
    {
        if (TutorialEnabled.Value == true)
        {
            RoleActionsCanGroup.alpha = 1;
            RoleActionsCanGroup.interactable = true;
            RoleActionsCanGroup.blocksRaycasts = true;

            List<Sector> playerSectors = new List<Sector>();

            foreach (Sector sector in sectorlist.list)
            {
                if (sector.controllerAgent == Controller.Player)
                {
                    playerSectors.Add(sector);
                }
            }

            for (int i = 0; i < playerSectors.Count; i++)
            {
                GameObject obj = Instantiate(FullListRoleSelectorPrefab);
                obj.transform.SetParent(parentTransform, false);
                UIRoleFullActionsButton choice = obj.GetComponent<UIRoleFullActionsButton>();
                choice.OnCreateButton(choicelist, playerSectors[i]);
                if (i == 0)
                {
                    choice.OnSelect();
                }
            }
        }
        else
        {
            MaincanGroup.alpha = 1;
            MaincanGroup.interactable = true;
            MaincanGroup.blocksRaycasts = true;
        }
    }
}

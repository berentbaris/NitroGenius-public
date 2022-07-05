using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIChoiceList : MonoBehaviour
{
    public Transform parentTransform;
    public GameObject cellPrefab;
    private List<GameObject> choiceCells = new List<GameObject>();
    public IntVariable currentYear;
    public bool fullList = false;

    private void Awake()
    {
        TurnController.DisplayActionSelectionScreen += PopulateList;
    }

    private void OnDestroy()
    {
        TurnController.DisplayActionSelectionScreen -= PopulateList;
    }

    public void PopulateList(Sector sector)
    {
        ClearList();
        foreach (Action action in sector.Actions)
        {
            if (fullList == true)
            {
                GameObject obj = Instantiate(cellPrefab);
                choiceCells.Add(obj);
                obj.transform.SetParent(parentTransform, false);
                UIChoice choice = obj.GetComponent<UIChoice>();
                choice.FullListChoice = true;
                choice.SetChoice(action);
            }
            else if (action._Priority <= currentYear.Value)
            {
                GameObject obj = Instantiate(cellPrefab);
                choiceCells.Add(obj);
                obj.transform.SetParent(parentTransform, false);
                UIChoice choice = obj.GetComponent<UIChoice>();
                choice.FullListChoice = false;
                choice.SetChoice(action);
            }
        }
    }

    private void ClearList()
    {
        foreach (GameObject UIChoiceGameobject in choiceCells)
        {
            Destroy(UIChoiceGameobject);
        }
        choiceCells.Clear();
    }
}
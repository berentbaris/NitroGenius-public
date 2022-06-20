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
    private Sector chosenSector;
    public IntVariable currentYear;

    private void Awake()
    {
        Role_selection_script.StartGame += OnStartGame;
    }

    private void OnDestroy()
    {
        Role_selection_script.StartGame -= OnStartGame;
    }

    private void OnStartGame(Sector sector)
    {
        chosenSector = sector;
        ClearList();
        PopulateList();
    }

    public void OnStartTurn()
    {
        ClearList();
        PopulateList();
    }

    private void PopulateList()
    {
        foreach (Action action in chosenSector.Actions)
        {
            if (action._Priority <= currentYear.Value)
            {
                GameObject obj = Instantiate(cellPrefab);
                choiceCells.Add(obj);
                obj.transform.SetParent(parentTransform, false);
                obj.GetComponent<UIChoice>().SetChoice(action);
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
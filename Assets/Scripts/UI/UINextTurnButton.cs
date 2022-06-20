using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UINextTurnButton : MonoBehaviour
{
    public IntVariable turn;
    public UnityEvent NextTurnEvent;
    public UnityEvent GameEndEvent;

    public void OnNextTurnButtonPress()
    {
        if (turn.Value == 13)
        {
            GameEndEvent.Invoke();
        }
        else
        {
            NextTurnEvent.Invoke();
        }
    }
}
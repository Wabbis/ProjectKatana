using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

    private Dictionary<Type, BaseState> availableStates;

    public BaseState currentState { get; private set; }
    public event Action<BaseState> OnStateChanged;

    public void SetStates(Dictionary<Type, BaseState> states)
    {
        availableStates = states;
    }

    private void Update()
    {
        if(currentState == null)
        {
            currentState = availableStates.Values.First();
            Debug.Log("State Chanded to " + currentState.ToString());
        }

        var nextState = currentState?.Tick();

        if(nextState != null && nextState != currentState?.GetType())
        {
            SwitchToState(nextState);
        }
    }

    private void SwitchToState(Type nextState)
    {
        currentState = availableStates[nextState];
        Debug.Log("State Chanded to " + currentState.ToString());
        OnStateChanged?.Invoke(currentState);
    }
}

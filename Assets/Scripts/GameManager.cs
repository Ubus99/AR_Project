using System;
using JetBrains.Annotations;
using StateMachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    readonly StartState _startState = new StartState();

    [CanBeNull]
    IState _currentState;

    // Start is called before the first frame update
    void Start()
    {
        _currentState = _startState;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentState != null)
        {
            _currentState.Execute();
            var nextState = _currentState.GetNextState();
            if (nextState != null && nextState != _currentState.GetType())
            {
                TransitionTo(nextState);
            }
        }
        else
        {
            _currentState = _startState;
        }
    }

    void TransitionTo(Type T)
    {
        if (_currentState == null)
            return;
        _currentState.Exit();
        _currentState = Activator.CreateInstance(T) as IState;
        _currentState.Enter();

    }
}

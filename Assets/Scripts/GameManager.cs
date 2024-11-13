using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using StateMachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    readonly IState _startState = new StartState();
    readonly Dictionary<string, IState> _stateCache = new Dictionary<string, IState>();

    [CanBeNull]
    IState _currentState;

    void Awake()
    {
        Application.targetFrameRate = 30;
    }

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

        string stateName = T.ToString();
        if (_stateCache.TryGetValue(stateName, out var state))
        {
            _currentState = state ?? new ErrorState();
        }
        else
        {
            _currentState = Activator.CreateInstance(T) as IState ?? new ErrorState();
            _stateCache.Add(stateName, _currentState);
        }

        _currentState.Enter();
    }
}

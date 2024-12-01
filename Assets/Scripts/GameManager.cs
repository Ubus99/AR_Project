using System;
using System.Collections.Generic;
using Spawners;
using StateMachine;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

public class GameManager : MonoBehaviour
{

    public ImageSpawnManager spawnManager;
    public EggSpawnManager eggSpawnManager;
    public ARRaycastManager raycastManager;

    public Vector2 mTapStartPosition;

    [SerializeField]
    XRInputValueReader<Vector2> mTapStartPositionInput = new XRInputValueReader<Vector2>("Tap Start Position");

    readonly Dictionary<string, AbstractMSM> _stateCache = new Dictionary<string, AbstractMSM>();

    AbstractMSM _currentState;

    public XRInputValueReader<Vector2> tapStartPosition
    {
        get => mTapStartPositionInput;
        set => XRInputReaderUtility.SetInputProperty(ref mTapStartPositionInput, value, this);
    }

    public bool tapStartThisFrame
    {
        get
        {
            var prevTapStartPosition = mTapStartPosition;
            return tapStartPosition.TryReadValue(out mTapStartPosition) &&
                prevTapStartPosition != mTapStartPosition;
        }
    }

    void Awake()
    {
        Application.targetFrameRate = 30;
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentState = new StartState(this);
        spawnManager = FindObjectOfType<ImageSpawnManager>();
        eggSpawnManager = FindObjectOfType<EggSpawnManager>();
        raycastManager = FindObjectOfType<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentState == null)
        {
            _currentState = new ErrorState(this);
        }
        else
        {
            _currentState.Execute();
            TransitionTo(_currentState.GetNextState());
        }
    }

    void TransitionTo(Type T)
    {
        if (_currentState == null || T == _currentState.GetType())
            return;

        _currentState.Exit();

        string stateName = T.ToString();
        if (_stateCache.TryGetValue(stateName, out var state))
        {
            _currentState = state ?? new ErrorState(this);
        }
        else
        {
            _currentState = (AbstractMSM)Activator.CreateInstance(T, new object[] { this }) ?? new ErrorState(this);
            _stateCache.Add(stateName, _currentState);
        }

        _currentState.Enter();
    }
}

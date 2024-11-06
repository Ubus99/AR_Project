using JetBrains.Annotations;
using StateMachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    readonly StartState _startState = new StartState();

    [CanBeNull]
    public AbstractState<GameManager> CurrentState;

    // Start is called before the first frame update
    void Start()
    {
        CurrentState = _startState;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentState?.Update();
    }
}

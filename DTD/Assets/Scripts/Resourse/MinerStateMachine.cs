using System;
using UnityEngine;

public class MinerStateMachine : MonoBehaviour
{
    [SerializeField] private MinerState _firstState;

    private MinerState _currentState;

    private void Start()
    {
        _currentState = _firstState;
        _currentState.Enter();
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        MinerState nextState = _currentState.GetNextState();
        if (nextState != null)
            SwitchState(nextState);
    }

    private void SwitchState(MinerState nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter();
    }
}

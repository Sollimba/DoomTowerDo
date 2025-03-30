using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] private Transition[] _transition;

    public void Enter()
    {
        if (enabled == false)
        {
            enabled = true;

            foreach (var transition in _transition)
                transition.enabled = true;
        }
    }

    public void Exit()
    {
        if (enabled == true)
        {
            foreach (var transition in _transition)
            {
                transition.enabled = false;
            }
            enabled = false;
        }
    }

    public State GetNextState()
    {
        foreach (var transition in _transition)
        {
            if (transition.NeedSwitch)
            {
                return transition.NextState;
            }
        }
        return null;
    }
}

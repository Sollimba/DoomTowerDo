using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _nextState;

    public bool NeedSwitch {  get; protected set; }


    public State NextState
    {
        get => _nextState;
    }

    private void OnEnable()
    {
        NeedSwitch = false;
    }

    private void OnDisable()
    {
        NeedSwitch = false;
    }
}

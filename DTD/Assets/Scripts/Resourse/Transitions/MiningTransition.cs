using UnityEngine;

public class MiningTransition : MinerTransition
{
    private bool _isBuildingPlaced;

    public bool IsBuildingPlaced
    {
        get => _isBuildingPlaced;
        set { _isBuildingPlaced = value; }
    }

    private void Update ()
    {
        if (_isBuildingPlaced) 
            NeedSwitch = true;
    }
}

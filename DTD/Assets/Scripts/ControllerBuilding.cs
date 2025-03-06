using UnityEngine;

public class ControllerBuilding : MonoBehaviour
{
    private static ControllerBuilding _instance;

    public static ControllerBuilding Instance {  get { return _instance; } }

    private Building[,] _grid;

    public Building[,] Grid
    {
        get => _grid;
        set
        {
            _grid = value;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }
}

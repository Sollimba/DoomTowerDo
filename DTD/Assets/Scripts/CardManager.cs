using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CardManager : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Card _cardSO;

    public Card CardSO
    {
        get => _cardSO;
        set { _cardSO = value; }
    }

    private GameObject _draggingBuilding;
    private Building _building;

    private Vector2Int _gridSize = new Vector2Int(15, 10); //размер поля
    private bool _isAvailableToBuild;

    private ControllerBuilding _controllerBuilding;

    private void Awake()
    {
        _controllerBuilding = ControllerBuilding.Instance;
        _controllerBuilding.Grid = new Building[_gridSize.x, _gridSize.y];
    }

    public void OnDrag(PointerEventData eventData)
    {
        var groundPlane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (groundPlane.Raycast(ray, out float pos))
        {
            Vector3 worldPosition = ray.GetPoint(pos);
            int x = Mathf.RoundToInt(worldPosition.x);
            int z = Mathf.RoundToInt(worldPosition.z);

            if (x < 0 || x > _gridSize.x - _building.BuildingSize.x)
                _isAvailableToBuild = false;
            else if (z < 0 || z > _gridSize.y - _building.BuildingSize.y)
                _isAvailableToBuild = false;
            else
                _isAvailableToBuild = true;

            if(_isAvailableToBuild && IsPlaceTaken(x, z)) _isAvailableToBuild = false;

            if ((x% 2 == 1) ||  (z % 2 == 1)) _isAvailableToBuild = false;

            _draggingBuilding.transform.position = new Vector3(x, 0, z);

            _building.SetColor(_isAvailableToBuild);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _draggingBuilding = Instantiate(_cardSO.prefab, Vector3.zero, Quaternion.identity);
        _building = _draggingBuilding.GetComponent<Building>();

        var groundPlane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (groundPlane.Raycast(ray, out float pos))
        { 
            Vector3 worldPosition = ray.GetPoint(pos);
            int x = Mathf.RoundToInt(worldPosition.x);
            int z = Mathf.RoundToInt(worldPosition.z);

            _draggingBuilding.transform. position = new Vector3(x, 0, z);
        }
         
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(!_isAvailableToBuild)
            Destroy(_draggingBuilding);
        else
        {
            _controllerBuilding.Grid[(int)_draggingBuilding.transform.position.x, (int)_draggingBuilding.transform.position.z] = _building;
            _building.ResetColor();
        }
    }

    private bool IsPlaceTaken(int x, int y)
    {
        if (_controllerBuilding.Grid[x,y] != null)
            return true;
        return false;
    }
}

using SmallHedge.SoundManager;
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
    private Building hoveredBuildingToDelete;

    private Vector2Int _gridSize = new Vector2Int(15, 10); //размер пол€
    private bool _isAvailableToBuild;

    private ControllerBuilding _controllerBuilding;
    private ResourceCounter _resourceCounter;

    [SerializeField] private GameObject floatingTextPrefab;

    public bool IsAbleToPlant { get; set; }

    private void Awake()
    {
        _controllerBuilding = ControllerBuilding.Instance;
        _controllerBuilding.Grid = new Building[_gridSize.x, _gridSize.y];
        _resourceCounter = ResourceCounter.Instance;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (hoveredBuildingToDelete != null)
        {
            hoveredBuildingToDelete.ResetColor();
            hoveredBuildingToDelete = null;
        }
        if (IsAbleToPlant && _draggingBuilding != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float pos))
            {
                Vector3 worldPosition = ray.GetPoint(pos);
                int x = Mathf.RoundToInt(worldPosition.x);
                int z = Mathf.RoundToInt(worldPosition.z);

                if (_cardSO.prefab.GetComponent<DeleteTool>() != null)
                {
                    // ћы используем карту удалени€
                    if (x >= 0 && x < _gridSize.x && z >= 0 && z < _gridSize.y)
                    {
                        hoveredBuildingToDelete = _controllerBuilding.Grid[x, z];
                        if (hoveredBuildingToDelete != null)
                            hoveredBuildingToDelete.SetColor(false); // подсветить красным
                    }
                    else
                    {
                        hoveredBuildingToDelete = null;
                    }
                    if (hoveredBuildingToDelete != null)
                        hoveredBuildingToDelete.SetColor(false); // подсветить красным
                    _draggingBuilding.transform.position = new Vector3(x, 0.1f, z);
                    return;
                }

                // ќбычное построение
                _isAvailableToBuild = (x >= 0 && x <= _gridSize.x - _building.BuildingSize.x) &&
                                      (z >= 0 && z <= _gridSize.y - _building.BuildingSize.y) &&
                                      !IsPlaceTaken(x, z) &&
                                      ((z % 2 == 0) && (x % 2 == 0));

                _draggingBuilding.transform.position = new Vector3(x, 0, z);
                _building.SetColor(_isAvailableToBuild);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsAbleToPlant)
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

                _draggingBuilding.transform.position = new Vector3(x, 0, z);
            }
            _draggingBuilding.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (IsAbleToPlant)
        {
            if (_cardSO.prefab.GetComponent<DeleteTool>() != null)
            {
                // ќбработка удалени€
                if (hoveredBuildingToDelete != null)
                {
                    Vector3 pos = hoveredBuildingToDelete.transform.position;
                    _controllerBuilding.Grid[(int)pos.x, (int)pos.z] = null;
                    int refundAmount = hoveredBuildingToDelete.BuildCost / 2;
                    _resourceCounter.ReceiveResources(refundAmount);

                    if (floatingTextPrefab != null)
                    {
                        GameObject popup = Instantiate(floatingTextPrefab, pos + Vector3.up * 2f, Quaternion.identity);
                        popup.GetComponent<FloatingText>().SetText($"+{refundAmount}");
                    }

                    Destroy(hoveredBuildingToDelete.gameObject);
                }

                Destroy(_draggingBuilding);
                hoveredBuildingToDelete = null;
                return;
            }

            if (!_isAvailableToBuild)
            {
                Destroy(_draggingBuilding);
            }
            else
            {
                _controllerBuilding.Grid[(int)_draggingBuilding.transform.position.x, (int)_draggingBuilding.transform.position.z] = _building;
                _building.ResetColor();
                _building.SetBuildCost(_cardSO.cost);
                _draggingBuilding.GetComponent<BoxCollider>().enabled = true;

                WorkingTransition workingTransition = _draggingBuilding.GetComponent<WorkingTransition>();
                workingTransition.IsBuildingPlaced = true;

                _resourceCounter.SpendResources(_cardSO.cost);

                SoundManager.PlaySound(SoundType.BuildPlaced);
            }
        }
    }

    private bool IsPlaceTaken(int x, int y)
    {
        if (_controllerBuilding.Grid[x,y] != null)
            return true;
        return false;
    }
}

using UnityEditor.iOS.Xcode;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private Vector2 _buildingSize;
    [SerializeField] private Renderer _renderer;

    public Vector2 BuildingSize { get => _buildingSize; set {; } }

    public void SetColor(bool isAvailsbleToBuild)
    {
        if (isAvailsbleToBuild)
            _renderer.material.color = Color.green;
        else 
            _renderer.material.color = Color.red;
    }

    public void ResetColor()
    {
        _renderer.material.color = Color.white;
    }
}

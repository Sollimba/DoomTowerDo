using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private Vector2 _buildingSize;
    [SerializeField] private SpriteRenderer _spriteRenderer; // Изменено с Renderer на SpriteRenderer
    [SerializeField] private int _maxHealth;

    public int CurrentHealth { get; private set; }

    public Vector2 BuildingSize { get => _buildingSize; set {; } }

    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }

    public void SetColor(bool isAvailableToBuild)
    {
        if (isAvailableToBuild)
            _spriteRenderer.color = Color.green;
        else
            _spriteRenderer.color = Color.red;
    }

    public void ResetColor()
    {
        _spriteRenderer.color = Color.white;
    }

    public void ReceiveDamage(int damage)//проблема 
    {
        CurrentHealth -= damage;
        if (CurrentHealth < 1)
            DestroyBuilding();
    }

    private void DestroyBuilding()
    {
        Destroy(this.gameObject);
    }
}

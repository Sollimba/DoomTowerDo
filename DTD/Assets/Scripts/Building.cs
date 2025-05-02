using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    [SerializeField] private Vector2 _buildingSize;
    [SerializeField] private SpriteRenderer _spriteRenderer; // Изменено с Renderer на SpriteRenderer
    [SerializeField] private int _maxHealth;

    [SerializeField] private GameObject _healthBar;
    [SerializeField] private Image _healthBarImage;


    public int CurrentHealth { get; private set; }

    public Vector2 BuildingSize { get => _buildingSize; set {; } }

    private void Awake()
    {
        CurrentHealth = _maxHealth;
        _healthBar.SetActive(false);
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
        if(CurrentHealth == _maxHealth)
            _healthBar.SetActive(true);

        CurrentHealth -= damage;
        _healthBarImage.fillAmount = (float)CurrentHealth / (float)_maxHealth;

        if (CurrentHealth < 1)
            DestroyBuilding();
    }

    private void DestroyBuilding()
    {
        Destroy(this.gameObject);
    }
}

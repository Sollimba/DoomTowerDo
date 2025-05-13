using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float floatSpeed = 1f;
    [SerializeField] private float lifetime = 1.5f;

    private TextMeshPro text;

    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
        Destroy(gameObject, lifetime);
    }

    public void SetText(string value)
    {
        text.text = value;
    }

    private void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
    }
}

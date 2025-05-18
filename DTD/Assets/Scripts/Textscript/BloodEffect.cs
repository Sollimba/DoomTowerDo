using System.Collections;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    public Sprite[] bloodSprites;
    public float frameRate = 0.05f; // скорость смены кадров

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(PlayAnimation());
    }

    private IEnumerator PlayAnimation()
    {
        foreach (Sprite sprite in bloodSprites)
        {
            spriteRenderer.sprite = sprite;
            yield return new WaitForSeconds(frameRate);
        }

        Destroy(gameObject); // удаляем объект после анимации
    }
}

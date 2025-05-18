using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 2f;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }
}

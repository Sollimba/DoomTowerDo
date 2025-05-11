using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected int _damage;
    [SerializeField] protected int _resourceReward = 1;
    [SerializeField][Range(0f, 1f)] protected float _resourceChance = 0.7f;

    protected virtual void Awake()
    {
        Destroy(gameObject, 5f);
    }

    protected virtual void FixedUpdate()
    {
        transform.position += Vector3.right * _moveSpeed;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        EnemySettings enemySettings = other.GetComponent<EnemySettings>();
        if (enemySettings != null)
        {
            enemySettings.ReceiveDamage(_damage);

            if (Random.value <= _resourceChance)
            {
                ResourceCounter.Instance.ReceiveResources(_resourceReward);
            }

            Destroy(gameObject);
        }
    }
}

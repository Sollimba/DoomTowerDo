using UnityEngine;

public class IceProjectile : TurretProjectile
{
    [SerializeField] private float slowDuration = 1f;
    [SerializeField] private float slowMultiplier = 0.5f;
    [SerializeField] private Color slowColor = Color.cyan;

    protected override void OnTriggerEnter(Collider other)
    {
        EnemySettings enemySettings = other.GetComponent<EnemySettings>();
        if (enemySettings != null)
        {
            enemySettings.ReceiveDamage(_damage);

            if (Random.value <= _resourceChance)
            {
                ResourceCounter.Instance.ReceiveResources(_resourceReward);
            }

            enemySettings.StartSlowEffect(slowMultiplier, slowDuration, slowColor);
            Destroy(gameObject);
        }
    }
}

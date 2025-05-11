using UnityEngine;

public class RadiationProjectile : TurretProjectile
{
    [SerializeField] private float poisonDuration = 1f;
    [SerializeField] private float tickInterval = 1f;
    [SerializeField] private int tickDamage = 1;
    [SerializeField] private Color poisonColor = Color.yellow;

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

            enemySettings.StartPoisonEffect(tickDamage, tickInterval, poisonDuration, poisonColor);
            Destroy(gameObject);
        }
    }
}

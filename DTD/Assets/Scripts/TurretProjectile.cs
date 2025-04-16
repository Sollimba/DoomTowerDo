using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _damage;
    [SerializeField] private int _resourceReward = 1;

    private void Awake()
    {
        Destroy(this.gameObject, 5f);
    }

    private void FixedUpdate()
    {
        transform.position += Vector3.right * _moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemySettings enemySettings = other.GetComponent<EnemySettings>();
        if (enemySettings != null)
        {
            enemySettings.ReceiveDamage(_damage);

            // ƒобавл€ем ресурсы за каждое попадание
            ResourceCounter.Instance.ReceiveResources(_resourceReward);

            Destroy(this.gameObject);
        }
    }
}

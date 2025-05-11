using System.Collections;
using UnityEngine;

public class TurretAttackingState : State
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _projectileSpawnPosition;

    [SerializeField, Tooltip("Задержка между выстрелами в секундах (меньше значение — выше скорострельность)")]
    private float fireRate = 1.5f; // Время между выстрелами

    private IEnumerator ShootProjectile()
    {
        while (true)
        {
            Instantiate(_projectile, _projectileSpawnPosition.position, Quaternion.identity);
            yield return new WaitForSeconds(fireRate);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(ShootProjectile());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}

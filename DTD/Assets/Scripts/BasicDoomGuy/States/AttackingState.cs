using System.Collections;
using UnityEngine;

public class AttackingState : State
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _projectileSpawnPosition;


    private IEnumerator ShootProjectile ()
    {
        Instantiate(_projectile, _projectileSpawnPosition.position, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(ShootProjectile());
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

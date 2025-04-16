using System.Collections;
using UnityEngine;

public class BasicEnemyAttackingState : State
{
    private Building _buildSettings;
    [SerializeField] private int _damage;

    private BasicEnemyWalkingState _walkingState;

    private void Awake()
    {
        _walkingState = GetComponent<BasicEnemyWalkingState>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Building building = other.GetComponent<Building>();
        if (building != null)
        {
            _buildSettings = building;
            StartCoroutine(AttackBuilding());
        }
    }

    private IEnumerator AttackBuilding()
    {
        if (_buildSettings.CurrentHealth > 0)
        {
            _walkingState.CanMove = false; // Останавливаем врага
            yield return new WaitForSeconds(1);
            if (_buildSettings != null)
                _buildSettings.ReceiveDamage(_damage);
            StartCoroutine(AttackBuilding());
        }
        else
        {
            _buildSettings = null;
            _walkingState.CanMove = true; // Включаем движение снова
        }
    }
}

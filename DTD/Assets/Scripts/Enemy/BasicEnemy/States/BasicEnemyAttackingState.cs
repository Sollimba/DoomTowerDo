using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAttackingState : State
{
    private Building _buildSettings;
    [SerializeField] private int _damage;
    private EnemyWalkingTransition _enemyWalkingTransition;

    private void Awake()
    {
        _enemyWalkingTransition = GetComponent<EnemyWalkingTransition>();
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
            _enemyWalkingTransition.IsAbleToGo = false;
            yield return new WaitForSeconds(1);
            if(_buildSettings != null)
                _buildSettings.ReceiveDamage(_damage);
            StartCoroutine(AttackBuilding());
        }
        else
        {
            _buildSettings = null;
            _enemyWalkingTransition.IsAbleToGo = true;
        }
    }

    private void Update()
    {
        
    }
} 

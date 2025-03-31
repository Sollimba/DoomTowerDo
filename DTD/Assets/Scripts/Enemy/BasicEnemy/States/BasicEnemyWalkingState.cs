using UnityEngine;

public class BasicEnemyWalkingState : State
{
    [SerializeField] private float _moveSpeed;

    private void FixedUpdate()
    {
            transform.position += _moveSpeed * Vector3.left;
    }
}

using UnityEngine;

public class BasicEnemyWalkingState : State
{
    [SerializeField] private float _moveSpeed;

    public bool CanMove = true;

    private void FixedUpdate()
    {
        if (CanMove)
            transform.position += _moveSpeed * Vector3.left * Time.fixedDeltaTime;
    }
}

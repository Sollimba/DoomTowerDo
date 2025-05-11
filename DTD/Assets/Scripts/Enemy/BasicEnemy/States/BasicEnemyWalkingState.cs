using UnityEngine;

public class BasicEnemyWalkingState : State
{
    [SerializeField] private float _moveSpeed;
    private float _originalSpeed;

    public bool CanMove = true;

    private void Awake()
    {
        _originalSpeed = _moveSpeed;
    }

    private void FixedUpdate()
    {
        if (CanMove)
            transform.position += _moveSpeed * Vector3.left * Time.fixedDeltaTime;
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        _moveSpeed = _originalSpeed * multiplier;
    }

    public void ResetSpeed()
    {
        _moveSpeed = _originalSpeed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

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
        TestWaves testWaves = other.GetComponent<TestWaves>();
        if (testWaves != null)
            Destroy(other.gameObject);
            Destroy(this.gameObject);
    }
}

using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    [SerializeField] private int damageToPlayer = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            UIManager.Instance.TakeDamage(damageToPlayer);
            Destroy(other.gameObject);
        }
    }
}
using UnityEngine;

public class EnemyAttackingTransition : Transition
{
    private bool NeedAttack;

    private void Update()
    {
        if (NeedAttack)
        {
            NeedSwitch = true;
            NeedAttack = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Building building = other.GetComponent<Building>();
        if (building != null)
        {
            if (building.CurrentHealth > 0)
                NeedAttack = true;
        }
    }
}

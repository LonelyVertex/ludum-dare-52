using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    [SerializeField] private int damage;

    public void TriggerTrap(DamageableObject damageableObject)
    {
        damageableObject.Damage(gameObject.transform, damage);
        OnAfterTrapTrigger();
    }

    protected abstract void OnAfterTrapTrigger();
}

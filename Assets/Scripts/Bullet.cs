using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<DamageableObject>();
        damageable?.Damage(transform);
        Destroy(gameObject);
    }
}

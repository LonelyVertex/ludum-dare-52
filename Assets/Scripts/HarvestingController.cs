using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestingController : MonoBehaviour
{
    [SerializeField] HarvestorController _harvestorController;
    [SerializeField] EnemySpawnerManager _enemySpawnerManager;
    [SerializeField] DamageableObject _playerDamagebleObject;

    [SerializeField] LayerMask _harvestableLayerMask;
    [SerializeField] LayerMask _enemyLayerMask;
    [SerializeField] LayerMask _trapLayerMask;

    protected void OnTriggerEnter(Collider other)
    {
        if (_harvestableLayerMask == (_harvestableLayerMask | (1 << other.gameObject.layer))){
            if (!_harvestorController.IsFull) {
                var otherHarvestableCrop = other.GetComponent<HarvestableCrop>();
                var valueHarvested = otherHarvestableCrop.Harvest();
                _harvestorController.AddHarvest(valueHarvested);
            }
        }
        if (_enemyLayerMask == (_enemyLayerMask | (1 << other.gameObject.layer)))
        {
            var enemy = other.gameObject.GetComponent<Ennemy>();
            if (enemy != null)
            {
                _enemySpawnerManager.KillEnemy(enemy);   
            }
        }
        if (_trapLayerMask == (_trapLayerMask | (1 << other.gameObject.layer)))
        {
            var trap = other.gameObject.GetComponent<Trap>();
            trap.TriggerTrap(_playerDamagebleObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestingController : MonoBehaviour
{
    [SerializeField] HarvestorController _harvestorController;

    [SerializeField] LayerMask _harvestableLayerMask;

    protected void OnTriggerEnter(Collider other)
    {
        if (_harvestableLayerMask == (_harvestableLayerMask | (1 << other.gameObject.layer))){

            var otherHarvestableCrop = other.GetComponent<HarvestableCrop>();
            var valueHarvested = otherHarvestableCrop.Harvest();
            _harvestorController.AddHarvest(valueHarvested);
        }
    }
}

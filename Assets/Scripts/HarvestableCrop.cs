using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestableCrop : MonoBehaviour
{
    [SerializeField] GameObject _grownGO;
    [SerializeField] GameObject _harvestedGO;
    [SerializeField] Collider _collider;

    [Space]
    [SerializeField] int _amountHarvested;

    public int Harvest()
    {
        _grownGO.SetActive(false);
        _harvestedGO.SetActive(true);
        _collider.enabled = false;
        return _amountHarvested;
    }
}

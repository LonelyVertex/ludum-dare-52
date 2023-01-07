using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestableCrop : MonoBehaviour
{
    [SerializeField] GameObject _grownGO;
    [SerializeField] GameObject _harvestedGO;

    [Space]
    [SerializeField] int _amountHarvested;

    public int Harvest()
    {
        _harvestedGO.SetActive(true);
        _grownGO.SetActive(false);
        return _amountHarvested;
    }
}

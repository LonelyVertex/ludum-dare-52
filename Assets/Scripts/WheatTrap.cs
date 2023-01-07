using System;
using UnityEngine;

[RequireComponent(typeof(HarvestableCrop))]
public class WheatTrap : Trap
{
    private HarvestableCrop _harvestableCrop;
    private void Start()
    {
        _harvestableCrop = GetComponent<HarvestableCrop>();
    }

    protected override void OnAfterTrapTrigger()
    {
        _harvestableCrop.Harvest();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestorsManager : Singleton<HarvestorsManager>
{
    public event System.Action<HarvestorController, int> harvestorValueChanged;

    protected void Start()
    {
        var harvestors = FindObjectsOfType<HarvestorController>();
        foreach(var harvestor in harvestors)
        {
            harvestor.harvestorValueChanged += HandleHarvestorValueChanged;
        }
    }

    private void HandleHarvestorValueChanged(HarvestorController harvestor, int newValue)
    {
        harvestorValueChanged?.Invoke(harvestor, newValue);
    }
}

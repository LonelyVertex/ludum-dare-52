using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestorsManager : Singleton<HarvestorsManager>
{
    public event System.Action<HarvestorController, int> harvestorValueChanged;
    public event System.Action<DamageableObject, int> harvestorHpChanged;
    public event System.Action<int> siloValueChanged;
    public event System.Action<bool> siloRangeChanged;

    int siloValue = 0;

    public int SiloValue => siloValue;
    
    protected void Start()
    {
        var harvestors = FindObjectsOfType<HarvestorController>();
        foreach(var harvestor in harvestors)
        {
            harvestor.harvestorValueChanged += HandleHarvestorValueChanged;
            harvestor.unloadToSilo += HandleUnloadToSilo;
            harvestor.siloRangeChanged += HandleSiloRangedChanged;

            var damageableObject = harvestor.GetComponent<DamageableObject>();
            damageableObject.OnDamaged += newValue => HandleHarvestorHpChanged(damageableObject, newValue);
        }
    }

    private void HandleHarvestorValueChanged(HarvestorController harvestor, int newValue)
    {
        harvestorValueChanged?.Invoke(harvestor, newValue);
    }

    void HandleHarvestorHpChanged(DamageableObject harvestor, int newValue)
    {
        harvestorHpChanged?.Invoke(harvestor, newValue);
    }

    void HandleUnloadToSilo(int value)
    {
        siloValue += value;
        siloValueChanged?.Invoke(siloValue);
    }

    void HandleSiloRangedChanged(bool value)
    {
        siloRangeChanged?.Invoke(value);
    }
}

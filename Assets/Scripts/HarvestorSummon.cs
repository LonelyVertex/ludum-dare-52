using UnityEngine;

public class HarvestorSummon : MonoBehaviour
{
    [SerializeField] GameObject _summonPrefab;
    [SerializeField] HarvestorController _harvestorController;

    private bool _summoned = false;
    private const int kValueToSummon = 2500;

    protected void Start()
    {
        _harvestorController.harvestorValueChanged += HandleHarvestorValueChanged;
    }

    private void HandleHarvestorValueChanged(HarvestorController harvestor, int newValue)
    {
        if(newValue > kValueToSummon && !_summoned)
        {
            Summon();
        }
    }

    private void Summon()
    {
        _summoned = true;
        Instantiate(_summonPrefab, transform);
    }
}

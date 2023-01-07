using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _harvestorValueText;

    protected void Start()
    {
        HarvestorsManager.instance.harvestorValueChanged += HandleHarvestorValueChanged;
    }

    private void HandleHarvestorValueChanged(HarvestorController harvestor, int newValue)
    {
        _harvestorValueText.text = newValue.ToString();
    }
}

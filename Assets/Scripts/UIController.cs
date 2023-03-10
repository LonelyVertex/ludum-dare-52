using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _harvestorValueText;
    [SerializeField] Slider _cropsSlider;
    [SerializeField] Image _cropsSliderImage;
    [SerializeField] Slider _hpSlider;
    [SerializeField] Image _hpSliderImage;
    [SerializeField] TextMeshProUGUI _startCountdown;
    [SerializeField] TextMeshProUGUI _time;
    [SerializeField] GameObject _gameOverScreen;
    [SerializeField] GameObject _victoryScreen;
    [SerializeField] TextMeshProUGUI _victoryPoints;
    [SerializeField] TextMeshProUGUI _previousVictoryPoints;
    [SerializeField] FlowManager _flowManager;

    [Header("Hints")] [SerializeField] GameObject unloadHint;
    [SerializeField] GameObject repairHint;
    [SerializeField] GameObject fullHint;

    bool _harvestorFull;
    bool _harvestorDamaged;
    bool _harvestorNotEmpty;
    bool _inSiloRange;
    bool _inRepairStationRange;
    
    float _firstTimeValue = -1.0f;

    public void SetStartCountdown(string value)
    {
        _startCountdown.text = value;
    }
    
    public void HideStartCountdown()
    {
        _startCountdown.gameObject.SetActive(false);
    }
    
    public void SetTime(float value)
    {
        if (_firstTimeValue == -1.0f)
        {
            _firstTimeValue = value;
        }
        
        value = Mathf.Round(value);
        var mins = Mathf.FloorToInt(value / 60);
        var secs = value % 60;
        var fill = secs < 10 ? "0" : "";
        _time.text = $"{mins}:{fill}{secs}";

        _time.color = Color.Lerp(Color.red, Color.white, value / _firstTimeValue);
    }

    public void ShowGameOverScreen()
    {
        _gameOverScreen.SetActive(true);
    }

    public void ShowVictoryScreen(int points, int prevPoints)
    {
        _previousVictoryPoints.gameObject.SetActive(prevPoints != -1);
        _previousVictoryPoints.text = $"Previous best: {prevPoints}";
        _victoryPoints.text = $"{points}";
        _victoryScreen.SetActive(true);
    }

    public void Restart()
    {
        _flowManager.Restart();
    }

    public void ToMenu()
    {
        _flowManager.ToMenu();
    }

    protected void Start()
    {
        HarvestorsManager.instance.harvestorValueChanged += HandleHarvestorValueChanged;
        HarvestorsManager.instance.harvestorHpChanged += HandleHarvestorHpChanged;
        HarvestorsManager.instance.siloValueChanged += HandleSiloValueChanged;
        HarvestorsManager.instance.siloRangeChanged += HandleSiloRangeChanged;
        HarvestorsManager.instance.repairStationRangeChanged += HandleRepairStationRangeChanged;

        _harvestorValueText.text = "0";
        _cropsSlider.value = 0;
        _hpSlider.value = 1;
    }

    private void HandleHarvestorValueChanged(HarvestorController harvestor, int newValue)
    {
        _cropsSlider.value = (float)newValue / harvestor.MaxGrain;
        _harvestorFull = harvestor.MaxGrain == newValue;
        _harvestorNotEmpty = newValue > 0;
        _cropsSliderImage.color = Color.Lerp(Color.white, Color.red, (float) newValue / harvestor.MaxGrain);
        UpdateHints();
    }

    private void HandleHarvestorHpChanged(DamageableObject harvestor, int newValue)
    {
        _harvestorDamaged = harvestor.DamageValue > 0;
        _hpSlider.value = (float)newValue / harvestor.MaxHp;
        _hpSliderImage.color = Color.Lerp(Color.red, Color.white, (float) newValue / harvestor.MaxHp);
    }

    private void HandleSiloValueChanged(int newValue)
    {
        _harvestorValueText.text = newValue.ToString();
    }

    private void HandleSiloRangeChanged(bool newValue)
    {
        _inSiloRange = newValue;
        UpdateHints();
    }

    private void HandleRepairStationRangeChanged(bool newValue)
    {
        _inRepairStationRange = newValue;
        UpdateHints();
    }

    void UpdateHints()
    {
        unloadHint.SetActive(false);
        repairHint.SetActive(false);
        fullHint.SetActive(false);

        if (_harvestorFull && !_inSiloRange)
        {
            fullHint.SetActive(true);
        }
        else if (_inSiloRange && _harvestorNotEmpty)
        {
            unloadHint.SetActive(true);
        }
        else if (_inRepairStationRange && _harvestorDamaged)
        {
            repairHint.SetActive(true);
        }
    }
}
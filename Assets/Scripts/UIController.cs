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
    [SerializeField] Slider _hpSlider;
    [SerializeField] TextMeshProUGUI _startCountdown;
    [SerializeField] TextMeshProUGUI _time;
    [SerializeField] GameObject _gameOverScreen;
    [SerializeField] GameObject _victoryScreen;
    [SerializeField] TextMeshProUGUI _victoryPoints;
    
    [Header("Hints")] [SerializeField] GameObject unloadHint;
    [SerializeField] GameObject fullHint;

    bool _harvestorFull;
    bool _harvestorNotEmpty;
    bool _inSiloRange;

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
        value = Mathf.Round(value);
        var mins = Mathf.FloorToInt(value / 60);
        var secs = value % 60;
        var fill = secs < 10 ? "0" : "";
        _time.text = $"{mins}:{fill}{secs}";
    }

    public void ShowGameOverScreen()
    {
        _gameOverScreen.SetActive(true);
    }

    public void ShowVictoryScreen(int points)
    {
        _victoryPoints.text = $"{points}";
        _victoryScreen.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    
    protected void Start()
    {
        HarvestorsManager.instance.harvestorValueChanged += HandleHarvestorValueChanged;
        HarvestorsManager.instance.harvestorHpChanged += HandleHarvestorHpChanged;
        HarvestorsManager.instance.siloValueChanged += HandleSiloValueChanged;
        HarvestorsManager.instance.siloRangeChanged += HandleSiloRangeChanged;

        _harvestorValueText.text = "0";
        _cropsSlider.value = 0;
        _hpSlider.value = 1;
    }

    private void HandleHarvestorValueChanged(HarvestorController harvestor, int newValue)
    {
        _cropsSlider.value = (float)newValue / harvestor.MaxGrain;
        _harvestorFull = harvestor.MaxGrain == newValue;
        _harvestorNotEmpty = newValue > 0;
        UpdateHints();
    }

    private void HandleHarvestorHpChanged(DamageableObject harvestor, int newValue)
    {
        _hpSlider.value = (float)newValue / harvestor.MaxHp;
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

    void UpdateHints()
    {
        unloadHint.SetActive(false);
        fullHint.SetActive(false);

        if (_harvestorFull && !_inSiloRange)
        {
            fullHint.SetActive(true);
        }
        else if (_inSiloRange && _harvestorNotEmpty)
        {
            unloadHint.SetActive(true);
        }
    }
}
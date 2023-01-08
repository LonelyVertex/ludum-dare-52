using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FlowManager : MonoBehaviour
{
    public FadeInController _fadeInController;
    public HarvestorController _harvestorController;
    public HarvestorsManager _harvestorsManager;
    public UIController _uiController;
    public DamageableObject _playerDamageable;

    [Space] 
    public int timeLimit;

    [Space] 
    public AudioSource audioSource;
    public AudioClip startNumberSound;
    public AudioClip startGoSound;
    public AudioClip victorySound;
    public AudioClip gameOverSound;

    float _startTime;
    float _extraTime;
    bool _gameRunning;
    
    void Start()
    {
        _harvestorController.enabled = false;
        _uiController.SetTime(timeLimit);
        
        _playerDamageable.OnDamaged += (health) =>
        {
            if (health <= 0)
                GameOver();
        };

        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        yield return _fadeInController.FadeIn();
        _uiController.SetStartCountdown("3");
        PlaySound(startNumberSound);
        yield return new WaitForSeconds(1);
        _uiController.SetStartCountdown("2");
        PlaySound(startNumberSound);
        yield return new WaitForSeconds(1);
        _uiController.SetStartCountdown("1");
        PlaySound(startNumberSound);
        yield return new WaitForSeconds(1);
        _uiController.SetStartCountdown("GO!");
        PlaySound(startGoSound);
        _harvestorController.enabled = true;
        _startTime = Time.time;
        _gameRunning = true;
        yield return new WaitForSeconds(1);
        _uiController.HideStartCountdown();
    }

    public void Restart()
    {
        StartCoroutine(RestartCoroutine());
    }

    private IEnumerator RestartCoroutine()
    {
        yield return _fadeInController.FadeOut();
        
        SceneManager.LoadScene("HarvestScene");
    }

    private void GameOver()
    {
        EndGame();
        PlaySound(gameOverSound);
        _uiController.ShowGameOverScreen();
    }

    void Victory()
    {
        EndGame();
        PlaySound(victorySound);
        _uiController.ShowVictoryScreen(_harvestorsManager.SiloValue);
        
        PlayerPrefs.SetInt("score", _harvestorsManager.SiloValue);
        PlayerPrefs.Save();
    }

    void EndGame()
    {
        _gameRunning = false;
        _harvestorController.enabled = false;
        var ennemies = FindObjectsOfType<Ennemy>();
        foreach (var ennemy in ennemies)
        {
            ennemy.PlayerDied();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gameRunning) return;
        
        var elapsed = Time.time - _startTime - _extraTime;
        _uiController.SetTime(timeLimit - elapsed);

        if (elapsed >= timeLimit)
        {
            Victory();
        }
    }

    public void AddTime(int seconds)
    {
        _extraTime += seconds;
    } 

    void PlaySound(AudioClip sound)
    {
        audioSource.clip = sound;
        audioSource.Play();
    }
}
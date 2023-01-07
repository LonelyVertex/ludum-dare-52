using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FlowManager : MonoBehaviour
{
    public HarvestorController _harvestorController;
    public DamageableObject _playerDamageable;
    public GameObject _gameOverScreen;
    public Button _ReplayButton;
    void Start()
    {
        _playerDamageable.OnDamaged += (health) =>
        {
            if (health <= 0)
                ShowGameOverScreen();
        };
        _ReplayButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
    }

    private void ShowGameOverScreen()
    {
        _harvestorController.enabled = false;
        _gameOverScreen.SetActive(true);
        var ennemies = FindObjectsOfType<Ennemy>();
        foreach(var ennemy in ennemies)
        {
            ennemy.PlayerDied();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

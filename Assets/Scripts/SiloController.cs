using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiloController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _addHarvestParticleSystem;
    [SerializeField] private float _speed;
    
    private float _harvestTime;
    
    protected void Start()
    {
        HarvestorsManager.instance.siloValueChanged += HandleSiloValueChanged;
    }

    private void OnDestroy()
    {
        HarvestorsManager.instance.siloValueChanged -= HandleSiloValueChanged;
    }

    private void Update()
    {
        if (_harvestTime <= 0.0f && !_addHarvestParticleSystem.isStopped)
        {
            _addHarvestParticleSystem.Stop();
        }

        _harvestTime = Mathf.Clamp(_harvestTime - (Time.deltaTime * _speed), 0.0f, 1.0f);
    }

    private void HandleSiloValueChanged(int value)
    {
        _harvestTime = 1.0f;

        if (!_addHarvestParticleSystem.isPlaying)
        {
            _addHarvestParticleSystem.Play();
        }
    }
}

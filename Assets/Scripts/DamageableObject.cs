using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObject : MonoBehaviour
{
    public int _startHitPoints;
    public List<GameObject> _meshesOfDecreasingHealth;
    private int _hitpoints;
    public Action<int> OnDamaged;
    void Start()
    {
        _hitpoints = _startHitPoints;
        UpdateMesh();
    }

    private void UpdateMesh()
    {
        GameObject activeMesh = null;
        for(int i = _meshesOfDecreasingHealth.Count - 1; i >= 0; i--)
        {
            if(_meshesOfDecreasingHealth[i] != null)
            {
                _meshesOfDecreasingHealth[i].SetActive(false);
                if(i >= _hitpoints)
                {
                    activeMesh = _meshesOfDecreasingHealth[i];
                }
            }
        }
        activeMesh.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage()
    {
        _hitpoints--;
        UpdateMesh();
        OnDamaged?.Invoke(_hitpoints);
    }
}

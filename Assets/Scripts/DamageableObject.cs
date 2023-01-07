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
    public float _hitBumpStr;
    public Rigidbody _rigidbody;

    public int MaxHp => _startHitPoints;
    
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
        if(Input.GetKeyDown(KeyCode.T)){

            var hitPos = transform.position + Vector3.forward;
            _rigidbody.AddForceAtPosition(Vector3.one * _hitBumpStr, hitPos, ForceMode.Impulse);
        }
    }

    public void Damage(Transform hitBy, int dmg = 1)
    {
        _hitpoints -= dmg;
        UpdateMesh();
        _rigidbody.AddForceAtPosition(Vector3.one * _hitBumpStr, hitBy.position, ForceMode.Impulse);
        OnDamaged?.Invoke(_hitpoints);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaisingGrain : MonoBehaviour
{
    public Vector3 _endScale;
    public Vector3 _endPos;
    public float _maxGrain;
    public HarvestorController _harvester;
    
    private Vector3 _startScale;
    private Vector3 _startPos;
    void Start()
    {
        _harvester.harvestorValueChanged += OnGrainValueChanged;
        _startScale = transform.localScale;
        _startPos = transform.localPosition;
    }

    private void OnGrainValueChanged(HarvestorController arg1, int grainVal)
    {
        float t = Mathf.InverseLerp(0, _maxGrain, grainVal);
        transform.localScale = Vector3.Lerp(_startScale, _endScale, t);
        transform.localPosition = Vector3.Lerp(_startPos, _endPos, t);
    }

}

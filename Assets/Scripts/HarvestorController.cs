using System;
using System.Collections.Generic;
using UnityEngine;

public class HarvestorController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] InputModule _inputModule;
    [SerializeField] Rigidbody _rigidbody;

    [SerializeField] float _accelerationMultiplier;
    [SerializeField] float _steeringMultiplier;
    [SerializeField] float _tireDrag;
    [SerializeField] float _maxSpeed;
    [SerializeField] float _tireMarkLimit;

    [SerializeField] List<TrailRenderer> _tireMarkTrailRenderers;

    [Header("Harvesting")] 
    [SerializeField] int _maxGrain;
    [SerializeField] Transform _silo;
    [SerializeField] float _siloUnloadDistance;
    [SerializeField] float _siloUnloadSpeed;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] GameObject _grainPrefab;
    [SerializeField] Transform _grainExhaust;
    [SerializeField] Transform _siloTop;
    
    [Header("Repairing")] 
    [SerializeField] DamageableObject _damageableObject;
    [SerializeField] Transform _repairStation;
    [SerializeField] float _repairStationDistance;
    [SerializeField] GameObject _repairStationParticleSystem;
    [SerializeField] float _repairSpeed;

    public event System.Action<HarvestorController, int> harvestorValueChanged;
    public event System.Action<int> unloadToSilo;
    public event System.Action<bool> siloRangeChanged;
    
    public event System.Action<bool> repairStationRangeChanged;

    public bool IsFull => MaxGrain <= _currentlyStoredHarvestValue;

    private int _currentlyStoredHarvestValue;

    public int MaxGrain => _maxGrain;

    protected void FixedUpdate()
    {
        HandleAcceleration();
        HandleSteering();
        HandleFriction();
        LimitMaxSpeed();
    }

    void Update()
    {
        HandleUnload();
        HandleRepair();
        
        siloRangeChanged?.Invoke(IsInSiloRange());
        repairStationRangeChanged?.Invoke(IsInRepairStationRange());
    }

    public void AddHarvest(int value)
    {
        _currentlyStoredHarvestValue = Mathf.Clamp(_currentlyStoredHarvestValue + value, 0, _maxGrain);
        harvestorValueChanged?.Invoke(this, _currentlyStoredHarvestValue);
    }

    private void HandleAcceleration()
    {
        float acceleration = _accelerationMultiplier * _inputModule.GetVerticalAxis();
        var accelerationVector = acceleration * transform.forward;
        _rigidbody.AddForce(accelerationVector);
    }

    private void HandleSteering()
    {
        //Rotate stuff
        float steeringInput = _inputModule.GetHorizontalAxis();
        if(Mathf.Abs(steeringInput) > 0.03f)
        {
            var currentRotation = transform.rotation;
            var currentAngle = currentRotation.eulerAngles.y;
            currentAngle -= Time.deltaTime * _steeringMultiplier * steeringInput;
            var newRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentAngle, currentRotation.z);
            _rigidbody.MoveRotation(newRotation);
        }
    }

    private void HandleFriction()
    {
        var currentVelocity = _rigidbody.velocity;
        var sideVelocity = transform.right * Vector3.Dot(currentVelocity, transform.right);
        var velocityDiminish = sideVelocity * _tireDrag * Time.deltaTime;
        currentVelocity -= velocityDiminish;
        _rigidbody.velocity = currentVelocity;
        SetTireMarksEnabled(sideVelocity.magnitude > _tireMarkLimit);
    }

    private void SetTireMarksEnabled(bool enabled)
    {
        foreach(var tireMark in _tireMarkTrailRenderers)
        {
            tireMark.emitting = enabled;
        }
    }

    private void LimitMaxSpeed()
    {
        var currentVelocity = _rigidbody.velocity;
        if(currentVelocity.magnitude > _maxSpeed)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * _maxSpeed;
        }
    }

    private void HandleUnload()
    {
        if (_inputModule.IsActionButtonDown() && IsInSiloRange() && _currentlyStoredHarvestValue > 0)
        {
            var value = Mathf.Min(_currentlyStoredHarvestValue, Mathf.CeilToInt(_siloUnloadSpeed * Time.deltaTime));
            AddHarvest(-value);
            unloadToSilo?.Invoke(value);

            SpawnUnloadingGrain();
            
            if (!_audioSource.isPlaying) _audioSource.Play();
        }
        else
        {
            _audioSource.Stop();
        }
    }

    private void HandleRepair()
    {
        if (_inputModule.IsActionButtonDown() && IsInRepairStationRange() && _damageableObject.DamageValue > 0)
        {
            _repairStationParticleSystem.SetActive(true);
            var value = _repairSpeed * Time.deltaTime;
            _damageableObject.Repair(value);
        }
        else
        {
            _repairStationParticleSystem.SetActive(false);
            _damageableObject.RestartRepairs();
            _audioSource.Stop();
        }
    }

    void SpawnUnloadingGrain()
    {
        var go = Instantiate(_grainPrefab, _grainExhaust.position, Quaternion.identity);
        go.GetComponent<Grain>().SetUp(_grainExhaust.forward, _siloTop.position);
    }

    bool IsInSiloRange()
    {
        return Vector3.Distance(transform.position, _silo.position) < _siloUnloadDistance;
    }

    bool IsInRepairStationRange()
    {
        return Vector3.Distance(transform.position, _repairStation.position) < _repairStationDistance;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class HarvestorController : MonoBehaviour
{
    
    [SerializeField] InputModule _inputModule;
    [SerializeField] Rigidbody _rigidbody;

    [SerializeField] float _accelerationMultiplier;
    [SerializeField] float _steeringMultiplier;
    [SerializeField] float _tireDrag;
    [SerializeField] float _maxSpeed;
    [SerializeField] float _tireMarkLimit;

    [SerializeField] List<TrailRenderer> _tireMarkTrailRenderers;

    public event System.Action<HarvestorController, int> harvestorValueChanged;

    private int _currentlyStoredHarvestValue;

    protected void FixedUpdate()
    {
        HandleAcceleration();
        HandleSteering();
        HandleFriction();
        LimitMaxSpeed();
    }

    public void AddHarvest(int value)
    {
        _currentlyStoredHarvestValue += value;
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

    private void HandleUnloader()
    {
        if (_inputModule.IsActionButtonDown())
        {
            DeployUnloader();
        }
    }

    private void DeployUnloader()
    {

    }
}

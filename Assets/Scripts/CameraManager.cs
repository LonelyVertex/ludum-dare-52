using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraManager : MonoBehaviour
{
    public Transform _toFollow;
    public Vector3 _followDistance;

    [Space]
    public float _shakeDuration;
    public float _shakeMagnitude;
    public float _shakeMaxMoveDelta;

    private Vector3 _targetShake;
    private Vector3 _shake;
    
    private void Start()
    {
        Follow();
    }

    void Update()
    {
        Follow();
    }

    public void Follow()
    {
        var idealPos = _toFollow.transform.position + _followDistance + Vector3.MoveTowards(_shake, _targetShake, _shakeMaxMoveDelta);
        transform.position = idealPos;
    }

    public void Shake()
    {
        StartCoroutine(ShakeEnumerator());
    }
    
    public IEnumerator ShakeEnumerator()
    {
        for (var elapsed = 0.0f; elapsed <= _shakeDuration; elapsed += Time.deltaTime)
        {
            float x = Random.Range(-1f, 1f) * _shakeMagnitude;
            float y = Random.Range(-1f, 1f) * _shakeMagnitude;

            _targetShake = new Vector3(x, y, 0f);

            yield return null;
        }

        _targetShake = Vector3.zero;
    }
}

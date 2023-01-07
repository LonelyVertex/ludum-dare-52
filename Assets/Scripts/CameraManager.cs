using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform _toFollow;
    public float _followDistance;
    void Start()
    {
    }

    void Update()
    {
        follow();
    }

    public void follow()
    {
        var idealPos = _toFollow.transform.position + new Vector3(_followDistance, _followDistance, 0);
        transform.position = idealPos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
    public Vector3 _speed;
    void Update()
    {
        transform.Rotate(_speed, Space.Self);
    }
}

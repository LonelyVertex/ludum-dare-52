using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform _toFollow;
    public float _movePos;


    void Update()
    {
        follow();
    }

    public void follow()
    {
        var step = _movePos * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _toFollow.position, step);
    }
}

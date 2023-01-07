using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{
    public Transform _player;
    public float _detectionRange;
    public float _moveSpeed;
    [Header("Attack Settings")]
    public float _attackRange;
    public float _attackInterval = 0.5f;
    [Header("Bullet Settings")]
    public bool _useBullets;
    public GameObject _bullet;
    public float _bulletSpeed;
    public Transform _bulletOrigin;
    public float _timeToDestroyBullets = 4f;
    [Header("Audio Settings")] 
    public AudioSource audioSource;
    public AudioClip shootSound;
    public AudioClip meleeSound;

    [Header("Kopr does not know what he is doing")]
    [SerializeField]
    private FlowManager flowManager;
    
    public enum AIState { Idle, ChaseAndAttack, GoBack }
    private Vector3 _startPos;
    private float _lastAttackTime;    
    private AIState _currentState;
    private void Start()
    {
        _startPos = transform.position;
    }

    void Update()
    {
        Act(_currentState);
    }

    private void Act(AIState currentState)
    {
        switch (currentState)
        {
            case AIState.Idle:
                if(_player == null)
                {
                    return;
                }
                if (IsInDetectionRange(_player.position))
                    _currentState = AIState.ChaseAndAttack;
                break;
            case AIState.ChaseAndAttack:
                if(_player != null && IsInAttackRange(_player.position)){
                    LookAt(_player.position);
                    Attack();
                }
                else if (_player != null && IsInDetectionRange(_player.position))
                {
                    MoveTo(_player.position);
                }
                else
                {
                    _currentState = AIState.GoBack;
                }
                break;
            case AIState.GoBack:
                if(_player != null && IsInDetectionRange(_player.position))
                {
                    _currentState = AIState.ChaseAndAttack;
                }
                MoveTo(_startPos);
                break;
        }
    }

    internal void PlayerDied()
    {
        _currentState = AIState.GoBack;
        _player = null;
    }

    void Attack()
    {
        if((Time.time - _lastAttackTime) >= _attackInterval)
        {
            _lastAttackTime = Time.time;
            if (_useBullets)
            {
                var bullet = Instantiate(_bullet, _bulletOrigin);
                bullet.GetComponent<Rigidbody>().AddForce(-bullet.transform.forward * _bulletSpeed, ForceMode.Impulse);
                bullet.transform.parent = null;
                StartCoroutine(WaitAndDestroyBullet(bullet));

                if (shootSound)
                {
                    audioSource.clip = shootSound;
                    audioSource.Play();
                }
            }
            else
            {
                _player.GetComponent<DamageableObject>().Damage(transform);

                if (meleeSound)
                {
                    audioSource.clip = meleeSound;
                    audioSource.Play();
                }
            }
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
        flowManager.AddTime(5);
    }

    IEnumerator WaitAndDestroyBullet(GameObject bullet)
    {
        yield return new WaitForSeconds(_timeToDestroyBullets);
        Destroy(bullet);
    }

    private bool IsInDetectionRange(Vector3 pos)
    {
        var playerDist = Vector3.Distance(pos, transform.position);
        return playerDist <= _detectionRange;
    }

    private bool IsInAttackRange(Vector3 pos)
    {
        var playerDist = Vector3.Distance(pos, transform.position);
        return playerDist <= _attackRange;
    }

    void MoveTo(Vector3 pos)
    {
        transform.position = Vector3.MoveTowards(transform.position, pos, _moveSpeed * Time.deltaTime);
        LookAt(pos);
    }

    void LookAt(Vector3 pos)
    {
        Vector3 targetDirection = pos - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, _moveSpeed * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}

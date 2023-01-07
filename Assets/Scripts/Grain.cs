using UnityEngine;

public class Grain : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float convergence;
    
    Vector3 _targetPosition;
    Vector3 _currentForce;

    bool _setUp;

    public void SetUp(Vector3 force, Vector3 targetPosition)
    {
        _currentForce = force.normalized;
        _targetPosition = targetPosition;
        _setUp = true;
    }
    
    
    void Update()
    {
        if (!_setUp) return;
        
        transform.Translate(_currentForce * (speed * Time.deltaTime));
        _currentForce = Vector3.Lerp(_currentForce, (_targetPosition - transform.position).normalized,
            convergence * Time.deltaTime);

        if (Vector3.Distance(transform.position, _targetPosition) < .1f)
        {
            Destroy(gameObject);
        }
    }
}

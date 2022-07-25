using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalistaTarget : MonoBehaviour
{
    [SerializeField] GameObject _balistaHead;
    [SerializeField] float _fireRange = 15f;
    [SerializeField] ParticleSystem _bolts;
    Transform _target;


    void Update() 
    {
        FindClosestTarget();

        float _currentDistance = Vector3.Distance(transform.position, _target.position);
        bool _isInRange = _currentDistance < _fireRange;

        if(_target != null)
        {
            _balistaHead.transform.LookAt(_target);
            Attack(_isInRange);
        }
    }

    void Attack(bool _isInRange)
    {
        var  emissionModule = _bolts.emission;
        emissionModule.enabled = _isInRange;
    }

    void FindClosestTarget()
    {
        EnemyMovement[] _enemies = FindObjectsOfType<EnemyMovement>();
        float _maxDistance = Mathf.Infinity;
        Transform _closestTarget = null;

        foreach(EnemyMovement _enemy in _enemies)
        {
            float _currentDistance = Vector3.Distance(transform.position, _enemy.transform.position);

            if(_currentDistance < _maxDistance)
            {
                _closestTarget = _enemy.transform;
                _maxDistance = _currentDistance;
            }
        }

        _target = _closestTarget;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int _maxHealthPoints = 10;
    int _currentHealthPoints;
    [SerializeField] int _damage = 1;
    [SerializeField] bool isEnemy = true;

    void OnEnable() 
    {
        _currentHealthPoints = _maxHealthPoints;
    }

    void Update()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        _currentHealthPoints -= _damage;
        if (_currentHealthPoints <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class Health : MonoBehaviour
{
    [SerializeField] int _maxHealthPoints = 10;
    [SerializeField] int _damage = 1;
    int _currentHealthPoints;
    Enemy _enemy;

    void OnEnable() 
    {
        _currentHealthPoints = _maxHealthPoints;
        _enemy = GetComponent<Enemy>();
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
            _enemy.RewardGold();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] [Range(1f, 50f)] int _poolSize = 5;
    [SerializeField] [Range(0.1f, 30f)] float _spawningRate = 1;
    GameObject[] _pool;

    void Start()
    {
        FillingUpPool();
        StartCoroutine(SpawnEnemies());
    }

    void FillingUpPool()
    {
        _pool = new GameObject[_poolSize];

        for(int i = 0; i < _pool.Length; i++)
        {
            _pool[i] = Instantiate(_enemyPrefab, transform);
            _pool[i].SetActive(false);
        }
    }
    void EnableObjectsInPool()
    {
        foreach( GameObject _clone in _pool)
            {
                if(_clone.activeInHierarchy == false)
                {
                    _clone.SetActive(true);
                    return;
                }
                
            }
    }

    IEnumerator  SpawnEnemies()
    {

        while(true)
        {
            EnableObjectsInPool();
            yield return new WaitForSeconds(_spawningRate);

        }
    }
    
}

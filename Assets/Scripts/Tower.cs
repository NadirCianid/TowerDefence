using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int _towerCost = 50;
    [SerializeField] int _buildDelay = 3;

    void Start() 
    {
        StartCoroutine(Build());
    }
    public bool PlaceTower(Tower _tower, Transform _transform)
    {
        Bank _bank = FindObjectOfType<Bank>();
        
        if(_bank == null) return false;

        if(_bank.CurrentBalance >= _towerCost)
        {
            Instantiate(_tower, _transform);
            _bank.Withdraw(_towerCost);
            return true;
        }
        
        return false;
    }

    IEnumerator Build()
    {
        foreach(Transform _child in transform)
        {
            _child.gameObject.SetActive(false);
            foreach(Transform _grandChild in transform)
            {
                _grandChild.gameObject.SetActive(false);
            }
        }

        foreach(Transform _child in transform)
        {
            

            _child.gameObject.SetActive(true);
            yield return new WaitForSeconds(_buildDelay);
            foreach(Transform _grandChild in transform)
            {
                _grandChild.gameObject.SetActive(true);
                
            }
        }
        
    }
}

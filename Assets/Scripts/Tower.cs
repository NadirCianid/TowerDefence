using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int _towerCost = 50;
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
}

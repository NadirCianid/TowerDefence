using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    
    [SerializeField] bool _isPlaceable;
    public bool IsPlaceable { get { return _isPlaceable; } }
    [SerializeField] GameObject _balistaPrefab;
    
    void OnMouseDown()
    {
        if(_isPlaceable)
        {
            Instantiate(_balistaPrefab, transform.position, Quaternion.identity);    
            _isPlaceable = false;
        }
    }
}

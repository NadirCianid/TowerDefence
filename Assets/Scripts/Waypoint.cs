using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    
    [SerializeField] bool _isPlaceable;
    public bool IsPlaceable { get { return _isPlaceable; } }
    [SerializeField] Tower _tower;
    
    void OnMouseDown()
    {
        if(_isPlaceable)   _isPlaceable = !_tower.PlaceTower(_tower, transform);
        
    }
}

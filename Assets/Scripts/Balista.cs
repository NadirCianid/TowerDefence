using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balista : MonoBehaviour
{
    [SerializeField] GameObject _balistaHead;
    PathFinder _target;



    void Update() 
    {
        
        _target = FindObjectOfType<PathFinder>();
        if(_target != null)
        {
            _balistaHead.transform.LookAt(_target.transform);
        }
        
    }
}

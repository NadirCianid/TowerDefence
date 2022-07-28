using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
[RequireComponent(typeof(Waypoint))]
public class TilingSystem : MonoBehaviour
{
    [SerializeField] TextMeshPro _coordinates;
    [SerializeField] Color _defaultColor = Color.white;
    [SerializeField] Color _blockedColor = Color.red;
    Waypoint waipoint;
    
    float x,y;
    string _tileName;

    void Awake()
    {
        waipoint = GetComponent<Waypoint>();
        UpdateCoordinates();
        
        
    }

        void Start()
    {
        _coordinates.enabled = false;
        if(!Application.isPlaying)
        {
            _coordinates.enabled = true;
        }
    }

    void Update()
    {
        if(!Application.isPlaying)
        {
            UpdateCoordinates();
        }

        SetLabelColor();
        ToggleLabels();
        
    }

    void SetLabelColor()
    {
        if(waipoint.IsPlaceable)
        {
            _coordinates.color = _defaultColor;
        }
        else
        {
            _coordinates.color = _blockedColor;
        }
    }
    
    void ToggleLabels()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            _coordinates.enabled = !_coordinates.IsActive();
        }
    }

    void UpdateCoordinates()
    {
        x = transform.position.x / 16;
        y = transform.position.z / 16;
        _coordinates.text = "[" + x.ToString() + "," + y.ToString() + "]";
        transform.name = _coordinates.text;
    }
}

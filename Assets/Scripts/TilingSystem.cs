using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
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
    void Update()
    {
        if(!Application.isPlaying)
        {
            UpdateCoordinates();
        }

        UpdateCoordinatesColor();
        
    }

    void UpdateCoordinatesColor()
    {
        if(waipoint.IsPlaceable)
        {
            _coordinates.color = _defaultColor;
        }
        else
        {
            _coordinates.color = _blockedColor;
        }
        ToggleLabels();
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
        x = transform.position.x / UnityEditor.EditorSnapSettings.move.x;
        y = transform.position.z / UnityEditor.EditorSnapSettings.move.z;
        _coordinates.text = "[" + x.ToString() + "," + y.ToString() + "]";
        transform.name = _coordinates.text;
    }
}

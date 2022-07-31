using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
[RequireComponent(typeof(Tile))]
public class TilingSystem : MonoBehaviour
{
    [SerializeField] TextMeshPro _coordinates;
    [SerializeField] Color _defaultColor = Color.white;
    [SerializeField] Color _blockedColor = Color.grey;
    [SerializeField] Color _exploredColor = Color.yellow;
    [SerializeField] Color _pathColor = Color.green;
    GridManager _gridManager;
    
    int x,y;
    string _tileName;

    void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();
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
        Vector2Int _nodeCoordinates = new Vector2Int(x,y);
        Node _node = _gridManager.GetNode(_nodeCoordinates);
        
        if(_node!=null)
        {
            if(!_node.isWalkable)
            {
                _coordinates.color = _blockedColor;
            }
            else if(_node.isPath)
                {
                    _coordinates.color = _pathColor;
                }
                else if(_node.isExplored)
                    {
                        _coordinates.color = _exploredColor;
                    }
                    else _coordinates.color = _defaultColor;
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
        x = (int)transform.position.x / _gridManager.UnityGridSize;
        y = (int)transform.position.z / _gridManager.UnityGridSize;
        _coordinates.text = "[" + x.ToString() + "," + y.ToString() + "]";
        transform.name = _coordinates.text;
    }
}

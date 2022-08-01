using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    
    [SerializeField] bool _isPlaceable;
    public bool IsPlaceable { get { return _isPlaceable; } }
    [SerializeField] Tower _tower;
    GridManager _gridManager;
    Pathfinder _pathfinder;
    Vector2Int _coordinates = new Vector2Int();

    void Awake() 
    {
        _gridManager = FindObjectOfType<GridManager>();
        _pathfinder = FindObjectOfType<Pathfinder>();
    }

    void Start() 
    {
        if(_gridManager!=null)
        {
            _coordinates = _gridManager.GetCoordinatesFromPosition(transform.position);
            if(!_isPlaceable)
            {
                _gridManager.Grid[_coordinates].isWalkable = false;
            }
        }   
    }
    
    void OnMouseDown()
    {
        if(_gridManager.GetNode(_coordinates).isWalkable && !_pathfinder.WillBlockPath(_coordinates))
        {
            bool _isSuccessfulPlaced = _tower.PlaceTower(_tower, transform);
            if(_isSuccessfulPlaced)
            {
                _gridManager.BlockNode(_coordinates);
                _pathfinder.NotifyRecievers();
            }
        }  
        
    }
}

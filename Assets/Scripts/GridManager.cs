using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class GridManager : MonoBehaviour
{
    Dictionary<Vector2Int, Node> _grid = new Dictionary<Vector2Int, Node>();
    [SerializeField] Vector2Int _gridSize = new Vector2Int();
    [SerializeField] int _unityGridSize = 16;
    public int UnityGridSize{ get{ return _unityGridSize;}}

    public Dictionary<Vector2Int, Node> Grid { get{ return _grid; } }

    public Node GetNode(Vector2Int _coordinates)
    {
        if(_grid.ContainsKey(_coordinates)) return _grid[_coordinates];
        else return null;
    }

    void Awake()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        for (int x = 0; x < _gridSize.x; x++)
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                Vector2Int _coordinates = new Vector2Int(x, y);
                Node _node = new Node(_coordinates, true);
                _grid.Add(_coordinates, _node);
            }
        }
    }   
    public void BlockNode(Vector2Int _coordinates)
    {
        if(_grid.ContainsKey(_coordinates))
        {
            _grid[_coordinates].isWalkable = false;
        }
    }

    public void ResetNodes()
    {
        foreach(KeyValuePair<Vector2Int, Node> _entry in _grid)
        {
            _entry.Value.isExplored = false;
            _entry.Value.isPath = false;
            _entry.Value.conectedTo = null;

        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 _position)
    {
        Vector2Int _coordinates = new Vector2Int();

        _coordinates.x = (int)_position.x / _unityGridSize;
        _coordinates.y = (int)_position.z / _unityGridSize;

        return _coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int _coordinates)
    {
        Vector3 _position = new Vector3();

        _position.x = _coordinates.x * _unityGridSize;
        _position.z = _coordinates.y * _unityGridSize;

        return _position;
    }
}

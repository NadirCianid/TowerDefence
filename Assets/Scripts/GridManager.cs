using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class GridManager : MonoBehaviour
{
    Dictionary<Vector2Int, Node> _grid = new Dictionary<Vector2Int, Node>();
    [SerializeField] Vector2Int _gridSize = new Vector2Int();

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
}

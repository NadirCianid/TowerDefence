using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Vector2Int _startNodeCoordinates;
    [SerializeField] Vector2Int _destinationNodeCoordinates;

    Node _currentSearchNode;
    Node _startNode;
    Node _destinationNode;

    
    GridManager _gridManager;
    
    Vector2Int[] _directions = {Vector2Int.down, Vector2Int.up, Vector2Int.right, Vector2Int.left };
    List<Node> _neighbors = new List<Node>();
    Queue<Node> _frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> _reached = new Dictionary<Vector2Int, Node>();

    void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();
    }
    void Start()
    {
        InitializeEdges();
        BreadthFirstSearch();
        BuildPath();
    }


    void InitializeEdges()
    {
        _startNode = _gridManager.Grid[_startNodeCoordinates];
        _destinationNode = _gridManager.Grid[_destinationNodeCoordinates];
    }

    void ExploreNeighbors()
    {
        foreach(Vector2Int _direction in _directions)
        {
            Vector2Int _neighborCoordinates = _currentSearchNode.coordinates + _direction;

            if(_gridManager.Grid.ContainsKey(_neighborCoordinates) && !_gridManager.Grid[_neighborCoordinates].isExplored)
            {
                _neighbors.Add(_gridManager.Grid[_neighborCoordinates]);
            }

            foreach(Node _neighbor in _neighbors)
            {
                if(!_reached.ContainsKey(_neighbor.coordinates))
                {
                    _neighbor.conectedTo = _currentSearchNode;

                    _reached.Add(_neighbor.coordinates, _neighbor);
                    _frontier.Enqueue(_neighbor);
                }
            }
        }
    }

    void BreadthFirstSearch()
    {
        bool _isRuning = true;
        _frontier.Enqueue(_startNode);
        _reached.Add(_startNodeCoordinates, _startNode);

        while(_frontier.Count > 0 && _isRuning)
        {
            _currentSearchNode = _frontier.Dequeue();
            _currentSearchNode.isExplored = true;
            ExploreNeighbors();

            if(_currentSearchNode.coordinates == _destinationNodeCoordinates)
            {
                _isRuning = false;
            }
        }
    }

    List<Node> BuildPath()
    {
        Node _currentNode = _currentSearchNode;
        List<Node> _path = new List<Node>();

        _path.Add(_currentNode);
        _currentNode.isPath = true;
        

        while(_currentNode.conectedTo != null)
        {
            _currentNode = _currentNode.conectedTo;
            _path.Add(_currentNode);
            _currentNode.isPath = true;
        }
        
        _path.Reverse();

        return _path;
    }
}

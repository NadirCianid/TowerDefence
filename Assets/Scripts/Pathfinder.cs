using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Vector2Int _startCoordinates;
    public Vector2Int StartCoordinates { get{ return _startCoordinates;}}
    [SerializeField] Vector2Int _destinationNodeCoordinates;

    Node _currentSearchNode;
    Node _startNode;
    Node _destinationNode;

    
    GridManager _gridManager;

    Vector2Int[] _directions = {Vector2Int.down, Vector2Int.up, Vector2Int.right, Vector2Int.left };
    
    Queue<Node> _frontier = new Queue<Node>();
    Dictionary<Vector2Int, Node> _reached = new Dictionary<Vector2Int, Node>();

    void Awake()
    {
        _gridManager = FindObjectOfType<GridManager>();
        InitializeEdges();
    }
    void Start()
    {
        GetNewPath();
    }

    public List<Node> GetNewPath(Vector2Int _curentPositionCoordinates)
    {
        _gridManager.ResetNodes(); 

        BreadthFirstSearch(_curentPositionCoordinates);
        
        return BuildPath();
    }
    public List<Node> GetNewPath()
    {
        return GetNewPath(_startCoordinates);
    }

    void InitializeEdges()
    {
        _startNode = _gridManager.Grid[_startCoordinates];
        _destinationNode = _gridManager.Grid[_destinationNodeCoordinates];
    }

    void ExploreNeighbors()
    {
        List<Node> _neighbors = new List<Node>();   

        foreach(Vector2Int _direction in _directions)
        {
            Vector2Int _neighborCoordinates = _currentSearchNode.coordinates + _direction;

            if(_gridManager.Grid.ContainsKey(_neighborCoordinates))
            {
                _neighbors.Add(_gridManager.Grid[_neighborCoordinates]);
            }
        }

        foreach(Node _neighbor in _neighbors)
        {
            if(!_reached.ContainsKey(_neighbor.coordinates) && _neighbor.isWalkable)
            {
                _neighbor.conectedTo = _currentSearchNode;

                _reached.Add(_neighbor.coordinates, _neighbor);
                    
                _frontier.Enqueue(_neighbor);
            }
        }
        
    }

    void BreadthFirstSearch(Vector2Int _curentPositionCoordinates)
    {
        _startNode.isWalkable = true;
        _destinationNode.isWalkable = true;

        _frontier.Clear();
        _reached.Clear();

        bool _isRuning = true;

        _frontier.Enqueue(_gridManager.Grid[_curentPositionCoordinates]);
        _reached.Add(_curentPositionCoordinates, _gridManager.Grid[_curentPositionCoordinates]);

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

    public bool WillBlockPath(Vector2Int _coordinates)
    {

        if(_gridManager.Grid.ContainsKey(_coordinates))
        {
           
            bool _previousState = _gridManager.Grid[_coordinates].isWalkable;

            _gridManager.Grid[_coordinates].isWalkable = false;
            List<Node> _newPath = GetNewPath();
            _gridManager.Grid[_coordinates].isWalkable = _previousState;

            if(_newPath.Count <= 1) 
            {
                GetNewPath();
                Debug.Log("will block");
                return true;
            }
        }
        return false;
    }

    public void  NotifyRecievers()
    {
        BroadcastMessage("RecalculatePath",false ,SendMessageOptions.DontRequireReceiver);
    }
}

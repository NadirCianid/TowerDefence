using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node 
{
    public Vector2Int coordinates;
    public bool isWalkable = true;
    public bool isExplored;
    public bool isPath;
    public Node conectedTo;

    public Node(Vector2Int _coordinates, bool _isWalkeble){
        this.coordinates = _coordinates;
        this.isWalkable = _isWalkeble;
    }
}

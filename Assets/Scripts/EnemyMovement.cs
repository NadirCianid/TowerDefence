using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] [Range(0, 5)] float _speed;
    List<Node> _path = new List<Node>();
    Enemy _enemy;
    GridManager _grid;
    Pathfinder _pathfinder;
    void OnEnable()
    {
        MoveToTheStart();
        RecalculatePath(true);
    }

    void Awake() 
    {
        _enemy = GetComponent<Enemy>();
        _grid = FindObjectOfType<GridManager>();
        _pathfinder = FindObjectOfType<Pathfinder>();

    }

    void RecalculatePath(bool _resetPath)
    {
        Vector2Int _coordinates = new Vector2Int();
        if(_resetPath){ _coordinates = _pathfinder.StartCoordinates;}
        else {_coordinates = _grid.GetCoordinatesFromPosition(transform.position);}
        //_coordinates = _grid.GetCoordinatesFromPosition(transform.position);
        
        StopAllCoroutines();
        _path.Clear();
        _path = _pathfinder.GetNewPath(_coordinates);
        StartCoroutine(Mooving());
    }
    void MoveToTheStart()
    {
        transform.position =  _grid.GetPositionFromCoordinates(_pathfinder.StartCoordinates);
    }
    IEnumerator Mooving()
    {
        for(int i=1; i<_path.Count; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = _grid.GetPositionFromCoordinates(_path[i].coordinates);
            float interpolationPersentage = 0;
            transform.LookAt(endPosition);

            while (interpolationPersentage < 1f)
            {

                interpolationPersentage += Time.deltaTime * _speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, interpolationPersentage);
                yield return new WaitForEndOfFrame();
            }

        }
        FinishPath();
    }

    private void FinishPath()
    {
        gameObject.SetActive(false);
        _enemy.StealGold();
    }
}

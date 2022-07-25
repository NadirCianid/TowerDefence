using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Transform> _path = new List<Transform>();
    [SerializeField] [Range(0, 5)] float _speed;
    Enemy _enemy;
    void OnEnable()
    {
        FindPath();
        MoveToTheStart();
        StartCoroutine(Mooving());
        _enemy = GetComponent<Enemy>();
    }

    void FindPath()
    {
        _path.Clear();
        GameObject _parent = GameObject.FindGameObjectWithTag("Path");

        foreach(Transform _child in _parent.transform)
        {
            _path.Add(_child);
        }
    }
    void MoveToTheStart()
    {
        transform.position = _path[0].transform.position;
    }
    IEnumerator Mooving()
    {
        foreach (Transform waypoint in _path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.position;
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

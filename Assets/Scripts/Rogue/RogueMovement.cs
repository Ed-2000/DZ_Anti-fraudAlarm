using System.Collections.Generic;
using UnityEngine;

public class RogueMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> _waypoints;
    [SerializeField] private float _speed = 5;

    private float _minDistanceToWaypoint = 0.5f;
    private int _currentWaypointIndex = 0;

    private void Update()
    {
        if (Vector3.Distance(transform.position, _waypoints[_currentWaypointIndex].position) <= _minDistanceToWaypoint)
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Count;

        Move();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _waypoints[_currentWaypointIndex].position, _speed * Time.deltaTime);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class RogueMovement : MonoBehaviour
{
    [SerializeField] private List<Transform> _waypoints;
    [SerializeField] private float _speed = 5;

    private float _minDistanceToWaypoint = 0.5f;
    private float _minDistanceToWaypointSquared;
    private int _currentWaypointIndex = 0;

    private void Awake()
    {
        _minDistanceToWaypointSquared = _minDistanceToWaypoint * _minDistanceToWaypoint;
    }

    private void Update()
    {
        Vector3 _currentDistanceToWaypointSquared = _waypoints[_currentWaypointIndex].position - transform.position;

        if (_currentDistanceToWaypointSquared.sqrMagnitude <= _minDistanceToWaypointSquared)
            _currentWaypointIndex = ++_currentWaypointIndex % _waypoints.Count;

        Move();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _waypoints[_currentWaypointIndex].position, _speed * Time.deltaTime);
    }
}

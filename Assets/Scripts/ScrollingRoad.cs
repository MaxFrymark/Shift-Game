using System;
using UnityEngine;

public class ScrollingRoad : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _road1;
    [SerializeField] private Rigidbody2D _road2;
    
    private GameObject _bottomRoad;
    private bool _roadOneOnBottom = true;
    
    private Vector3 _roadPosition = new Vector3(0, 13, 0);
    private float _roadLowerBound = -13f;
    private float _speed = 8f;
    
    void Start()
    {
        _bottomRoad = _road1.gameObject;
        _road1.linearVelocity = Vector2.down * _speed;
        _road2.linearVelocity = Vector2.down * _speed;
    }
    
    void Update()
    {
        if(_bottomRoad.transform.position.y <= _roadLowerBound)
        {
            ResetRoadPosition();
        }
    }

    private void ResetRoadPosition()
    {
        _bottomRoad.transform.position = _roadPosition;
        _roadOneOnBottom = !_roadOneOnBottom;
        _bottomRoad = _roadOneOnBottom ? _road1.gameObject : _road2.gameObject;
    }
}

using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] ScoreCounter _scoreCounter;
    
    private readonly Vector2[] _lanes = new []{new Vector2(-1.5f, -4), new Vector2(0, -4), new Vector2(1.5f, -4)};
    private int _currentLane = 1;
    
    private bool _isMoving = false;
    private Vector2 _targetPosition;
    private float _moveSpeed = 20f;

    private void Update()
    {
        if (_isMoving)
        {
            MoveToDestination();
        }
    }

    public void ShiftLane(int shiftDirection)
    {
        int newLane = _currentLane + shiftDirection;
        //Debug.Log($"Shifting lane by {shiftDirection}, new lane: {newLane}");
        if (newLane < 0 || newLane >= _lanes.Length)
            return;
        SetNewLane(newLane);
    }

    private void SetNewLane(int newLane)
    {
        //Debug.Log($"Moving to lane {newLane}");
        _currentLane = newLane;
        _targetPosition = _lanes[newLane];
        _isMoving = true;
    }

    private void MoveToDestination()
    {
        transform.position = Vector2.MoveTowards(transform.position, _targetPosition, _moveSpeed * Time.deltaTime);
        if ((Vector2)transform.position == _targetPosition)
        {
            _isMoving = false;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Gem"))
        {
            collision.gameObject.SetActive(false);
            _scoreCounter.IncreaseScore();
        }
    }
}

using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] ScoreCounter _scoreCounter;
    [SerializeField] AudioSource _swooshEffect;
    [SerializeField] private AudioClip[] _collisionSounds;
    [SerializeField] private GameObject _gameOverScreen;
    
    private readonly Vector2[] _lanes = new []{new Vector2(-1f, -4), new Vector2(0, -4), new Vector2(1f, -4)};
    private int _currentLane = 1;
    
    private bool _isMoving = false;
    private Vector2 _targetPosition;
    private float _moveSpeed = 20f;

    private InputHandler _inputHandler;

    private void Update()
    {
        if (_isMoving)
        {
            MoveToDestination();
        }
    }

    public void SubscribeToInputHandler(InputHandler inputHandler)
    {
        this._inputHandler = inputHandler;
        inputHandler.OnShiftLane += ShiftLane;
    }

    private void ShiftLane(int shiftDirection)
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
        _swooshEffect.Play();
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
            _gameOverScreen.SetActive(true);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Gem"))
        {
            collision.gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(_collisionSounds[0], Camera.main.transform.position);
            _scoreCounter.IncreaseScore();
        }
    }

    private void OnDisable()
    {
        _inputHandler.OnShiftLane -= ShiftLane;
    }
}

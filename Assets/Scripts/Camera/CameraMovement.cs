using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{ 
    [SerializeField] private float _speed;
    [SerializeField] private BoxCollider2D _scopeGameField;

    private bool _isMove;
    private Vector3 _targetPosition;

    private void Update()
    {
        if (_targetPosition != transform.position && _isMove == true)
        {
            Move(_targetPosition);
        }
        else if (_isMove == false)
        {
            _targetPosition = transform.position;
        }
    }

    public void LaunchMove(Vector3 directionSwipe)
    {
        SetTargetPosition(directionSwipe);
        _isMove = true;
    }

    private void Move(Vector3 targetPosition)
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, targetPosition.x, _speed * Time.deltaTime),
                Mathf.Lerp(transform.position.y, targetPosition.y, _speed * Time.deltaTime), transform.position.z);
        if (CheckProximity(transform.position, targetPosition, 0.01f))
        {
            _isMove = false;
        }
    }

    private bool CheckProximity(Vector3 currentPosition, Vector3 targetPosition, float permissibleValue)
    {
        return (Mathf.Abs(targetPosition.x - currentPosition.x) <= permissibleValue && Mathf.Abs(targetPosition.y - currentPosition.y) <= permissibleValue);
    }

    private void SetTargetPosition(Vector3 directionSwipe)
    {              
        _targetPosition = new Vector3(Mathf.Clamp(transform.position.x - directionSwipe.x, _scopeGameField.bounds.min.x / 2, _scopeGameField.bounds.max.x / 2),
                Mathf.Clamp(transform.position.y - directionSwipe.y, _scopeGameField.bounds.min.y / 2, _scopeGameField.bounds.max.y / 2), transform.position.z);
    }
}
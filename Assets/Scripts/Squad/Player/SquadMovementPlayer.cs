using System;
using UnityEngine;

public class SquadMovementPlayer : MonoBehaviour
{    
    private GameObject _directionNavigator;
    private SquadData _squadData;    

    public void Init(SquadData squadData)
    {
        _squadData = squadData;
        _directionNavigator = squadData.DirectionNavigator;
    }
    
    private void Update()
    {
        ChangePosition();
    }

    public void LaunchMove()
    {
        _squadData.IsMove = true;
        _squadData.IsRotating = true;

        _directionNavigator.transform.SetParent(null);        
    }
    
    private void ChangePosition()
    {
        if (_squadData.IsMove == true)
        {
            if (_squadData.IsRotating == true)
            {
                Rotation();
                if (CheckProximity(transform.rotation, _directionNavigator.transform.rotation, 0.1f))
                {
                    _directionNavigator.transform.parent = transform;
                    _squadData.IsRotating = false;
                }
            }
            else if (_squadData.IsRotating == false)
            {
                Move();
                if (CheckProximity(transform.position, _squadData.TargetPosition, 0.01f))
                {
                    _squadData.IsMove = false;
                }
            }
        }        
    }

    private void Move()
    {
        transform.Translate(GetDirection(_squadData.TargetPosition, transform.position) * Time.deltaTime * _squadData.SpeedMove, Space.World);
    }

    private void Rotation()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _directionNavigator.transform.rotation, _squadData.SpeedRotate * Time.deltaTime);        
    }

    private bool CheckProximity(Vector3 currentPosition, Vector3 targetPosition, float permissibleValue)
    {
        return (Mathf.Abs(targetPosition.x - currentPosition.x) <= permissibleValue && Mathf.Abs(targetPosition.y - currentPosition.y) <= permissibleValue);        
    }

    private bool CheckProximity(Quaternion currentQuatrenion, Quaternion targetQuatrenion, float permissibleValue)
    {
        return (Mathf.Abs(Quaternion.Angle(currentQuatrenion, targetQuatrenion)) <= permissibleValue);
    }

    private Vector3 GetDirection(Vector3 whereTo, Vector3 whereFrom)
    {
        Vector3 heading = whereTo - whereFrom;
        float distance = heading.magnitude;

        return heading / distance;
    }
}
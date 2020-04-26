using UnityEngine;

public class SquadDirectionNavigator : MonoBehaviour
{
    private SquadData _squadData;

    public void Init(SquadData squadData)
    {
        _squadData = squadData;
    }

    public void SetDirectionMove()
    {
        Vector3 targetPosition = _squadData.TargetPosition;        
        transform.up = targetPosition - transform.position;        
    }
}

using UnityEngine;

public class SquadDirectionNavigator : MonoBehaviour
{
    [SerializeField] private Squad _squad;
    public void SetDirectionMove()
    {
        Vector3 targetPosition = _squad.SquadData.TargetPosition;
        targetPosition.z = 0;
        transform.up = targetPosition - transform.position;        
    }
}

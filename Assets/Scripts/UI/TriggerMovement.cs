using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMovement : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void ShiftTrigger()
    {
        _animator.SetTrigger("isShot");
    }
}

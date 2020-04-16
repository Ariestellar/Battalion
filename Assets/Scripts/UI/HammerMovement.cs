using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerMovement : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void ShiftHammer(int numberAttackZone) 
    {
        _animator.SetInteger("AttackZone", numberAttackZone);
    }
}

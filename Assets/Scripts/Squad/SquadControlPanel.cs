using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SquadControlPanel : MonoBehaviour
{
    private SquadData _squadData;    
    private HammerMovement _animatorHammer;    
    private TriggerMovement _animatorTrigger; 
    
    public UnityAction _setAttackZone;    
    
    public void Init(GameObject squadControlPanel, SquadData squadData)
    {
        _squadData = squadData;
        _animatorHammer = squadControlPanel.GetComponent<HammerMovement>();
        _animatorTrigger = squadControlPanel.GetComponent<TriggerMovement>();
        ChangeAttackZone(_squadData.ActiveAttackZone);
    }

    public void ChangeAttackZone(int numberAttackZone)        
    {        
        _squadData.ActiveAttackZone = numberAttackZone;
        _setAttackZone?.Invoke();
        _animatorHammer.ShiftHammer(numberAttackZone);        
    }    

    public void FireCommand()
    {        
        if (_squadData.IsReadinessShot == true)
        {
            _squadData.IsShot = true;
            _animatorTrigger.ShiftTrigger();            
            _squadData.IsReadinessShot = false;
        }     
    }
}

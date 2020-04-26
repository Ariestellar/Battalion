using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadSelectedPlayer : MonoBehaviour
{    
    private SquadData _squadData;    
    private SpriteRenderer[] _zoneAttakList;   

    public void Init(SquadData squadData)
    {
        _squadData = squadData;              
        _zoneAttakList = squadData.ZoneAttack.GetComponentsInChildren<SpriteRenderer>();        
    }

    public void Switch(bool value)
    {
        _squadData.Highlighting.SetActive(value);
        _squadData.ZoneAttack.SetActive(value);        
        _squadData.IsHighlighting = value;        
    }

    public void SetHighlightZoneAttack()
    {        
        for (int i = 0; i < _zoneAttakList.Length; i++)
        {
            if (i <= _squadData.ActiveAttackZone)
            {
                _zoneAttakList[i].GetComponent<SpriteRenderer>().enabled = true;
            } 
            else if (i > _squadData.ActiveAttackZone)
            {
                _zoneAttakList[i].GetComponent<SpriteRenderer>().enabled = false;
            }            
        }
    }    
}

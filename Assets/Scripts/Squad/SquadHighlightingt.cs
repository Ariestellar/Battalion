using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SquadControlPanel))]
public class SquadHighlightingt : MonoBehaviour
{     
    [SerializeField] private GameObject _highlighting;
    [SerializeField] private GameObject _zoneAtack;

    private SquadData _squadData;    
    private SpriteRenderer[] _zoneAttakList;    
    
    public void Init(SquadData squadData)
    {
        _squadData = squadData;
        _zoneAttakList = _zoneAtack.GetComponentsInChildren<SpriteRenderer>();
    }

    public void Switch(bool value)
    {           
        _highlighting.SetActive(value);
        _zoneAtack.SetActive(value);        
        _squadData.IsHighlighting = value;        
    }

    public void SetHighlightZoneAttack()
    {        
        for (int i = 0; i < _zoneAttakList.Length; i++)
        {
            if (i <= _squadData.ActiveAttackZone)
            {
                _zoneAttakList[i].enabled = true;
            } 
            else if (i > _squadData.ActiveAttackZone)
            {
                _zoneAttakList[i].enabled = false;
            }            
        }
    }
}

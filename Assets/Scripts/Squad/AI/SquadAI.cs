using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadAI : MonoBehaviour, Squad
{
    private SquadData _squadData;
    private Transform[] _childrenList;
    private SquadWeapon _squadWeapon;

    public SquadData SquadData { get => _squadData; set => _squadData = value; }

    public void Init(SquadData squadData)
    {
        _squadData = squadData;

        _childrenList = GetComponentsInChildren<Transform>(true);

        squadData.SquadFiller = _childrenList[1].gameObject;
        squadData.ZoneAttack = _childrenList[2].gameObject;
        squadData.Highlighting = _childrenList[7].gameObject;        

        squadData.SquadFiller.GetComponent<SquadFiller>().Init(Color.red);

        _squadWeapon = gameObject.AddComponent<SquadWeapon>();
        _squadWeapon.Init(squadData);
    }

    public void Select(bool value)
    {
        
    }

    public void ReduceSquadSize(float angleAttack, int numberSuccessfulShots)
    {        
        _squadData.SquadFiller.GetComponent<SquadFiller>().SetFillBar(_squadData.Size / 100);
        _squadWeapon.ReduceAtackZoneSize(_squadData.Size / 100);
    }
}

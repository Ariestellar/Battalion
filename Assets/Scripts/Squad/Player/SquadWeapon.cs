using SquadParameters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadWeapon : MonoBehaviour
{
    [SerializeField] private Transform[] _zoneAtackList;
    private GameObject _zoneAtack;

    [SerializeField] private List<GameObject> _enemySquadFirstAttackZone;
    [SerializeField] private List<GameObject> _enemySquadSecondAttackZone;
    [SerializeField] private List<GameObject> _enemySquadThirdAttackZone;    
    [SerializeField] private float _sizeSquad;    
    [SerializeField] private Dictionary<FactorSquad, float> _factor;

    public void Init(SquadData squadData)
    {
        _zoneAtack = squadData.ZoneAttack;
        _sizeSquad = squadData.Size;
        _factor = squadData.Factor;
        
        _zoneAtackList = _zoneAtack.GetComponentsInChildren<Transform>();         
    }

    public void Fire()
    {
        ToDamageUnitsInAttackZone();            
    }

    public void ReduceAtackZoneSize(float number)
    {
        number = Mathf.Clamp01(number);        
        _zoneAtack.transform.localScale = new Vector3(number, 1, 1);
    }

    private void ToDamage(List<GameObject> enemys, float hitProbability)
    {
        foreach (var enemy in enemys)
        {
            enemy.GetComponent<Squad>().ReduceSquadSize(GetAngleAttack(enemy), GetNumberDamage(hitProbability));            
        }
    }

    private float GetAngleAttack(GameObject enemy)
    {        
        return Vector3.Angle(enemy.transform.up, transform.up);
    }

    private void ToDamageUnitsInAttackZone()
    {        
        ToDamage(_zoneAtackList[1].GetComponent<AttackAreaWatcher>().ObjectsInZone, _factor[FactorSquad.HitInZone]);
        ToDamage(_zoneAtackList[2].GetComponent<AttackAreaWatcher>().ObjectsInZone, _factor[FactorSquad.HitInZone]/2);
        ToDamage(_zoneAtackList[3].GetComponent<AttackAreaWatcher>().ObjectsInZone, _factor[FactorSquad.HitInZone]/4);        
    }

    private int GetNumberDamage(float hitProbability)
    {
        int maximalTotalDamage = Mathf.RoundToInt(_sizeSquad * _factor[FactorSquad.HitsIndividualSoldiers] * hitProbability);
        int minimalTotalDamage = maximalTotalDamage - Mathf.RoundToInt(maximalTotalDamage * _factor[FactorSquad.Сhance]);
        return Random.Range(minimalTotalDamage, maximalTotalDamage);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadWeapon : MonoBehaviour
{    
    private GameObject _zoneAtack;

    [SerializeField] private List<GameObject> _enemySquadFirstAttackZone;
    [SerializeField] private List<GameObject> _enemySquadSecondAttackZone;
    [SerializeField] private List<GameObject> _enemySquadThirdAttackZone;
    [SerializeField] private Transform[] _zoneAtackList;
    [SerializeField] private float _angle;

    public void Init(SquadData squadData)
    {
        _zoneAtack = squadData.ZoneAttack;
        _zoneAtackList = _zoneAtack.GetComponentsInChildren<Transform>();        
    }

    public void Fire()
    {
        ToDamageUnitsInAttackZone();
        /*CheckAllShotAreas();
        ToDamage(_enemySquadFirstAttackZone, 1);
        ToDamage(_enemySquadSecondAttackZone, 0.5f);
        ToDamage(_enemySquadThirdAttackZone, 0.25f);*/      
    }

    public void ReduceAtackZoneSize(float number)
    {
        number = Mathf.Clamp01(number);        
        _zoneAtack.transform.localScale = new Vector3(number, 1, 1);
    }

    /*private void CheckAllShotAreas()
    {
        _enemySquadFirstAttackZone = _zoneAtackList[1].GetComponent<AttackAreaWatcher>().ObjectsInZone;
        _enemySquadSecondAttackZone = _zoneAtackList[2].GetComponent<AttackAreaWatcher>().ObjectsInZone;
        _enemySquadThirdAttackZone = _zoneAtackList[3].GetComponent<AttackAreaWatcher>().ObjectsInZone;
    }*/

    private void ToDamage(List<GameObject> enemys, float hitProbability)
    {
        foreach (var enemy in enemys)
        {
            enemy.GetComponent<Squad>().ReduceSquadSize(hitProbability, GetAngleAttack(enemy));            
        }
    }
    private float GetAngleAttack(GameObject enemy)
    {        
        return Vector3.Angle(enemy.transform.up, transform.up);
    }

    private void ToDamageUnitsInAttackZone()
    {
        ToDamage(_zoneAtackList[1].GetComponent<AttackAreaWatcher>().ObjectsInZone, 1);
        ToDamage(_zoneAtackList[2].GetComponent<AttackAreaWatcher>().ObjectsInZone, 0.5f);
        ToDamage(_zoneAtackList[3].GetComponent<AttackAreaWatcher>().ObjectsInZone, 0.25f);
    }
}

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

    public void Init(SquadData squadData)
    {
        _zoneAtack = squadData.ZoneAttack;
        _zoneAtackList = _zoneAtack.GetComponentsInChildren<Transform>();        
    }

    public void Fire()
    {
        CheckAllShotAreas();
        ToDamage(_enemySquadFirstAttackZone, 30);
        ToDamage(_enemySquadSecondAttackZone, 20);
        ToDamage(_enemySquadThirdAttackZone, 10);        
    }

    public void ReduceAtackZoneSize(float number)
    {
        number = Mathf.Clamp01(number);        
        _zoneAtack.transform.localScale = new Vector3(number, 1, 1);
    }

    private void CheckAllShotAreas()
    {
        _enemySquadFirstAttackZone = _zoneAtackList[1].GetComponent<AttackAreaWatcher>().ObjectsInZone;
        _enemySquadSecondAttackZone = _zoneAtackList[2].GetComponent<AttackAreaWatcher>().ObjectsInZone;
        _enemySquadThirdAttackZone = _zoneAtackList[3].GetComponent<AttackAreaWatcher>().ObjectsInZone;
    }

    private void ToDamage(List<GameObject> enemys, float amountDamage)
    {
        foreach (var enemy in enemys)
        {
            enemy.GetComponent<Squad>().ReduceSquadSize(amountDamage);
            //enemy.GetComponent<SquadWeapon>().ReduceAtackZoneSize(amountDamage);
        }
    }
}

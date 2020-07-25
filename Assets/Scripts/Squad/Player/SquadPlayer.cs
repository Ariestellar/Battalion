using SquadParameters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SquadPlayer : MonoBehaviour, Squad
{
    private SquadData _squadData;
    
    private SquadSelectedPlayer _squadSelected;
    private SquadControlPanel _squadControlPanel;
    private SquadMovementPlayer _squadMovement;
    private SquadWeapon _squadWeapon;
    private DamageDisplay _damageDisplay;
    private ParticleSystem _effectSmoke;

    private GameObject _squadControlPanelUI;
    private ClickHandler _clickHandler;    
    private Transform[] _childrenList;
       
    public void Init(SquadData squadData)
    {       
        _squadData = squadData;

        _childrenList = GetComponentsInChildren<Transform>(true);

        squadData.SquadFiller = _childrenList[1].gameObject;
        squadData.ZoneAttack = _childrenList[2].gameObject;
        squadData.Highlighting = _childrenList[7].gameObject;
        _effectSmoke = _childrenList[8].gameObject.GetComponent<ParticleSystem>();

        _squadControlPanelUI = squadData.SquadUI;
        _clickHandler = squadData.ClickHandler;
        
        _squadMovement = gameObject.AddComponent<SquadMovementPlayer>();
        _squadSelected = gameObject.AddComponent<SquadSelectedPlayer>();
        _squadControlPanel = gameObject.AddComponent<SquadControlPanel>();        
        _squadWeapon = gameObject.AddComponent<SquadWeapon>();
        _damageDisplay = GetComponent<DamageDisplay>();

        squadData.SquadFiller.GetComponent<SquadFiller>().Init(Color.blue);
        _squadMovement.Init(squadData);
        _squadSelected.Init(squadData);
        _squadControlPanel.Init(squadData);
        _squadWeapon.Init(squadData);
    }

    public void Select(bool value)
    {
        _squadSelected.Switch(value);
        if (value == true)
        {
            SubscriptionAllEvents();
        }
        else if (value == false)
        {
            UnsubscribeAllEvents();
        }
        _squadControlPanelUI.GetComponent<PanelMovement>().Display(value);
        _squadSelected.Switch(value);        
    }

    public void ReduceSquadSize(float angleAttack, int numberDamageTaken)
    {        
        int totalDamage = GetTotalDamage(numberDamageTaken, angleAttack);
        _squadData.Size -= totalDamage;
        _damageDisplay.SetTextDamage(totalDamage);
        _squadData.SquadFiller.GetComponent<SquadFiller>().SetFillBar(_squadData.Size / 100);
        _squadWeapon.ReduceAtackZoneSize(_squadData.Size / 100);
        if (_squadData.Size == 0)
        {
            UnsubscribeAllEvents();
            Destroy(this.gameObject);
        }
    }

    public void SetTargetPosition(Vector3 value)
    {
        value.z = transform.position.z;
        _squadData.TargetPosition = value;
    }

    private void SubscriptionAllEvents()
    {
        _clickHandler.NewSquadPositionSet += _squadMovement.LaunchMove;
        _clickHandler.NewSquadPositionSet += _squadData.DirectionNavigator.GetComponent<SquadDirectionNavigator>().SetDirectionMove;

        _squadControlPanelUI.GetComponent<ButtonTouch>().Init(_squadControlPanel);
        _squadControlPanel._setAttackZone += _squadSelected.SetHighlightZoneAttack;
        _squadControlPanel._giveCommandFire += _squadWeapon.Fire;
        _squadControlPanel._giveCommandFire += _effectSmoke.Play;
        _squadControlPanel.ChangeAttackZone(_squadData.ActiveAttackZone);
    }

    private void UnsubscribeAllEvents()
    {
        _clickHandler.NewSquadPositionSet -= _squadMovement.LaunchMove;
        _clickHandler.NewSquadPositionSet -= _squadData.DirectionNavigator.GetComponent<SquadDirectionNavigator>().SetDirectionMove;

        _squadControlPanelUI.GetComponent<ButtonTouch>().OnDisable();
        _squadControlPanel._giveCommandFire -= _squadWeapon.Fire;
        _squadControlPanel._giveCommandFire -= _effectSmoke.Play;
        _squadControlPanel._setAttackZone -= _squadSelected.SetHighlightZoneAttack;
    }

    private int GetNumberSoldiersOnLine(int lineNumber, float angleAttack)
    {
        if (angleAttack == 0 || angleAttack == 180)
        {
            return Mathf.RoundToInt(_squadData.Size / _squadData.NumberRows);
        }
        else
        {
            if (lineNumber == 1)
            {
                return Mathf.RoundToInt(_squadData.Size * ((90 - angleAttack) / 90) / _squadData.NumberRows + _squadData.NumberRows - 1);
            }
            else 
            {
                return Mathf.RoundToInt(_squadData.Size * ((90 - angleAttack) / 90) / _squadData.NumberRows - 1);
            }            
        }
    }

    //Распределение урона по линиям
    //factorRemontessLine - фактор удаленности шеренги.
    //На первую шеренгу штраф не действует(factorRemontessLine = 1), каждая следующая шеренга дает штраф к оставшемуся от нанесения по первой линии урону - 50%(factorRemontessLine/2)
    //Если величина атакуемого отряда меньше, чем количество нанесенного урона в более чем два раза, то отряд уничтожается полностью. 
    private int GetTotalDamage(int numberDamageTaken, float angleAttack)
    {
        float totalDamage = 0;
        float factorRemontessLine = _squadData.Factor[FactorSquad.RemotenessLine];

        if (_squadData.Size < numberDamageTaken / 2)
        {
            totalDamage = _squadData.Size;
        }
        else 
        {
            for (int i = 1; i <= _squadData.NumberRows; i++, factorRemontessLine /= 2)
            {
                if (GetNumberSoldiersOnLine(i, angleAttack) >= numberDamageTaken)
                {
                    totalDamage += numberDamageTaken * factorRemontessLine;                    
                    break;
                }
                else
                {
                    totalDamage += GetNumberSoldiersOnLine(i, angleAttack) * factorRemontessLine;
                    numberDamageTaken -= Mathf.RoundToInt(totalDamage);                    
                }
            }
        }        
        return Mathf.RoundToInt(totalDamage);
    }
}

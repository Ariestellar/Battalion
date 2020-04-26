using SquadParameters;
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

        _squadControlPanelUI = squadData.SquadUI;
        _clickHandler = squadData.ClickHandler;
        
        _squadMovement = gameObject.AddComponent<SquadMovementPlayer>();
        _squadSelected = gameObject.AddComponent<SquadSelectedPlayer>();
        _squadControlPanel = gameObject.AddComponent<SquadControlPanel>();        
        _squadWeapon = gameObject.AddComponent<SquadWeapon>();

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

    public void ReduceSquadSize(float number)
    {
        _squadData.Size -= number;
        _squadData.SquadFiller.GetComponent<SquadFiller>().SetFillBar(_squadData.Size / 100);
        _squadWeapon.ReduceAtackZoneSize(_squadData.Size / 100);
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
        _squadControlPanel.ChangeAttackZone(_squadData.ActiveAttackZone);
    }

    private void UnsubscribeAllEvents()
    {
        _clickHandler.NewSquadPositionSet -= _squadMovement.LaunchMove;
        _clickHandler.NewSquadPositionSet -= _squadData.DirectionNavigator.GetComponent<SquadDirectionNavigator>().SetDirectionMove;

        _squadControlPanelUI.GetComponent<ButtonTouch>().OnDisable();
        _squadControlPanel._giveCommandFire -= _squadWeapon.Fire;
        _squadControlPanel._setAttackZone -= _squadSelected.SetHighlightZoneAttack;
    }    
}

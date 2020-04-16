using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Squad : MonoBehaviour
{
    [SerializeField] private SquadData _squadData;
    
    private SquadHighlightingt _squadHighlightingt;
    private SquadControlPanel _squadControlPanel;
    private SquadMovement _squadMovement;
    private GameObject _squadControlPanelUI;
    private ClickHandler _clickHandler;
    private SquadDirectionNavigator _directionNavigator;

    public SquadData SquadData => _squadData;

    public void Init(ClickHandler clickHandler, GameObject squadControlPanel, string name)
    {        
        _squadData = CreateData();
        SetStartingValues();

        _squadMovement = GetComponent<SquadMovement>();
        _squadHighlightingt = GetComponent<SquadHighlightingt>();
        _squadControlPanel = GetComponent<SquadControlPanel>();
        _squadControlPanelUI = squadControlPanel;
        _clickHandler = clickHandler;
        _directionNavigator = GetComponentInChildren<SquadDirectionNavigator>();

        _squadMovement.Init(_squadData);
        _squadHighlightingt.Init(_squadData);
        _squadControlPanel.Init(squadControlPanel, _squadData);
    }

    private SquadData CreateData()
    {
        SquadData data = ScriptableObject.CreateInstance<SquadData>();        
        AssetDatabase.CreateAsset(data, "Assets/Scripts/ScriptableObjects/SquadData" + name + ".asset");
        AssetDatabase.Refresh();
        return data;
    }
    private void SetStartingValues()
    {
        _squadData.SpeedMove = 0.5f;
        _squadData.SpeedRotate = 10f;
        _squadData.ActiveAttackZone = 2;
        _squadData.IsShot = false;
        _squadData.IsReadinessShot = true;
        _squadData.IsHighlighting = false;
        _squadData.IsMove = false;
        _squadData.IsRotating = false;
    }

    public void Select(bool value)
    {
        if (value == true)
        {
            _clickHandler.NewSquadPositionSet += _squadMovement.LaunchMove;
            _clickHandler.NewSquadPositionSet += _directionNavigator.SetDirectionMove;

            _squadControlPanelUI.GetComponent<ButtonTouch>().Init(gameObject);
            _squadControlPanel._setAttackZone += _squadHighlightingt.SetHighlightZoneAttack;
            _squadControlPanel.ChangeAttackZone(_squadData.ActiveAttackZone);
        }
        else if (value == false)
        {
            _clickHandler.NewSquadPositionSet -= _squadMovement.LaunchMove;
            _clickHandler.NewSquadPositionSet -= _directionNavigator.SetDirectionMove;

            _squadControlPanelUI.GetComponent<ButtonTouch>().OnDisable();
            _squadControlPanel._setAttackZone -= _squadHighlightingt.SetHighlightZoneAttack;
        }
        _squadControlPanelUI.GetComponent<PanelMovement>().Display(value);
        _squadHighlightingt.Switch(value);
    }
}

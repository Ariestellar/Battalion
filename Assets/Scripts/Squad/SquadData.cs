using SquadParameters;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSquadData", menuName = "SquadData", order = 51)]
public class SquadData : ScriptableObject
{
    [SerializeField] private String _name;
    [SerializeField] private Side _side;
    [SerializeField] private TypeArmy _typeArmy;

    [SerializeField] private float _size;
    [SerializeField] private float _numberRows;

    [SerializeField] private GameObject _squad;
    [SerializeField] private GameObject _zoneAttackView;    
    [SerializeField] private GameObject _highlighting;    
    [SerializeField] private GameObject _directionNavigator;
    [SerializeField] private GameObject _squadUI;
    [SerializeField] private GameObject _squadFiller;
    [SerializeField] private ClickHandler _clickHandler;

    [SerializeField] private float _speedMove;//0.02 = 1м/сек. При масштабе: юнит = 50 метров.
    [SerializeField] private float _speedRotate;
    [SerializeField] private int _activeAttackZone;
    [SerializeField] private bool _isShot;
    [SerializeField] private bool _isReadinessShot;
    [SerializeField] private bool _isHighlighting;
    [SerializeField] private bool _isMove;
    [SerializeField] private bool _isRotating;
    [SerializeField] private Vector3 _targetPosition;
    [SerializeField] private Vector3 _currentPosition;    
    
    public float SpeedMove => _speedMove;
    public float SpeedRotate => _speedRotate;
    public int ActiveAttackZone { get => _activeAttackZone; set => _activeAttackZone = value; }
    public bool IsShot { get => _isShot; set => _isShot = value; }
    public bool IsReadinessShot { get => _isReadinessShot; set => _isReadinessShot = value; }
    public bool IsHighlighting { get => _isHighlighting; set => _isHighlighting = value; }
    public bool IsMove { get => _isMove; set => _isMove = value; }
    public bool IsRotating { get => _isRotating; set => _isRotating = value; }
    public Vector3 TargetPosition { get => _targetPosition; set => _targetPosition = value; }
    public Vector3 CurrentPosition { get => _currentPosition; set => _currentPosition = value; }
    public Side Side { get => _side; set => _side = value; }
    public GameObject SquadUI { get => _squadUI; set => _squadUI = value; }
    public ClickHandler ClickHandler { get => _clickHandler; set => _clickHandler = value; }
    public GameObject Squad { get => _squad; set => _squad = value; }
    public GameObject DirectionNavigator { get => _directionNavigator; set => _directionNavigator = value; }
    public GameObject ZoneAttack { get => _zoneAttackView; set => _zoneAttackView = value; }
    public GameObject Highlighting { get => _highlighting; set => _highlighting = value; }
    public float Size { get => _size; set => _size = value; }
    public GameObject SquadFiller { get => _squadFiller; set => _squadFiller = value; }
    public float NumberRows { get => _numberRows; set => _numberRows = value; }

    public static SquadData CreateSquadData(String name, Side side, ClickHandler clickHandler, GameObject squadUI)
    {        
        SquadData data = ScriptableObject.CreateInstance<SquadData>();        
        AssetDatabase.CreateAsset(data, "Assets/Scripts/ScriptableObjects/SquadData" + name + ".asset");
        data.SetStartingValues(name, side, clickHandler, squadUI);
        AssetDatabase.Refresh();
        return data;
    }

    private void SetStartingValues(String name, Side side, ClickHandler clickHandler, GameObject squadUI)
    {
        Side = side;
        _name = name;
        _size = 100;
        _numberRows = 2;
        _speedMove = 0.5f;
        _speedRotate = 10f;
        _activeAttackZone = 2;
        _isShot = false;
        _isReadinessShot = true;
        _isHighlighting = false;
        _isMove = false;
        _isRotating = false;
        ClickHandler = clickHandler;
        _squadUI = squadUI;
    } 
}

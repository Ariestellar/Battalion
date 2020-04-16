using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSquadData", menuName = "SquadData", order = 51)]
public class SquadData : ScriptableObject
{
    [SerializeField] private float _speedMove;//0.02 - 1м/сек учитывая что юнит 50 метров
    [SerializeField] private float _speedRotate;
    [SerializeField] private int _activeAttackZone;
    [SerializeField] private bool _isShot;
    [SerializeField] private bool _isReadinessShot;
    [SerializeField] private bool _isHighlighting;
    [SerializeField] private bool _isMove;
    [SerializeField] private bool _isRotating;
    [SerializeField] private Vector3 _targetPosition;
    
    public float SpeedMove { get => _speedMove; set => _speedMove = value; }
    public float SpeedRotate { get => _speedRotate; set => _speedRotate = value; }
    public int ActiveAttackZone { get => _activeAttackZone; set => _activeAttackZone = value; }
    public bool IsShot { get => _isShot; set => _isShot = value; }
    public bool IsReadinessShot { get => _isReadinessShot; set => _isReadinessShot = value; }
    public bool IsHighlighting { get => _isHighlighting; set => _isHighlighting = value; }
    public bool IsMove { get => _isMove; set => _isMove = value; }
    public bool IsRotating { get => _isRotating; set => _isRotating = value; }
    public Vector3 TargetPosition { get => _targetPosition; set => _targetPosition = value; }

    public static void CreateSquadData()
    {
        SquadData data = ScriptableObject.CreateInstance<SquadData>();
        AssetDatabase.CreateAsset(data, "Assets/Scripts/ScriptableObjects/SquadData.asset");
        AssetDatabase.Refresh();
    }
}

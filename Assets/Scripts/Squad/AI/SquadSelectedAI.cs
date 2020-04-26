using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadSelectedAI : MonoBehaviour
{
    private GameObject _highlighting;
    private GameObject _zoneAtack;
    private SquadData _squadData;
    private SpriteRenderer[] _zoneAttakList;

    public void Init(SquadData squadData, GameObject zoneAtack, GameObject highlighting)
    {
        _zoneAtack = zoneAtack;
        _squadData = squadData;
        _zoneAttakList = zoneAtack.GetComponentsInChildren<SpriteRenderer>();
        _highlighting = highlighting;
    }

    public void Switch(bool value)
    {
        _squadData.IsHighlighting = value;
    }
}

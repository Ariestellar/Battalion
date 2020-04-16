using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerated : MonoBehaviour
{
    [SerializeField] private GameObject _prefabSquad;
    [SerializeField] private GameObject _prefabSquadUI;
    [SerializeField] private GameObject _canvasUI;
    [SerializeField] private GameObject _squadUI;
    [SerializeField] private ClickHandler _clickHandler;     
    [SerializeField] private int _countSquad;     
    [SerializeField] private List<GameObject> _squadList;     
    
    void Start()
    {
        _squadUI = CreateSquadUI();
        for (int i = 0; i < _countSquad; i++)
        {
            _squadList.Add(CreateSquad(_squadUI, "Battalion"+ (i+1)));
            _squadList[i].transform.position = new Vector3(i, 0, 1);            
        }        
    }

    private GameObject CreateSquad(GameObject squadUI, string name)
    {
        GameObject squad;

        squad = Instantiate(_prefabSquad);
        //squad.AddComponent<BoxCollider2D>();
        squad.name = name;
        squad.GetComponent<Squad>().Init(_clickHandler, squadUI, name);

        return squad;
    }

    private GameObject CreateSquadUI()
    {
        return Instantiate(_prefabSquadUI, _canvasUI.transform);
    }

}

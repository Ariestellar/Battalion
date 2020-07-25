using SquadParameters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerated : MonoBehaviour
{ 
    [SerializeField] private int _countSquadPlayer;
    [SerializeField] private int _countSquadAI;   
    [SerializeField] private SquadFactoryAI _squadFactoryAI;
    [SerializeField] private SquadFactoryPlayer _squadFactoryPlayer;
    [SerializeField] private GameObject _prefabSquadUI; 
    [SerializeField] private GameObject _canvasUI;
    [SerializeField] private List<GameObject> _squadListPlayer;
    [SerializeField] private List<GameObject> _squadListAI;

    private GameObject _squadUI;
    
    void Start()
    {
        _squadUI = CreateSquadUI();

        SquadFactory squadFactory = GetSquadFactory(Side.Player);
        squadFactory.CreateSquad(_squadUI, "Battalion" + 1 + "Player", new Vector3(-3, 0, -1), TypeArmy.Infantry);
        squadFactory.CreateSquad(_squadUI, "Battalion" + 2 + "Player", new Vector3(-3, 1, -1), TypeArmy.Infantry);
        squadFactory.CreateSquad(_squadUI, "Battalion" + 3 + "Player", new Vector3(0, 0, -1), TypeArmy.Infantry);
        squadFactory.CreateSquad(_squadUI, "Battalion" + 4 + "Player", new Vector3(0, 2, -1), TypeArmy.Infantry);
        squadFactory.CreateSquad(_squadUI, "Battalion" + 5 + "Player", new Vector3(3, 0, -1), TypeArmy.Infantry);
        squadFactory.CreateSquad(_squadUI, "Battalion" + 6 + "Player", new Vector3(3, 3, -1), TypeArmy.Infantry);
    }

    private GameObject CreateSquadUI()
    {
        return Instantiate(_prefabSquadUI, _canvasUI.transform);
    }

    private SquadFactory GetSquadFactory(Side side)
    {
        SquadFactory squadFactory = null;

        if (side == Side.Player)
        {
            squadFactory = _squadFactoryPlayer;
        } 
        else if (side == Side.AI)
        {
            squadFactory = _squadFactoryAI;
        }

        return squadFactory;
    }
}

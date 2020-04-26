using SquadParameters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadFactoryAI : MonoBehaviour, SquadFactory
{    
    [SerializeField] private CreatorSquad _creatorSquad;
    [SerializeField] private ClickHandler _clickHandler;    

    public GameObject CreateSquad(GameObject squadUI, string name, Vector3 currentPosition, TypeArmy typeArmy)
    {        
        GameObject squad = _creatorSquad.GetConcreteSquad(currentPosition, typeArmy);

        squad.name = name;
        squad.AddComponent<SquadAI>();

        SquadData squadData = SquadData.CreateSquadData(name, Side.AI, _clickHandler, squadUI);
        squadData.Squad = squad;

        squadData.DirectionNavigator = CreateDirectionNavigator(squadData);

        squad.GetComponent<Squad>().Init(squadData);
        return squad;
    }

    private GameObject CreateDirectionNavigator(SquadData squadData) 
    {
        GameObject squadDirectionNavigator = new GameObject("SquadDirectionNavigator");
        squadDirectionNavigator.transform.SetParent(squadData.Squad.transform, false);

        squadDirectionNavigator.AddComponent<SpriteRenderer>();
        squadDirectionNavigator.GetComponent<SpriteRenderer>().sprite = squadData.Squad.GetComponent<SpriteRenderer>().sprite;
        squadDirectionNavigator.GetComponent<SpriteRenderer>().color = new Color(0, 0, 1, 0.3f);
        squadDirectionNavigator.GetComponent<SpriteRenderer>().sortingOrder = 3;

        squadDirectionNavigator.AddComponent<SquadDirectionNavigator>();
        squadDirectionNavigator.GetComponent<SquadDirectionNavigator>().Init(squadData);

        return squadDirectionNavigator;
    }
}
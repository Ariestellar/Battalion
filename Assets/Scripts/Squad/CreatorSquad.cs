using SquadParameters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatorSquad : MonoBehaviour
{
    [SerializeField] private GameObject _prefabSquadInfantry;
    [SerializeField] private GameObject _prefabSquadArtillery;
    [SerializeField] private GameObject _prefabSquadCavalry;

    [SerializeField] private ClickHandler _clickHandler;

    public GameObject GetConcreteSquad(Vector3 currentPosition, TypeArmy typeArmy)
    {
        return Instantiate(GetPrefab(typeArmy), currentPosition, Quaternion.identity);        
    }

    private GameObject GetPrefab(TypeArmy typeArmy)
    {
        GameObject prefabSquad = null;

        if (typeArmy == TypeArmy.Infantry)
        {
            prefabSquad = _prefabSquadInfantry;
        }
        else if (typeArmy == TypeArmy.Сavalry)
        {
            prefabSquad = _prefabSquadCavalry;
        }
        else if (typeArmy == TypeArmy.Artillery)
        {
            prefabSquad = _prefabSquadArtillery;
        }
        return prefabSquad;
    }
}

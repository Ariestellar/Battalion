using SquadParameters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SquadFactory 
{
    GameObject CreateSquad(GameObject squadUI, string name, Vector3 currentPosition, TypeArmy typeArmy);
}

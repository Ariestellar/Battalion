using SquadParameters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Squad
{
    void Init(SquadData squadData);

    void Select(bool value);

    void ReduceSquadSize(float amountDamage);
}

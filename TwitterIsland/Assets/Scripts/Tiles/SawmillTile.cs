using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawmillTile : BaseTile
{

    public int woodPerTree = 2;
    [Tooltip("How many trees this building will cut down at the end of this player's play")]
    public int treesPerTurn = 1;

    public override string GetPrefabName()
    {
        return "Sawmill";
    }

    public override void ProcessEndOfTurn()
    {

    }

}

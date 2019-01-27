using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HutTile : BaseTile
{

    private void Awake()
    {
        isBuilding = true;
    }

    public override string GetPrefabName()
    {
        return "Hut";
    }
}

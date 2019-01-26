using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTile : BaseTile
{
    public override string GetPrefabName()
    {
        return "Water";
    }

    public override string ToJson()
    {
        return base.ToJson();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HutAction : TileAction
{
    public override void Setup()
    {
        m_points = 100;
        type = ActionType.TILE_ACTION;
        conditionals.Add(() => { return GameController.worldResources["wood"] > 0; });
    }

    public override void Perform(BaseTile onTile)
    {
        GameController.instance.m_PlayerScore += m_points;
        GameController.worldResources["wood"] -= 1;
        GameController.instance.ReplaceTile(onTile, "Huts");
    }
}

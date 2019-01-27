using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopTreeAction : TileAction
{
    public override void Setup()
    {
        m_points = 50;
        type = ActionType.TILE_ACTION;
        conditionals.Add(() => { return GameController.instance.GetTreeCount() > 0; });
    }

    public override void Perform(BaseTile onTile)
    {
        GameController.instance.m_PlayerScore += m_points;
        ((TreesTile)onTile).Cut();
    }
}

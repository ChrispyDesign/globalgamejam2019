using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantTreeAction : TileAction
{
    public override void Setup()
    {
        m_points = 10;
        type = ActionType.TILE_ACTION;

        affectedTypes.Add(typeof(GrassTile));
    }

    public override void Perform(BaseTile onTile)
    {
        GameController.instance.m_PlayerScore += m_points;
        GameController.instance.ReplaceTile(onTile, "Forest");
    }
}

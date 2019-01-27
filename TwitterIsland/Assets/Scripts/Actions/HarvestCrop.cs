﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestCrop : TileAction
{
    public override void Setup()
    {
        m_points = 30;
        type = ActionType.TILE_ACTION;
        conditionals.Add(() => { return GameController.instance.GetCropCount() > 0; });
    }

    public override void Perform(BaseTile onTile)
    {
        GameController.instance.m_PlayerScore += m_points;
        GameController.instance.ReplaceTile(onTile, "Grass");
    }
}

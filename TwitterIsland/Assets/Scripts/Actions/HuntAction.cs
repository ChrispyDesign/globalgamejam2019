using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntAction : TileAction
{
    public override void Setup()
    {
        m_points = 30;
        conditionals.Add(() => { return GameController.worldValues["animals"] > 0; });
    }

    public override void Perform(BaseTile onTile)
    {
        SoundManager.instance.Play("Action_Hunt");

        GameController.instance.m_PlayerScore += m_points;

        // give food
        GameController.worldValues["food"] += GameController.instance.m_fFoodValue;

        // take away animals
        GameController.worldValues["animals"] -= GameController.instance.m_fAnimalHealthValue;
    }

}

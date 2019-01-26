using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntAction : TileAction
{

    public override void Setup()
    {
        //conditionals.Add(() => { return GameController.worldValues["animals"] > GameController.instance.minimumAnimalsToHunt; });
    }

    public override void Perform(BaseTile onTile)
    {
        // give food
        GameController.worldValues["food"] += 999999;
        // take away animals
        GameController.worldValues["animals"] -= 999999;
    }

}

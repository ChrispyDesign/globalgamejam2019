//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class HuntAction : TileAction
//{

//    public override void Setup()
//    {
//        conditionals.Add(() => { return GameController.worldValues["animals"] > GameController.instance.m_fAnimalHealthValue; });
//    }

//    public override void Perform(BaseTile onTile)
//    {
//        SoundManager.instance.Play("Action_Hunt");

//        // give food
//        GameController.worldValues["food"] += GameController.instance.m_fFoodValue;

//        // take away animals
//        GameController.worldValues["animals"] -= GameController.instance.m_fAnimalHealthValue;
//    }

//}

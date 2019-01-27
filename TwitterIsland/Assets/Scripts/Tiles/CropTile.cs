using System.Collections.Generic;
using UnityEngine;

public class CropTile : BaseTile
{

    public int growthState = 0;
    public List<float> growthHeights;
    public Transform cropPlants;

    public int growthProgress = 0;

    public bool CanBeCut()
    {
        return growthState == 2;
    }

    public override void ProcessEndOfTurn()
    {
        base.ProcessEndOfTurn();
        growthProgress++;
        if (growthProgress >= GameController.instance.m_HowManyTurnstoGrowCrop)
            Grow();
    }

    public void Grow()
    {
        if (growthState < 2)
            growthState++;
        UpdatePlants();
    }

    public void Cut(bool getFood = true)
    {
        GameController.instance.ReplaceTile(this, "Grass");
    }

    public void UpdatePlants()
    {
        growthState = Mathf.Min(growthHeights.Count - 1, growthState);
        growthState = Mathf.Max(0, growthState);

        Vector3 pos = cropPlants.localPosition;
        pos.y = growthHeights[growthState];
        cropPlants.localPosition = pos;
    }

    public override string GetPrefabName()
    {
        return "Crop";
    }
    public override string ToJson()
    {
        return base.JsonStart() +
            ", \"state\":" + growthState +
            ", \"growth\":" + growthProgress +
            base.JsonEnd();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HutTile : BaseTile
{

    public GameObject hut1;
    public GameObject hut2;
    public GameObject hut3;

    private void Awake()
    {
        isBuilding = true;
    }

    public bool CanBuild()
    {
        return BuildingCount() < 3;
    }

    public override int BuildingCount()
    {
        int result = 0;
        if (hut1.activeSelf)
            result++;
        if (hut2.activeSelf)
            result++;
        if (hut3.activeSelf)
            result++;
        return result;
    }

    public void SetHutCount(int i)
    {
        hut1.SetActive(i > 0);
        hut2.SetActive(i > 1);
        hut3.SetActive(i > 2);
    }

    public override string GetPrefabName()
    {
        return "Hut";
    }
    public override string ToJson()
    {
        return base.JsonStart() +
            ", \"huts\":" + BuildingCount() +
            base.JsonEnd();
    }

}

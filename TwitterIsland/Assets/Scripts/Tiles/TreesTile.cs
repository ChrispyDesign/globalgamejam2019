using System.Collections.Generic;
using UnityEngine;

public class TreesTile : BaseTile
{

    public List<GameObject> cutPrefabs;
    public List<GameObject> growingPrefabs;
    public List<GameObject> grownPrefabs;

    public List<GameObject> group1;
    public List<GameObject> group2;
    public List<GameObject> group3;

    public int growth1 = 1;
    public int growth2 = 1;
    public int growth3 = 1;

    public int growthTurns = 0;

    private void Awake()
    {
        UpdateTrees();
    }

    public void Cut(bool applyWood = true)
    {
        if (!CanBeCut())
            return;
        if (growth1 == 2)
            growth1 = 0;
        else if (growth2 == 2)
            growth2 = 0;
        else if (growth3 == 3)
            growth3
                 = 0;
        UpdateTrees();
        // add wood
        if (applyWood)
            GameController.worldResources["wood"]++;

        if(!CanBeCut())
            GameController.instance.ReplaceTile(this, "Grass");
    }

    public bool CanBeCut()
    {
        if (growth1 == 2 || growth2 == 2 || growth3 == 2)
            return true;
        return false;
    }

    public int GetTreeCount()
    {
        int result = 0;

        if (growth1 == 2)
            result++;
        if (growth2 == 2)
            result++;
        if (growth3 == 2)
            result++;

        return result;
    }

    public void UpdateTrees()
    {
        SetTreesGrowth(group1, GetPrefabs(growth1));
        SetTreesGrowth(group2, GetPrefabs(growth2));
        SetTreesGrowth(group3, GetPrefabs(growth3));
    }

    public override void ProcessEndOfTurn()
    {
        growthTurns++;

        if(growthTurns >= GameController.instance.m_HowManyTurnstoGrowTree)
        {
            growthTurns = 0;
            growth1 = Mathf.Min(2, growth1 + 1);
            growth2 = Mathf.Min(2, growth2 + 1);
            growth3 = Mathf.Min(2, growth3 + 1);
            UpdateTrees();
        }
    }

    private void SetTreesGrowth(List<GameObject> positions, List<GameObject> prefabs)
    {
        // clear trees
        foreach (var go in positions)
            foreach (Transform t in go.transform)
                Destroy(t.gameObject);

        foreach (var go in positions)
        {
            GameObject tospawn = prefabs[Random.Range(0, prefabs.Count)];
            Instantiate(tospawn, go.transform);
        }
    }

    private void ClearTree(GameObject go)
    {
        foreach (Transform t in go.transform)
            Destroy(t.gameObject);
    }

    public override string GetPrefabName()
    {
        return "Forest";
    }

    public List<GameObject> GetPrefabs(int growth)
    {
        if (growth == 0)
            return cutPrefabs;
        if (growth == 1)
            return growingPrefabs;
        return grownPrefabs;
    }

    public override string ToJson()
    {
        return base.JsonStart() +
            ", \"g1\":" + growth1 +
            ", \"g2\":" + growth2 +
            ", \"g3\":" + growth3 +
            ", \"growth\":" + growthTurns +
            base.JsonEnd();
    }

}

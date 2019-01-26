using System.Collections.Generic;
using UnityEngine;

public class GrassTile : BaseTile
{
    //public List<GameObject> randomDecorations;

    private void Start()
    {
        //int decorationCount = Random.Range(1, 3);
        //for (int i = 0; i < decorationCount; ++i)
        //{
        //    GameObject newDecoration = Instantiate(randomDecorations[Random.Range(0, randomDecorations.Count)], decorationContainer.transform);
        //    var pos = Vector3.one * Random.Range(0.0f, 5.0f);
        //    pos.y = 0.0f;
        //    newDecoration.transform.localPosition = pos;
        //}
    }

    public override string ToJson()
    {
        return base.ToJson();
    }

    public override string GetPrefabName()
    {
        return "Grass";
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class GameController : MonoBehaviour
{

    public static GameController instance = null;

    public static Dictionary<string, float> worldValues;

    public List<BaseTile> allTiles;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        var tiles = GameObject.FindObjectsOfType<BaseTile>();
        foreach(var t in tiles)
            allTiles.Add((BaseTile)t);
    }

    private void Update()
    {
    }

    public string ToJson()
    {
        Dictionary<string, object> boop = new Dictionary<string, object>();
        boop.Add("worldValues", worldValues);
        string finalWorld = JsonConvert.SerializeObject(boop);
        return finalWorld;
    }

}

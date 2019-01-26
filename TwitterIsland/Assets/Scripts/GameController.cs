using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class GameController : MonoBehaviour
{

    public int actionPoints = 5;
    public static GameController instance = null;

    public static Dictionary<string, float> worldValues;
    public static Dictionary<string, int> worldResources;

    public List<BaseTile> allTiles;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GetAllTiles();
        TilesToJson();
    }

    public void GetAllTiles()
    {
        allTiles.Clear();
        var tiles = GameObject.FindObjectsOfType<BaseTile>();
        foreach(var t in tiles)
            allTiles.Add((BaseTile)t);
    }

    public void EndGame()
    {

    }

    public string ToJson()
    {
        Dictionary<string, object> boop = new Dictionary<string, object>();
        boop.Add("worldValues", worldValues);
        boop.Add("worldResources", worldResources);
        string finalWorld = JsonConvert.SerializeObject(boop);
        return finalWorld;
    }

    public string TilesToJson()
    {
        string s = "\"tiles\":[";
        foreach(var t in allTiles)
            s += t.ToJson() + ",";
        s += "]";

        Debug.Log(s);
        return s;
    }

}

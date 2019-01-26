using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public BaseTile tilePrefab;

    public int actionPoints = 5;
    public static GameController instance = null;

    public static Dictionary<string, float> worldValues;
    public static Dictionary<string, int> worldResources;

    public List<BaseTile> allTiles;
    public List<TileAction> allActions;

    [Header("Numbers for Chris")]
    public float foodstuff = 10.0f;

    private void Awake()
    {
        SetupActions();
        instance = this;
    }

    private void Start()
    {
        GetAllTiles();
        TilesToJson();
        LoadTilesFromJson(TilesToJson());
    }

    private void Update()
    {
    }

    public void GetAllTiles()
    {
        allTiles.Clear();
        var tiles = GameObject.FindObjectsOfType<BaseTile>();
        foreach (var t in tiles)
            allTiles.Add((BaseTile)t);
    }

    public void EndGame()
    {
        // make changes to parameters here :)
        // "humans", "food", "atmosphere", "soil", "animals", "buildings","crops","trees"
        worldValues["humans"] = 69.0f;

        bool didWorldEnd = false;
        if (!didWorldEnd)
        {
        // this sends ALL the current world info to the server :)
            GetAllTiles();
            ServerCommunication.instance.SendWorldState();
        }
        else
        {
            // erase everything from the server so we can re-generate it next time :)
            ServerCommunication.instance.ResetWorld();
        }
    }

    public string ToJson()
    {
        Dictionary<string, object> boop = new Dictionary<string, object>();
        boop.Add("worldValues", worldValues);
        boop.Add("worldResources", worldResources);
        boop.Add("tiles", TileListFromJson(TilesToJson()));
        string finalWorld = JsonConvert.SerializeObject(boop);
        finalWorld = finalWorld.Replace("\\\"", "\"");
        Debug.Log(finalWorld);
        return finalWorld;
    }

    public string TilesToJson()
    {
        string s = "[";
        foreach (var t in allTiles)
            s += t.ToJson() + ",";
        s += "]";

        Debug.Log(s);
        return s;
    }

    public List<Dictionary<string, object>> TileListFromJson(string json)
    {
        return JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json);
    }

    public void LoadTilesFromJson(string json)
    {
        var tiles = TileListFromJson(json);

        // remove all existing tiles
        GetAllTiles();
        foreach (var t in allTiles)
            Destroy(t.gameObject);

        foreach (var tile in tiles)
        {
            BaseTile newTile = Instantiate(tilePrefab);

            // get position
            Vector3 tilePos = SerializationHelper.JsonToVector(tile["pos"].ToString());
            newTile.transform.position = tilePos;
        }
        GetAllTiles();
    }

    public List<BaseTile> GetTiles(System.Type type)
    {
        List<BaseTile> result = new List<BaseTile>();

        foreach(var t in allTiles)
        {
            if (t.GetType() == type)
                result.Add(t);
        }

        return result;
    }

    void SetupActions()
    {
        allActions.Add(new HuntAction());

        foreach(var a in allActions)
            a.Setup();
    }

}

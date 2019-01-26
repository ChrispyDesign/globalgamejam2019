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

    [Header("Increament n Decrement Values")]
    public float m_fHumanHealthValue;
    public float m_fFoodValue;
    public float m_fAmountToCutEveryTurnFood;
    public float m_fAtmosphereHealthValue;
    public float m_fSoilHealthValue;
    public float m_fAnimalHealthValue;
    public int m_nBuildingsHealthValue;
    public int m_nCropHealthValue;
    public int m_nTreesHealthValue;
    public int m_nWoodValue;
    public int m_nStoneValue;
    public int m_HowManyTurnstoGrowTree;
    public int m_HowManyTurnstoGrowCrop;

    [Header("Percentage Range")]
    public float m_fHigh;
    public float m_fMiddle;
    public float m_fLow;

    public List<BaseTile> allTiles;
    public List<TileAction> allActions;

    [Header("Friggen tiles dude")]
    public List<BaseTile> tilePrefabs;

    private void Awake()
    {
        SetupActions();
        instance = this;
    }

    private void Start()
    {
        //GetAllTiles();
        //TilesToJson();
        //LoadTilesFromJson(TilesToJson());
    }

    private void Update()
    {
    }

    public void GetAllTiles()
    {
        allTiles.Clear();
        var tiles = FindObjectsOfType<BaseTile>();
        foreach (var t in tiles)
            allTiles.Add(t);
    }

    public void EndGame()
    {
        // make changes to parameters here :)
        // "humans", "food", "atmosphere", "soil", "animals", "buildings","crops","trees"

        bool didWorldEnd = false;

        //Health stuff
        {
            if (worldValues["food"] > m_fHigh)
            {
                worldValues["humans"] += worldValues["food"] * m_fHumanHealthValue;
                worldValues["food"] += worldValues["humans"] * m_fFoodValue;
            }
            else if(worldValues["food"] > m_fLow)
            {
                worldValues["humans"] -= worldValues["food"] * m_fHumanHealthValue;
                worldValues["food"] += worldValues["humans"] * m_fFoodValue;
            }
            else if(worldValues["food"] > 0.0f)
            {
                didWorldEnd = true;
            }
            else
            {
                worldValues["food"] += worldValues["humans"] * 0.1f;
            }
        }

        //Food stuff
        {
            worldValues["food"] -= m_fAmountToCutEveryTurnFood;
        }

        //Atmosphere stuff
        {
            //Change later
            //if(GetTiles(typeof(TreesTile)).Count > m_fHigh && GetTiles(typeof(CropTile)).Count > m_fHigh)
            //    worldValues["atmosphere"] += 

            //Implement crops and trees

            if (worldValues["animals"] > m_fHigh)
                worldValues["atmosphere"] -= m_fAtmosphereHealthValue;

            //GetTreeCount

        }

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
            BaseTile newTile = null;

            if (tile.ContainsKey("type"))
            {
                string typeName = tile["type"].ToString();
                // grab any variation of this tile
                BaseTile t = GetTileVariation(typeName);
                if (t != null)
                    newTile = Instantiate(t);
            }

            if (newTile == null)
                newTile = Instantiate(GetTileVariation("Grass"));

            // get position
            Vector3 tilePos = SerializationHelper.JsonToVector(tile["pos"].ToString());
            newTile.transform.position = tilePos;
        }
        GetAllTiles();
    }

    public List<BaseTile> GetTiles(System.Type type)
    {
        List<BaseTile> result = new List<BaseTile>();

        foreach (var t in allTiles)
        {
            if (t.GetType() == type)
                result.Add(t);
        }

        return result;
    }

    // fuck you bgabeg
    public int GetTreeCount()
    {
        int result = 0;
        var treez = GetTiles(typeof(TreesTile));
        foreach(var o in treez)
            ((TreesTile)o).GetTreeCount();
        return result;
    }

    void SetupActions()
    {
        allActions = new List<TileAction>();
        allActions.Add(new HuntAction());

        foreach (var a in allActions)
            a.Setup();
    }

    BaseTile GetTileVariation(string tileName)
    {
        int count = GetTileVariationCount(tileName);
        string name = tileName + Random.Range(0, count);
        foreach (var t in tilePrefabs)
        {
            if (t.name == name)
                return t;
        }
        Debug.Log("NO tile variation of tile " + tileName);
        return null;
    }

    int GetTileVariationCount(string tileName)
    {
        for (int i = 0; i < 999; ++i)
        {
            bool wasFound = false;
            foreach (var t in tilePrefabs)
            {
                if (t.name == (tileName + i))
                    wasFound = true;
            }
            if (!wasFound)
                return i;
        }
        return 0;
    }

    public void ReplaceTile(BaseTile tile, string name)
    {
        var newVar = GetTileVariation(name);
        if (newVar == null)
            return;

        Vector3 pos = tile.transform.position;
        Destroy(tile.gameObject);
        Instantiate(newVar, pos, Quaternion.identity);
    }

}

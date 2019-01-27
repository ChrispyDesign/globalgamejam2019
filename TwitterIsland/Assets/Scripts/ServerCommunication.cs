using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerCommunication : MonoBehaviour
{

    public static ServerCommunication instance = null;

    public string ourId = "";

    public string currentIdUrl = "http://ggj.fsh.zone/getcode";
    public string worldStateUrl = "http://ggj.fsh.zone/getstate";
    public string worldFinishedUrl = "http://ggj.fsh.zone/update/";
    public string twitterHandleUrl = "http://ggj.fsh.zone/gethandle/";

    // "humans", "food", "atmosphere", "soil", "animals", "buildings","crops","trees"
    [Header("Default Values for WorldState OWO")]
    public float m_fHumanHealth;
    public float m_fFood;
    public float m_fAtmosphereHealth;
    public float m_fSoilHealth;
    public float m_fAnimalHealth;
    public int m_nBuildingsHealth;
    public int m_nCropHealth;
    public int m_nTreesHealth;
    public int m_nWood;
    public int m_nStone;

    private void Awake()
    {
        instance = this;
        StartCoroutine(MakeIdCurrent());
    }

    private void Start()
    {
        instance = this;
        StartCoroutine(GetWorldState());
    }

    IEnumerator MakeIdCurrent()
    {
        using (WWW www = new WWW(currentIdUrl))
        {
            yield return www;
            ourId = www.text;
        }
    }

    IEnumerator GetWorldState()
    {
        //WebClient wc = new WebClient();

        //Stream data = wc.OpenRead(worldStateUrl);
        //StreamReader reader = new StreamReader(data);
        //string s = reader.ReadToEnd();
        //data.Close();
        //reader.Close();

        string s = "";
        using (WWW www = new WWW(worldStateUrl))
        {
            yield return www;
            s = www.text;
        }

        var list = JsonConvert.DeserializeObject<Dictionary<string, object>>(s);

        if (list == null || !list.ContainsKey("worldValues"))
        {
            // "humans", "food", "atmosphere", "soil", "animals", "buildings","crops","trees"
            // generate a new world
            // stuff
            GameController.worldValues = new Dictionary<string, float>();
            GameController.worldResources = new Dictionary<string, int>();

            GameController.worldValues.Add("humans", m_fHumanHealth);
            GameController.worldValues.Add("food", m_fFood);
            GameController.worldValues.Add("atmosphere", m_fAtmosphereHealth);
            GameController.worldValues.Add("soil", m_fSoilHealth);
            GameController.worldValues.Add("animals", m_fAnimalHealth);

            GameController.worldResources.Add("wood", m_nWood);
            GameController.worldResources.Add("stone", m_nStone);

            // generate world
            GameController.instance.Gener8World();

            yield break;
        }

        // grab main world parameters from json
        if (list.ContainsKey("worldValues"))
        {
            // need to convert this into a new dictionary
            var values = JsonConvert.DeserializeObject<Dictionary<string, float>>(list["worldValues"].ToString());
            GameController.worldValues = values;
        }
        // grab resources too
        if (list.ContainsKey("worldResources"))
        {
            // need to convert this into a new dictionary
            var values = JsonConvert.DeserializeObject<Dictionary<string, int>>(list["worldResources"].ToString());
            GameController.worldResources = values;
        }
        // and tiles
        if (list.ContainsKey("tiles"))
        {
            GameController.instance.LoadTilesFromJson(list["tiles"].ToString());
        }
        if(list.ContainsKey("riverPrefab"))
        {
            GameController.instance.MakeRiver(int.Parse(list["riverPrefab"].ToString()));
        }
    }

    public void SendWorldState()
    {
        StartCoroutine(_SendWorldState());
    }

    IEnumerator _SendWorldState()
    {
        GameController.instance.GetAllTiles();
        string ourUrl = worldFinishedUrl + "?id=" + ourId + "&data=" + GameController.instance.ToJson();
        using (WWW www = new WWW(ourUrl))
        {
            yield return www;
        }
    }

    public void ResetWorld()
    {
        StartCoroutine(_ResetWorld());
    }

    IEnumerator _ResetWorld()
    {
        string ourUrl = worldFinishedUrl + "?id=" + ourId + "&data=";
        using(WWW www = new WWW(ourUrl))
        {
            yield return www;
        }
    }

    public void SetHandle()
    {
        StartCoroutine(_SetHandle());
    }

    IEnumerator _SetHandle()
    {
        string ourUrl = twitterHandleUrl;
        using(WWW www = new WWW(ourUrl))
        {
            yield return www;
            GameController.ourHandle = www.text;
        }
    }

}

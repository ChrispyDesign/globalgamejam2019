using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

public class ServerCommunication : MonoBehaviour
{

    public string ourId = "";

    public string currentIdUrl = "http://ggj.fsh.zone/getcode";
    public string worldStateUrl = "http://ggj.fsh.zone/getstate";
    public string worldFinishedUrl = "http://ggj.fsh.zone/update/";

    private void Awake()
    {
        MakeIdCurrent();
        GetWorldState();
    }

    void MakeIdCurrent()
    {
        WebClient wc = new WebClient();

        Stream data = wc.OpenRead(currentIdUrl);
        StreamReader reader = new StreamReader(data);
        ourId = reader.ReadToEnd();
        data.Close();
        reader.Close();
    }

    void GetWorldState()
    {
        WebClient wc = new WebClient();

        Stream data = wc.OpenRead(worldStateUrl);
        StreamReader reader = new StreamReader(data);
        string s = reader.ReadToEnd();
        data.Close();
        reader.Close();

        var list = JsonConvert.DeserializeObject<Dictionary<string, object>>(s);

        // grab main world parameters from json
        if (list.ContainsKey("worldValues"))
        {
            // need to convert this into a new dictionary
            var values = JsonConvert.DeserializeObject<Dictionary<string, float>>(list["worldValues"].ToString());
            GameController.worldValues = values;
        }
    }

    public void SendWorldState()
    {
        WebClient wc = new WebClient();

        string ourUrl = worldFinishedUrl + "?id=" + ourId + "&data=" + GameController.instance.ToJson();

        Stream data = wc.OpenRead(ourUrl);
        StreamReader reader = new StreamReader(data);
        //string s = reader.ReadToEnd();
        data.Close();
        reader.Close();
    }

}

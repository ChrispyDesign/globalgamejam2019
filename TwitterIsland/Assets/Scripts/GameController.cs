using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class GameController : MonoBehaviour
{

    public static GameController instance = null;

    public static Dictionary<string, float> worldValues;
    public Text airText;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (worldValues.ContainsKey("air"))
            airText.text = string.Format("air: {0:0.0}", worldValues["air"]);
    }

    public void ChangeAir(float amt)
    {
        if (worldValues.ContainsKey("air"))
            worldValues["air"] += amt;

        ToJson();
    }

    public string ToJson()
    {
        Dictionary<string, object> boop = new Dictionary<string, object>();
        boop.Add("worldValues", worldValues);
        string finalWorld = JsonConvert.SerializeObject(boop);
        Debug.Log(finalWorld);
        return finalWorld;
    }

    string CleanJson(string s)
    {
        return s.Replace("\\\"", "\"");
    }

}

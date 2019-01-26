using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class SerializationHelper { 

    public static string VectorToJson(Vector3 vec)
    {
        float[] arr = { vec.x, (int)vec.y, vec.z };
        return JsonConvert.SerializeObject(arr);
    }

    public static Vector3 JsonToVector(string json)
    {
        float[] arr = JsonConvert.DeserializeObject<float[]>(json);
        return new Vector3(arr[0], (int)arr[1], arr[2]);
    }

}

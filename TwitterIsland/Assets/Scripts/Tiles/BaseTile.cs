using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTile : MonoBehaviour {

    public List<BaseTile> GetAdjacentTiles(int casts = 12)
    {
        List<BaseTile> result = new List<BaseTile>();

        float perStep = (2.0f * Mathf.PI) / 2.0f;
        for(float f = 0; f < 2.0f * Mathf.PI; f += perStep)
        {
            // angle is f, turn that into a vector
            //Vector3 direction = new Vector3()/*;*/
            // no
        }

        return result;
    }

}

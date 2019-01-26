using System.Collections.Generic;
using UnityEngine;

public class BaseTile : MonoBehaviour
{
    private void Start()
    {
    }

    public List<BaseTile> GetAdjacentTiles(int casts = 12)
    {
        List<BaseTile> result = new List<BaseTile>();

        float perStep = (2.0f * Mathf.PI) / casts;
        for (float f = 0; f < 2.0f * Mathf.PI; f += perStep)
        {
            // angle is f, turn that into a vector
            Vector3 direction = new Vector3();
            direction.x = Mathf.Sin(f);
            direction.z = Mathf.Cos(f);

            var hits = Physics.RaycastAll(transform.position, direction);
            float closestDist = Mathf.Infinity;
            BaseTile closestTile = null;
            foreach(var hit in hits)
            {
                if(hit.distance < closestDist && hit.collider.gameObject != this.gameObject)
                {
                    closestDist = hit.distance;
                    closestTile = hit.collider.gameObject.GetComponent<BaseTile>();
                }
            }
            if (closestTile != null && !result.Contains(closestTile))
                result.Add(closestTile);
        }

        return result;
    }

}

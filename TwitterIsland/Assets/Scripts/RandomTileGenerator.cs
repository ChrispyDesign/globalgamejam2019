using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTileGenerator : MonoBehaviour {

    public List<Transform> spawnPoints;
    public List<GameObject> tiles;
    public List<GameObject> pieces;

    private void Start()
    {
        Generate();
    }

    public void Generate()
    {
        //if (pieces != null)
        //{
        //    foreach (var piece in pieces)
        //    {
        //        Destroy(piece);
        //    }

        //    pieces.Clear();
        //}

        foreach (var points in spawnPoints)
        {

            int tileNumber = Random.Range(0, tiles.Count);
            GameObject piece = Instantiate(tiles[tileNumber], points.position, points.rotation);
            pieces.Add(piece.gameObject);

        }

    }
}
﻿using System.Collections.Generic;
using UnityEngine;

public class RandomTileGenerator : MonoBehaviour
{

    public List<Transform> spawnPoints;
    public List<GameObject> tiles;
    public List<string> tileNames;
    public List<GameObject> pieces;

    private void Start()
    {
        // don't generate on start - the game controller will handle it if we need to generate
       // Generate();
    }

    public void Generate()
    {
        if (pieces != null)
        {
            foreach (var piece in pieces)
            {
                Destroy(piece);
            }

            pieces.Clear();
        }

        foreach (var points in spawnPoints)
        {
            RaycastHit hit;
            if (!Physics.Raycast(points.position + Vector3.up*2.0f, Vector3.down, out hit))
            {
                var toInstantiate = GameController.instance.GetTileVariation(tileNames[Random.Range(0, tileNames.Count)]);
                BaseTile piece = Instantiate(toInstantiate, points.position, points.rotation);
                pieces.Add(piece.gameObject);
            }
            else
            {
                Debug.Log("Skipping spawning tile due to collision");
            }
        }

    }
}
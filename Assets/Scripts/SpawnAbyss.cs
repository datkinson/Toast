using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnAbyss : MonoBehaviour
{
    [SerializeField] public Tile abyssTile;
    [SerializeField] public Tilemap tileMap;
    void Start()
    {
        for(int i = tileMap.cellBounds.position.x; i < (tileMap.cellBounds.size.x / 2); i++){
            // Debug.Log(String.Format("x: {0}, y: {1}", i, tileMap.cellBounds.position.y));
            tileMap.SetTile(new Vector3Int(i, tileMap.cellBounds.position.y, 0), abyssTile);
        }
    }

    void Update()
    {
    }
}

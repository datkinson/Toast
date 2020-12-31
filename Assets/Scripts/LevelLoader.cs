using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private float level = 1;
    [SerializeField] private int unitDivider = 800;
    private int environmentProperties = 4;
    private int melonProperties = 2;
    [SerializeField] public Tile groundTile;
    [SerializeField] public Tile groundTile2;
    [SerializeField] public Tile groundTile3;
    [SerializeField] public Tile groundTile4;
    [SerializeField] public Tile groundTile5;
    [SerializeField] public Tilemap map;
    void Start()
    {
        List<Tile> tiles = new List<Tile>();
        tiles.Add(groundTile);
        tiles.Add(groundTile2);
        tiles.Add(groundTile3);
        tiles.Add(groundTile4);
        tiles.Add(groundTile5);
        TextAsset levelData=(TextAsset)Resources.Load("lvl" + level);
        string[] levelComponents = levelData.ToString().Split(';');
        var player = new Dictionary<string, float>(){
            {"x", float.Parse(levelComponents[0])},
            {"y", float.Parse(levelComponents[1])}
        };
        int environmentItems = int.Parse(levelComponents[2]);
        int itterator = 2;
        List<Dictionary<string,int>> environments = new List<Dictionary<string,int>>();
        for (int i = 0; i < environmentItems; i++)
        {
            itterator++;
            var environmentItem = new Dictionary<string, int>(){};
            environmentItem["x"] = int.Parse(levelComponents[itterator]);
            itterator++;
            environmentItem["y"] = int.Parse(levelComponents[itterator]);
            itterator++;
            environmentItem["width"] = int.Parse(levelComponents[itterator]);
            itterator++;
            environmentItem["height"] = int.Parse(levelComponents[itterator]);
            environments.Add(environmentItem);
        }
        itterator++;
        int melonCount = int.Parse(levelComponents[itterator]);
        List<Dictionary<string,int>> melons = new List<Dictionary<string,int>>();
        for (int i = 0; i < melonCount; i++)
        {
            var melonItem = new Dictionary<string, int>(){};
            itterator++;
            melonItem["x"] = int.Parse(levelComponents[itterator]);
            itterator++;
            melonItem["y"] = int.Parse(levelComponents[itterator]);
            melons.Add(melonItem);
        }
        int tileToUse = 0;
        foreach (var item in environments)
        {
            int x = (item["x"] / unitDivider) - (map.cellBounds.size.x / 2);
            int y = (item["y"] / unitDivider) - (map.cellBounds.size.y / 2);
            int width = (item["x"] + item["width"]) / unitDivider;
            int height = (item["y"] + item["height"]) / unitDivider;
            // int x = item["x"];
            // int y = item["y"];
            // int width = (item["x"] + item["width"]);
            // int height = (item["y"] + item["height"]);
            Debug.Log(String.Format("x, y, width, height\n {0}, {1}, {2}, {3}", x, y, width, height));
            for (int i = x; i < x+width; i++)
            {
                for (int j = y; j < y+height; j++)
                {
                    map.SetTile(new Vector3Int(i, j, 0), tiles[tileToUse]);
                }
            }
            tileToUse++;
        }
    }

    void Update()
    {
        
    }
}

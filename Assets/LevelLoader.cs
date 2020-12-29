using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private float level = 1;
    [SerializeField] private int unitMultiplier = 200;
    private int environmentProperties = 4;
    private int melonProperties = 2;
    [SerializeField] public Tile groundTile;
    [SerializeField] public Tilemap map;
    void Start()
    {
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
        foreach (var item in environments)
        {
            int x = item["x"] / unitMultiplier;
            var y = item["y"] / unitMultiplier;
            var width = (item["x"] + item["width"]) / unitMultiplier;
            var height = (item["y"] + item["height"]) / unitMultiplier;
            Debug.Log("x: " + x);
            Debug.Log("y: " + y);
            Debug.Log("width: " + width);
            Debug.Log("height: " + height);
            // map.BoxFill(
            //     Vector3Int.zero,
            //     groundTile,
            //     x,
            //     y,
            //     width,
            //     height
            // );
            map.BoxFill(
                Vector3Int.zero,
                groundTile,
                1,
                0,
                15,
                4
            );
        }
    }

    void Update()
    {
        
    }
}

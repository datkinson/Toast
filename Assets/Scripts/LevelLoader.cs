using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private float level = 1;
    [SerializeField] private int unitDivider = 300;
    private int environmentProperties = 4;
    private int melonProperties = 2;
    private int itemYOffset = 4;
    private int playerWorldYOffset = 8;
    [SerializeField] public Tile groundTile;
    [SerializeField] public Tilemap map;
    [SerializeField] public GameObject playerObject;
    [SerializeField] public GameObject melonObject;
    void Start()
    {
        int xOffset = map.cellBounds.size.x / 2;
        int yOffset = map.cellBounds.size.y / 2;
        TextAsset levelData=(TextAsset)Resources.Load("lvl" + level);
        string[] levelComponents = levelData.ToString().Split(';');
        var player = new Dictionary<string, int>(){};
        player["x"] = Convert.ToInt32(Math.Round(Convert.ToDouble(levelComponents[0]))) / unitDivider;
        player["y"] = Convert.ToInt32(Math.Round(Convert.ToDouble(levelComponents[1]))) / unitDivider;

        int playerXOffset = player["x"] - xOffset;
        int playerYOffset = player["y"] - yOffset;
        // playerObject.transform.Translate((playerXOffset - playerObject.transform.position.x), (playerYOffset - playerObject.transform.position.y), 0);
        Instantiate(playerObject, new Vector3(player["x"] - xOffset, (player["y"] - yOffset) + playerWorldYOffset, 0), Quaternion.identity);
        int environmentItems = int.Parse(levelComponents[2]);
        int itterator = 2;
        List<Dictionary<string,int>> environments = new List<Dictionary<string,int>>();
        for (int i = 0; i < environmentItems; i++)
        {
            itterator++;
            var environmentItem = new Dictionary<string, int>(){};
            // environmentItem["x"] = int.Parse(levelComponents[itterator]);
            environmentItem["x"] = Convert.ToInt32(Math.Round(Convert.ToDouble(levelComponents[itterator]))) / unitDivider;
            itterator++;
            // environmentItem["y"] = int.Parse(levelComponents[itterator]);
            environmentItem["y"] = Convert.ToInt32(Math.Round(Convert.ToDouble(levelComponents[itterator]))) / unitDivider;
            itterator++;
            // environmentItem["width"] = int.Parse(levelComponents[itterator]);
            environmentItem["width"] = Convert.ToInt32(Math.Round(Convert.ToDouble(levelComponents[itterator]))) / unitDivider;
            if(environmentItem["width"] < 1){
                environmentItem["width"] = 1;
            }
            itterator++;
            // environmentItem["height"] = int.Parse(levelComponents[itterator]);
            environmentItem["height"] = Convert.ToInt32(Math.Round(Convert.ToDouble(levelComponents[itterator]))) / unitDivider;
            if(environmentItem["height"] < 1){
                environmentItem["height"] = 1;
            }
            environments.Add(environmentItem);
        }
        itterator++;
        int melonCount = int.Parse(levelComponents[itterator]);
        List<Dictionary<string,int>> melons = new List<Dictionary<string,int>>();
        for (int i = 0; i < melonCount; i++)
        {
            var melonItem = new Dictionary<string, int>(){};
            itterator++;
            melonItem["x"] = int.Parse(levelComponents[itterator]) / unitDivider;
            itterator++;
            melonItem["y"] = (int.Parse(levelComponents[itterator]) / unitDivider) + itemYOffset;
            melons.Add(melonItem);
            Instantiate(melonObject, new Vector3(melonItem["x"] - xOffset, melonItem["y"] - yOffset, 0), Quaternion.identity);
        }
        int environmentItemIndex = 0;
        foreach (var item in environments)
        {
            // int x = (item["x"] / unitDivider) - xOffset;
            int x = item["x"] - xOffset;
            // int y = (item["y"] / unitDivider) - yOffset;
            int y = item["y"] - yOffset;
            // int width = (item["width"] / unitDivider);
            int width = item["width"];
            // int height = (item["height"] / unitDivider);
            int height = item["height"];
            Debug.Log("Environment Item: " + (environmentItemIndex + 1));
            Debug.Log(String.Format("x, y, width, height, x+width, y+height\n {0}, {1}, {2}, {3}, {4}, {5}", x, y, width, height, x+width, y+height));
            for (int i = x; i < x+width; i++)
            {
                for (int j = y; j < y+height; j++)
                {
                    map.SetTile(new Vector3Int(i, j, 0), groundTile);
                }
            }
            environmentItemIndex++;
        }
    }

    void Update()
    {
        
    }
}

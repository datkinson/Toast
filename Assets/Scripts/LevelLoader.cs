using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class LevelLoader : MonoBehaviour
{
    class EnvironmentItem {
        public float X;
        public float Y;
        public float Width;
        public float Height;
    }

    class Player {
        public float X;
        public float Y;
    }

    [SerializeField] private float level = 1;
    [SerializeField] private float unitDivider = 300;
    private int environmentProperties = 4;
    private int melonProperties = 2;
    private int itemYOffset = 0;
    private int playerWorldYOffset = 8;
    [SerializeField] public Tile groundTile;
    [SerializeField] public Tilemap map;
    [SerializeField] public GameObject playerObject;
    [SerializeField] public GameObject melonObject;
    [SerializeField] public GameObject enemyObject;
    [SerializeField] public GameObject wallObject;
    [SerializeField] public Vector3 pos = new Vector3(0, 0, 0);
    [SerializeField] public List<(Vector2, Vector2)> rectangles;
    void Start()
    {
        rectangles = new List<(Vector2, Vector2)>();
        GameObject parentGameObject = new GameObject();
        Canvas canvas = parentGameObject.AddComponent<Canvas>();

        var xOffset = map.cellBounds.size.x / 2;
        var yOffset = map.cellBounds.size.y / 2;
        TextAsset levelData=(TextAsset)Resources.Load("lvl" + level);
        string[] levelComponents = levelData.ToString().Split(';');
        var player = new Player {
            X = Convert.ToSingle(levelComponents[0]) / unitDivider,
            Y = Convert.ToSingle(levelComponents[1]) / unitDivider
        };

        var playerXOffset = player.X - xOffset;
        var playerYOffset = player.Y - yOffset;
        // playerObject.transform.Translate((playerXOffset - playerObject.transform.position.x), (playerYOffset - playerObject.transform.position.y), 0);
        Instantiate(playerObject, new Vector3(player.X, (-player.Y), 0), Quaternion.identity);
        var environmentItems = int.Parse(levelComponents[2]);
        var itterator = 2;
        List<EnvironmentItem> environments = new List<EnvironmentItem>();
        for (int i = 0; i < environmentItems; i++)
        {
            itterator++;

            var environmentItem = new EnvironmentItem();
            environmentItem.X = Convert.ToSingle(levelComponents[itterator]) / unitDivider;
            itterator++;

            environmentItem.Y = -Convert.ToSingle(levelComponents[itterator]) / unitDivider;
            itterator++;

            environmentItem.Width = Convert.ToSingle(levelComponents[itterator]) / unitDivider;
            //if(environmentItem.Width < 1){
            //    environmentItem.Width = 1;
            //}
            itterator++;

            environmentItem.Height = -Convert.ToSingle(levelComponents[itterator]) / unitDivider;
            //if(environmentItem.Height < 1){
            //    environmentItem.Height = 1;
            //}

            rectangles.Add((new Vector2(environmentItem.X, environmentItem.Y), new Vector2(environmentItem.Width, environmentItem.Height)));
            environments.Add(environmentItem);
            var wallInstance = Instantiate(wallObject);
            wallInstance.transform.localScale = new Vector3(environmentItem.Width, environmentItem.Height, 1);
            wallInstance.transform.position = new Vector3(environmentItem.X + (environmentItem.Width / 2), environmentItem.Y + (environmentItem.Height / 2), 0);
        }
        itterator++;
        // Get Melons
        int melonCount = int.Parse(levelComponents[itterator]);
        List<Dictionary<string,float>> melons = new List<Dictionary<string,float>>();
        for (int i = 0; i < melonCount; i++)
        {
            var melonItem = new Dictionary<string, float>(){};
            itterator++;
            melonItem["x"] = Convert.ToSingle(levelComponents[itterator]) / unitDivider;
            itterator++;
            melonItem["y"] = -(Convert.ToSingle(levelComponents[itterator]) / unitDivider) + itemYOffset;
            melons.Add(melonItem);
            var melonInstance = Instantiate(melonObject, new Vector3(melonItem["x"], melonItem["y"], 0), Quaternion.identity);
            // melonInstance.transform.position = new Vector3(melonItem["x"] + (), melonItem["y"] + ());
        }
        // Get Enemies
        List<Dictionary<string,float>> enemies = new List<Dictionary<string,float>>();
        while (itterator < (levelComponents.Length - 2))
        {
            var enemyItem = new Dictionary<string, float>(){};
            itterator++;
            int environmentKey = int.Parse(levelComponents[itterator]) - 1;
            enemyItem["x"] = (environments[environmentKey].X);
            enemyItem["y"] = -(environments[environmentKey].Y  + itemYOffset);
            itterator++;
            if(Convert.ToBoolean(levelComponents[itterator])) {
                enemies.Add(enemyItem);
                Instantiate(enemyObject, new Vector3(enemyItem["x"] - xOffset, enemyItem["y"] - yOffset, 0), Quaternion.identity);
            }
        }
        // int environmentItemIndex = 0;
        // foreach (var item in environments)
        // {
        //     // int x = (item["x"] / unitDivider) - xOffset;
        //     var x = item.X - xOffset;
        //     // int y = (item["y"] / unitDivider) - yOffset;
        //     var y = item.Y - yOffset;
        //     // int width = (item["width"] / unitDivider);
        //     var width = item.Width;
        //     // int height = (item["height"] / unitDivider);
        //     var height = item.Height;
        //     // Debug.Log("Environment Item: " + (environmentItemIndex + 1));
        //     // Debug.Log(String.Format("x, y, width, height, x+width, y+height\n {0}, {1}, {2}, {3}, {4}, {5}", x, y, width, height, x+width, y+height));
        //     for (var i = x; i < x+width; i++)
        //     {
        //         for (var j = y; j < y+height; j++)
        //         {
        //             map.SetTile(new Vector3Int((int)i, (int)j, 0), groundTile);
        //         }
        //     }
        //     environmentItemIndex++;
        // }
    }

    void Update()
    {
        
    }
    public static void Set_Height(GameObject gameObject, float height)
    {
        if (gameObject != null)
        {
            var rectTransform = gameObject.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height);
            }
        }
    }
    public static void Set_Width(GameObject gameObject, float width)
    {
        if (gameObject != null)
        {
            var rectTransform = gameObject.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
            }
        }
    }
    public void OnDrawGizmos() {
        if (rectangles == null) {
            return;
        }
        foreach (var (position, size) in rectangles) {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(position, new Vector2(position.x + size.x, position.y));
            Gizmos.DrawLine(position, new Vector2(position.x, position.y + size.y));
            Gizmos.DrawLine(new Vector2(position.x + size.x, position.y), new Vector2(position.x + size.x, position.y + size.y));
            Gizmos.DrawLine(new Vector2(position.x + size.x, position.y + size.y), new Vector2(position.x, position.y + size.y));
        }
    }
}

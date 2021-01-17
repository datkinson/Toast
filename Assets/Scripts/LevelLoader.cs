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
    [SerializeField] private Tile groundTile;
    [SerializeField] private Tilemap map;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject melonObject;
    [SerializeField] private GameObject enemyObject;
    [SerializeField] private GameObject wallObject;
    [SerializeField] private Vector3 pos = new Vector3(0, 0, 0);
    [SerializeField] private List<(Vector2, Vector2)> rectangles;

    public GameObject MelonObject { get => melonObject; set => melonObject = value; }
    public GameObject PlayerObject { get => playerObject; set => playerObject = value; }
    public GameObject EnemyObject { get => enemyObject; set => enemyObject = value; }
    public GameObject WallObject { get => wallObject; set => wallObject = value; }
    public Vector3 Pos { get => pos; set => pos = value; }
    public List<(Vector2, Vector2)> Rectangles { get => rectangles; set => rectangles = value; }
    public Tilemap Map { get => map; set => map = value; }
    public Tile GroundTile { get => groundTile; set => groundTile = value; }

    void Start()
    {
        Rectangles = new List<(Vector2, Vector2)>();
        GameObject parentGameObject = new GameObject();
        Canvas canvas = parentGameObject.AddComponent<Canvas>();

        var xOffset = Map.cellBounds.size.x / 2;
        var yOffset = Map.cellBounds.size.y / 2;
        TextAsset levelData=(TextAsset)Resources.Load("lvl" + level);
        string[] levelComponents = levelData.ToString().Split(';');
        var player = new Player {
            X = Convert.ToSingle(levelComponents[0]) / unitDivider,
            Y = Convert.ToSingle(levelComponents[1]) / unitDivider
        };

        var playerXOffset = player.X - xOffset;
        var playerYOffset = player.Y - yOffset;
        // playerObject.transform.Translate((playerXOffset - playerObject.transform.position.x), (playerYOffset - playerObject.transform.position.y), 0);
        Instantiate(PlayerObject, new Vector3(player.X, (-player.Y), 0), Quaternion.identity);
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
            itterator++;

            environmentItem.Height = -Convert.ToSingle(levelComponents[itterator]) / unitDivider;

            Rectangles.Add((new Vector2(environmentItem.X, environmentItem.Y), new Vector2(environmentItem.Width, environmentItem.Height)));
            environments.Add(environmentItem);
            var wallInstance = Instantiate(WallObject);
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
            var melonInstance = Instantiate(MelonObject, new Vector3(melonItem["x"], melonItem["y"], 0), Quaternion.identity);
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
                Instantiate(EnemyObject, new Vector3(enemyItem["x"] - xOffset, enemyItem["y"] - yOffset, 0), Quaternion.identity);
            }
        }
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
        if (Rectangles == null) {
            return;
        }
        foreach (var (position, size) in Rectangles) {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(position, new Vector2(position.x + size.x, position.y));
            Gizmos.DrawLine(position, new Vector2(position.x, position.y + size.y));
            Gizmos.DrawLine(new Vector2(position.x + size.x, position.y), new Vector2(position.x + size.x, position.y + size.y));
            Gizmos.DrawLine(new Vector2(position.x + size.x, position.y + size.y), new Vector2(position.x, position.y + size.y));
        }
    }
}

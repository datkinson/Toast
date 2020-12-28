using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    // public static List<string> items = new List<string>();
    // public Image GemBlue, GemGreen, GemOrange;
    Text inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<Text> ();
        // GemBlue = GetComponent<Image> ();
        // GemBlue = GetComponent("GemBlue") as Image;
        // GemGreen = GetComponent<Image> ();
        // GemOrange = GetComponent<Image> ();
        // GemBlue.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        string concat = string.Join("\n", State.items.ToArray());
        inventory.text = "Inventory:\n" + concat;
        // if(items.Contains("GemBlue")){
        //     SetGemState(GemBlue, true);
        // } else {
        //     SetGemState(GemBlue, false);
        // }
        // if(items.Contains("GemGreen")){
        //     SetGemState(GemGreen, true);
        // } else {
        //     SetGemState(GemGreen, false);
        // }
        // if(items.Contains("GemOrange")){
        //     SetGemState(GemOrange, true);
        // } else {
        //     SetGemState(GemOrange, false);
        // }
    }

    private static void SetGemState(UnityEngine.UI.Image gem, bool state) {
        if(gem.enabled != state) {
            gem.enabled = state;
        }
    }
}

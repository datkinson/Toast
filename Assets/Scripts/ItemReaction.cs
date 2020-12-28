using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReaction : MonoBehaviour
{

    public static int scoreIncrease;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(scoreIncrease > 0) {
            State.ScoreValue += scoreIncrease;
            scoreIncrease = 0;
        }
    }

    void OnTriggerEnter2D (Collider2D other){
        if (other.CompareTag ("Gem")) {
            scoreIncrease = 5;
            if (!State.items.Contains("+ " + other.gameObject.name)) {
                State.items.Add("+ " + other.gameObject.name);
            }
            Destroy(other.gameObject);
        }
    }
}

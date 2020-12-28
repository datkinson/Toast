using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MelonReaction : MonoBehaviour
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
            Score.ScoreValue += scoreIncrease;
            scoreIncrease = 0;
        }
    }

    void OnTriggerEnter2D (Collider2D other){
        if (other.CompareTag ("Melon")) {
            scoreIncrease = 1;
            Destroy(other.gameObject);
        }
    }
}

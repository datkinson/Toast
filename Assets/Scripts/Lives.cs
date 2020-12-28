using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lives : MonoBehaviour
{
    public static int livesChange;
    Text lives;
    // Start is called before the first frame update
    void Start()
    {
         lives = GetComponent<Text> ();
    }

    // Update is called once per frame
    void Update()
    {
        if(livesChange != 0) {
            State.Lives += livesChange;
            livesChange = 0;
        }
        lives.text = "Lives: " + State.Lives;
        if(State.Lives < 0) {
            Debug.Log("Lose");
            // SceneManager.LoadScene (State.scenes[State.scenes.Length - 1]);
            State.loseScene();
        }
    }
}

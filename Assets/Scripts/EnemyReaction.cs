﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyReaction : MonoBehaviour
{
    public static int sceneChange = 0;
    public static Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("Scene: " + State.currentScene);
        // scene = SceneManager.GetActiveScene();
        // Debug.Log("Active Scene is '" + scene.name + "'.");
    }

    // Update is called once per frame
    void Update()
    {
        if(sceneChange != 0) {
            // State.currentScene += sceneChange;
            // sceneChange = 0;
            // Debug.Log("Scenes: " + State.scenes.Length);
            // Debug.Log("Attempting to switch to scene ID: " + State.currentScene);
            // Debug.Log("Attempting to switch to scene Name: " + State.scenes[State.currentScene]);
            // SceneManager.LoadScene (State.scenes[State.currentScene]);
        }
    }

    void OnTriggerEnter2D (Collider2D other){
        if (other.CompareTag ("Enemy")) {
            State.ScoreValue = State.savedScore;
            State.items.Clear();
            Lives.livesChange = -1;
            SceneManager.LoadScene (State.scenes[State.currentScene]);
        }
        if (other.CompareTag ("Abyss")) {
            State.ScoreValue = State.savedScore;
            State.items.Clear();
            Lives.livesChange = -1;
            SceneManager.LoadScene (State.scenes[State.currentScene]);
        }
        if (other.CompareTag ("Finish")) {
            Debug.Log("Scene Finish: " + State.currentScene);
            State.savedScore = State.ScoreValue;
            State.nextScene();
            // sceneChange = State.currentScene + 1;
        }
        if (other.CompareTag ("Win")) {
            Debug.Log("Win");
            State.savedScore = State.ScoreValue;
            // SceneManager.LoadScene ("Win");
            State.winScene();
        }
    }
}

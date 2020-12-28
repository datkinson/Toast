using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class State : MonoBehaviour
{
    public static List<string> items = new List<string>();
    public static int ScoreValue = 0;
    public static int Lives = 5;
    // public static string[] scenes = {"Scene1", "Scene2", "Scene3"};
    public static List<string> scenes = new List<string> {"Scene1", "Scene2", "Scene3"};
    public static int currentScene = 0;
    public static int newSceneID = 0;
    public static int savedScore = 0;
    // Start is called before the first frame update
    void Start()
    {}

    // Update is called once per frame
    void Update()
    {}

    public static void nextScene() {
        Debug.Log("State loading next scene");
        Scene scene = SceneManager.GetActiveScene();
        int index = scenes.IndexOf(scene.name);
        Debug.Log("Current scene index: " + index);
        currentScene = index + 1;
        Debug.Log("Next scene index: " + currentScene);
        Debug.Log("Next scene name: " + scenes[currentScene]);
        SceneManager.LoadScene (scenes[index + 1]);
    }

    // public static void previousScene() {
    //     newSceneID = currentScene - 1;
    //     SceneManager.LoadScene (State.scenes[newSceneID]);
    //     currentScene = newSceneID;
    // }

    public static void loseScene() {
        // SceneManager.LoadScene (State.scenes[State.scenes.Length - 1]);
        SceneManager.LoadScene ("Lose");
    }
    public static void winScene() {
        // SceneManager.LoadScene (State.scenes[State.scenes.Length - 2]);
        SceneManager.LoadScene ("Win");
    }
}

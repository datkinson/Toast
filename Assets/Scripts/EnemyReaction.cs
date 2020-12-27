using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyReaction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D other){
        if (other.CompareTag ("Enemy")) {
            SceneManager.LoadScene ("Scene1");
        }
        if (other.CompareTag ("Abyss")) {
            SceneManager.LoadScene ("Scene1");
        }
    }
}

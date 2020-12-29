using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 2;
    [SerializeField] private float distance = 0.2f;
    private bool movingRight = true;
    private Vector2 detectionDirection = Vector2.right;
    public Transform groundDetection;

    void Start()
    {}

    void Update()
    {}

    private void Flip() {
        if(movingRight) {
            transform.eulerAngles = new Vector3(0, -180, 0);
            movingRight = false;
            detectionDirection = Vector2.left;
        } else {
            transform.eulerAngles = new Vector3(0, 0, 0);
            movingRight = true;
            detectionDirection = Vector2.right;
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        RaycastHit2D wallInfo = Physics2D.Raycast(groundDetection.position, detectionDirection, distance);
        if(groundInfo.collider == false) {
            Flip();
        }
        if(wallInfo.collider == true) {
            if(wallInfo.collider.name == "Tilemap") {
                Flip();
            }
            // Debug.Log("wallInfo.collider.name: " + wallInfo.collider.name);
        }
    }
}

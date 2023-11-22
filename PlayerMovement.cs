using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{ 
    [SerializeField] private float speed;

    private Boolean canMove = false;
    private Rigidbody2D body;
    private Vector3 mousePos;
 
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
 
    private void Update()
    {
        if(canMove == true)
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);  
    }
}
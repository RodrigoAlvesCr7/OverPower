using UnityEngine;

public class PlayerMovement : MonoBehaviour
{ 
    [SerializeField] private float speed;

    private Rigidbody2D body;
    private Vector3 mousePos;
 
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }
 
    private void Update()
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);  
    }
}
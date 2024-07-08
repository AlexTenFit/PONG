using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float baseSpeed;
    [SerializeField] float baseVelocity = 1;
    private float speed;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.gravityScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        speed = baseSpeed * baseVelocity;
        float moveVertical = Input.GetAxis("Vertical");
        rb2d.velocity = new Vector2 (0, moveVertical * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Assuming 'speed' is a field that controls the knockback speed
        float knockBackForce = Input.GetAxis("Vertical") * speed;

        // Apply the knockback force in the opposite direction of the collision
        Vector2 knockBackDirection = -collision.GetContact(0).normal * knockBackForce;

        // Use Rigidbody2D to move the paddle instead of directly setting transform.position
        rb2d.velocity = knockBackDirection;
    }

}

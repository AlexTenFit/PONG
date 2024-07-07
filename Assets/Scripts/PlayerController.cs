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
        if (collision.gameObject.CompareTag("Wall"))
        {
            float knockBack = Input.GetAxis("Vertical");
            transform.position = Vector2.MoveTowards(
                transform.position,
                new Vector2(transform.position.x, transform.position.y / 5),
                speed / 5 * Time.deltaTime);
        }
    }

}

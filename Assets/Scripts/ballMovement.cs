using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

public class BallMovement : MonoBehaviour
{
    private static readonly float[] X_DIR_OPTS = { -1.0f, 1.0f };

    [SerializeField] public float ballSpeed = 1f;
    [SerializeField] private float ballAccel = 0.01f;
    [SerializeField] private float ballMaxSpeed = 10f;

    private float _xDirection = 0f;
    private float _yDirection = 0f;
    private Vector2 _velocity;

    private Rigidbody2D _ballRb = null;

    [SerializeField] private bool debug = false;

    void Start()
    {
        _ballRb = gameObject.GetOrAddComponent<Rigidbody2D>();

        _xDirection = X_DIR_OPTS[Random.Range(0, 2)]; // Only left or right

        if (debug) Debug.Log("start direction: " + _xDirection);

        _yDirection = Random.Range(-0.5f, 0.5f);
        _velocity = new Vector2(_xDirection, _yDirection).normalized * ballSpeed;
    }

    private void FixedUpdate()
    {
        // maintain velocity base constant
        _velocity = _velocity.normalized * ballSpeed;
        _ballRb.linearVelocity = _velocity;

        if (debug)
        {
            Debug.DrawRay(transform.position, _ballRb.linearVelocity, Color.green);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("paddle") && ballSpeed < ballMaxSpeed) 
        {
            ballSpeed *= 1 + ballAccel;
            if (debug) Debug.Log("new ball speed: " + ballSpeed);
        } else if (ballSpeed > ballMaxSpeed)
        {
            ballSpeed = ballMaxSpeed;
        }

        /* Formula for bouncing off of things:
         *
         *     r = d - 2 * (d . n)n
         *
         * where r: relected vector
         *       d: incident vector
         *       n: normal vector of surface at point of impact */


        foreach (var contact in other.contacts)
        {
            if (debug) Debug.DrawRay(contact.point, contact.normal, Color.red, 3);

            var d = _velocity;
            var n = contact.normal;
            var r = d - (2 * Vector2.Dot(d, n) * n);

            _velocity = r;
        }

        if (debug) Debug.Log("current velocity: " + _velocity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("bound"))
        {
            Application.Quit();
        }
    }
}
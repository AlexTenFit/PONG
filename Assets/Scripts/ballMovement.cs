using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ballMovement : MonoBehaviour
{
    private static readonly int[] X_DIR_OPTS = {  -1, 1};
    
    [SerializeField] private float ballSpeed = 1f;
    [SerializeField] private float ballAccel = 0.01f;
    
    private int _xDirection = 0;
    private float _yDirection = 0f;
    
    private Rigidbody2D _ballRb = null;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _ballRb = gameObject.GetOrAddComponent<Rigidbody2D>();

        _xDirection = X_DIR_OPTS[Random.Range(0, 1)]; // Only left or right
        Debug.Log(_xDirection);
        _yDirection = Random.Range(0f, 0.1f);
        _ballRb.AddForce(new Vector2(_xDirection * ballSpeed, _yDirection * ballSpeed), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("paddle"))
        {
            ballSpeed *= ballAccel;
        }
        _ballRb.AddForce(
            -1.0f * _ballRb.linearVelocity + new Vector2(
                             -other.gameObject.transform.position.x * ballSpeed, 
                             -other.gameObject.transform.position.y * ballSpeed), 
            ForceMode2D.Impulse);
    }
}
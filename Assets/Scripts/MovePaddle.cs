using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;

public class MovePaddle : MonoBehaviour
{
    [SerializeField] private float paddleSpeed = 1f;
    
    private InputAction _movePaddle;

    [SerializeField] private bool debug = false;
    
    void Start()
    {
        _movePaddle = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        var moveAmount = _movePaddle.ReadValue<Vector2>() * Time.deltaTime;
        if (debug) print("Move vector: " + moveAmount);
        
        transform.position = new Vector2(transform.position.x, transform.position.y + (moveAmount.y * paddleSpeed));
    }
}

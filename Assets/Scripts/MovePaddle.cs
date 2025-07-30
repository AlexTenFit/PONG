using UnityEngine;
using UnityEngine.InputSystem;

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
        var moveDir = _movePaddle.ReadValue<Vector2>();
        var moveAmount = moveDir * Time.deltaTime;
        if (debug)
        {
            Debug.Log("Move vector: " + moveAmount);
            Debug.DrawRay(transform.position, moveDir * 3, Color.green);
        }
        
        transform.position = new Vector2(transform.position.x, transform.position.y + (moveAmount.y * paddleSpeed * Time.deltaTime));
    }
}
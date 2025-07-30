using UnityEngine;

public class CpuPaddle : MonoBehaviour
{
    private float _paddleSpeed = 0f;
    private BallMovement _ball = null;

    [SerializeField] private float cpuSpeedBuffer = 0.9f;

    [SerializeField] private bool debug = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _ball = FindAnyObjectByType<BallMovement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        _paddleSpeed = _ball.ballSpeed;
        var target = new Vector2(transform.position.x, _ball.transform.position.y);
        if (debug)
        {
            Debug.Log("Cpu paddle move speed: " + _paddleSpeed);
            Debug.DrawRay(transform.position, new Vector2(0, target.y).normalized * 3, Color.blue);
        }
        
        transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime);
    }
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CameraChaser : MonoBehaviour
{
    private Rigidbody rb;
    private float currentSpeed;
    [SerializeField] private float defaultSpeed = 30f;
    [SerializeField] private Transform spherePosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Accelerate(defaultSpeed);       
    }

    private void Start()
    {
        GameManager.Instance.OnStartGame += () => { this.enabled = true; };
        this.enabled = false;
    }

    private void Update()
    {
        if (transform.position.x > spherePosition.position.x) Accelerate(defaultSpeed);
        else
        {
            var TimeBetweenObjects = (spherePosition.position.x - transform.position.x) / defaultSpeed;
            Accelerate(defaultSpeed * (TimeBetweenObjects * 10 / 0.02f)); //0.02 stands for FixedUpdate interval 
        }
        var newPosition = transform.position;
        newPosition.y = spherePosition.position.y;
        transform.position = newPosition;
    }


    private void FixedUpdate()
    {
        rb.velocity = Vector3.right * currentSpeed * Time.fixedDeltaTime;
    }


    public void Accelerate(float speed)
    {
        currentSpeed = speed;
    }
}

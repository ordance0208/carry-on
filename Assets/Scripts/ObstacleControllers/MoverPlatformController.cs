using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoverPlatformController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float moveLimit = 3f;
    private float startingXPos;
    private Rigidbody rb;
    private float inputX;
    private Renderer _renderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startingXPos = transform.position.x;
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        inputX = ButtonsInput.InputValue;

        inputX = !_renderer.isVisible ? 0 : inputX;

        rb.drag = rb.angularDrag = inputX != 0 ? 0 : Mathf.Infinity;

        RestrictMovement();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.velocity = Vector3.right * moveSpeed * inputX * Time.fixedDeltaTime;
    }

    private void RestrictMovement()
    {
        if(transform.position.x > startingXPos + moveLimit)
        {
            transform.position = new Vector3(startingXPos + moveLimit, transform.position.y, transform.position.z);           
        } else if (transform.position.x < startingXPos - moveLimit)
        {
            transform.position = new Vector3(startingXPos - moveLimit, transform.position.y, transform.position.z);
        }
    }
}

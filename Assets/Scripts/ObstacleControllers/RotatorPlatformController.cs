using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class RotatorPlatformController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;
    private Rigidbody rb;
    private Renderer _renderer;
    public float inputX { get; private set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        //if (inputX != Input.GetAxisRaw("Horizontal")) { rb.angularVelocity = Vector3.zero; }

        inputX = ButtonsInput.InputValue;

        inputX = !_renderer.isVisible ? 0 : inputX;

    }

    private void FixedUpdate()
    {
        Rotate();
        rb.drag = rb.angularDrag = inputX != 0 ? 0 : Mathf.Infinity;
    }

    private void Rotate()
    {
        rb.AddTorque(Vector3.forward * rotationSpeed * -inputX * Time.fixedDeltaTime);
    }

    //Debugging
    private void Restart()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

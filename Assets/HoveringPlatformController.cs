using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoveringPlatformController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float moveLimit = 3f;
    private float startingYPos;
    private Rigidbody rb;
    private float inputX;
    private Renderer _renderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startingYPos = transform.position.y;
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
        rb.velocity = Vector3.up * moveSpeed * inputX * Time.fixedDeltaTime;
    }

    private void RestrictMovement()
    {
        if (transform.position.y > startingYPos + moveLimit)
        {
            transform.position = new Vector3(transform.position.x, startingYPos + moveLimit, transform.position.z);
        }
        else if (transform.position.y < startingYPos - moveLimit)
        {
            transform.position = new Vector3(transform.position.x, startingYPos - moveLimit, transform.position.z);
        }
    }
}

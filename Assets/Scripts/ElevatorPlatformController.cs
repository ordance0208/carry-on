using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPlatformController : MonoBehaviour
{
    [SerializeField] private Rigidbody[] elevatorSurfaces;
    [SerializeField] private Transform[] elevatorLevelers;
    [SerializeField] private Vector2 elevationMinMaxY;
    [SerializeField] private Vector2 scaleMinMaxY;
    [SerializeField] private float elevationRate;
    [SerializeField] private float scaleRate;
    private float startingPositionY;
    private float startingScaleY;
    private float inputX;
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponentInChildren<Renderer>();
        startingPositionY = elevatorSurfaces[0].transform.position.y;
        startingScaleY = elevatorLevelers[0].localScale.y;
    }

    private void Update()
    {
        inputX = ButtonsInput.InputValue;

        inputX = !_renderer.isVisible ? 0 : inputX;
    }

    private void FixedUpdate()
    {
        if (inputX > 0) { ElevateLeft(); }
        else if (inputX < 0) { ElevateRight(); }
        else { ElevateBoth(); }
    }

    private void ElevateLeft()
    {
        var tempScaleLeft = elevatorLevelers[0].localScale;
        var tempScaleRight = elevatorLevelers[1].localScale;
        if (elevatorSurfaces[0].transform.position.y > startingPositionY + elevationMinMaxY.x)
        { elevatorSurfaces[0].velocity = Vector3.down * elevationRate * Time.fixedDeltaTime; }
        else { RestrictMovement(elevatorSurfaces[0]); }
        if (elevatorSurfaces[1].transform.position.y < startingPositionY + elevationMinMaxY.y)
        { elevatorSurfaces[1].velocity = Vector3.up * elevationRate * Time.fixedDeltaTime; }
        else { RestrictMovement(elevatorSurfaces[1]); }

        if (tempScaleLeft.y > startingScaleY + scaleMinMaxY.x)
        { elevatorLevelers[0].localScale = new Vector3(tempScaleLeft.x,
            tempScaleLeft.y - Mathf.Clamp(1f, scaleMinMaxY.x, scaleMinMaxY.y) * scaleRate * Time.fixedDeltaTime, tempScaleLeft.z); }
        else { RestrictRescale(elevatorLevelers[0], startingScaleY + scaleMinMaxY.x); }
        if (tempScaleRight.y < startingScaleY + scaleMinMaxY.y)
        { elevatorLevelers[1].localScale = new Vector3(tempScaleRight.x,
            tempScaleRight.y + Mathf.Clamp(1f, scaleMinMaxY.x, scaleMinMaxY.y) * scaleRate * Time.fixedDeltaTime, tempScaleRight.z); }
        else { RestrictRescale(elevatorLevelers[1], startingScaleY + scaleMinMaxY.y); }
    }

    private void ElevateRight()
    {
        var tempScaleLeft = elevatorLevelers[0].localScale;
        var tempScaleRight = elevatorLevelers[1].localScale;
        if (elevatorSurfaces[0].transform.position.y < startingPositionY + elevationMinMaxY.y)
        { elevatorSurfaces[0].velocity = Vector3.up * elevationRate * Time.fixedDeltaTime; }
        else { RestrictMovement(elevatorSurfaces[0]); }
        if (elevatorSurfaces[1].transform.position.y > startingPositionY + elevationMinMaxY.x)
        { elevatorSurfaces[1].velocity = Vector3.down * elevationRate * Time.fixedDeltaTime; }
        else { RestrictMovement(elevatorSurfaces[1]); }


        if (tempScaleLeft.y < startingScaleY + scaleMinMaxY.y)
        { elevatorLevelers[0].localScale = new Vector3(tempScaleLeft.x,
            tempScaleLeft.y + Mathf.Clamp(1f, scaleMinMaxY.x, scaleMinMaxY.y) * scaleRate * Time.fixedDeltaTime, tempScaleLeft.z); }
        else { RestrictRescale(elevatorLevelers[0], startingScaleY + scaleMinMaxY.y); }
        if (tempScaleRight.y > startingScaleY + scaleMinMaxY.x)
        { elevatorLevelers[1].localScale = new Vector3(tempScaleRight.x,
            tempScaleRight.y - Mathf.Clamp(1f, scaleMinMaxY.x, scaleMinMaxY.y) * scaleRate * Time.fixedDeltaTime, tempScaleRight.z); }
        else { RestrictRescale(elevatorLevelers[1], startingScaleY + scaleMinMaxY.x); }
    }

    private void ElevateBoth()
    {
        var tempScaleLeft = elevatorLevelers[0].localScale;
        var tempScaleRight = elevatorLevelers[1].localScale;
        if (elevatorSurfaces[0].transform.position.y < startingPositionY + elevationMinMaxY.y)
        { elevatorSurfaces[0].velocity = Vector3.up * elevationRate * Time.fixedDeltaTime; }
        else { RestrictMovement(elevatorSurfaces[0]); }
        if (elevatorSurfaces[1].transform.position.y < startingPositionY + elevationMinMaxY.y)
        { elevatorSurfaces[1].velocity = Vector3.up * elevationRate * Time.fixedDeltaTime; }
        else { RestrictMovement(elevatorSurfaces[1]); }

        if (tempScaleLeft.y < startingScaleY + scaleMinMaxY.y)
        { elevatorLevelers[0].localScale = new Vector3(tempScaleLeft.x,
            tempScaleLeft.y + Mathf.Clamp(1f, scaleMinMaxY.x, scaleMinMaxY.y) * scaleRate * Time.fixedDeltaTime, tempScaleLeft.z); }
        else { RestrictRescale(elevatorLevelers[0], startingScaleY + scaleMinMaxY.y); }
        if (tempScaleRight.y < startingScaleY + scaleMinMaxY.y)
        { elevatorLevelers[1].localScale = new Vector3(tempScaleRight.x,
            tempScaleRight.y + Mathf.Clamp(1f, scaleMinMaxY.x, scaleMinMaxY.y) * scaleRate * Time.fixedDeltaTime, tempScaleRight.z); }
        else { RestrictRescale(elevatorLevelers[1], startingScaleY + scaleMinMaxY.y); }
    }


    private void RestrictMovement(Rigidbody objRb)
    {
        objRb.velocity = Vector3.zero;
    }

    private void RestrictRescale(Transform objScale, float limitedScale)
    {
        objScale.localScale = new Vector3(objScale.localScale.x, limitedScale, objScale.localScale.z);
    }

 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCoin : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 360f;

    private void Update()
    {
        transform.Rotate(0f, rotationSpeed, 0f);
    }
}

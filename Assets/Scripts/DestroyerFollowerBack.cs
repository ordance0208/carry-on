using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerFollowerBack : MonoBehaviour
{
    [SerializeField] private Transform followingObject;
    [SerializeField] private Vector2 minMaxOffset;
    [SerializeField] private CameraController camController;

    private void Awake()
    {
        UpdateWidth();
    }

    private void Update()
    {
        var tempPos = transform.position;
        tempPos.x = followingObject.position.x;
        followingObject.position = tempPos;
        UpdateWidth();
    }

    private void UpdateWidth()
    {
        var tempPos = followingObject.position;
        if (camController.cameraViewSize.z > camController.cameraViewSize.x)
        { tempPos.x = transform.position.x - minMaxOffset.y; }
        else if (camController.cameraViewSize.z <= camController.cameraViewSize.x)
        { tempPos.x = transform.position.x - minMaxOffset.x; }
        followingObject.position = tempPos;
    }
}

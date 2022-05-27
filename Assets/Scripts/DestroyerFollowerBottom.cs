using UnityEngine;

public class DestroyerFollowerBottom : MonoBehaviour
{
    [SerializeField] private Transform followingObject;
    [SerializeField] private float offset;

    private void Awake()
    {
        UpdateHeight();
    }

    private void OnCollisionEnter(Collision collision)
    {
        UpdateHeight();
    }

    private void Update()
    {
        var tempPos = transform.position;
        tempPos.y = followingObject.position.y;
        followingObject.position = tempPos;
    }

    private void UpdateHeight()
    {
        var tempPos = followingObject.position;
        tempPos.y = transform.position.y - offset;
        followingObject.position = tempPos;
    }
}

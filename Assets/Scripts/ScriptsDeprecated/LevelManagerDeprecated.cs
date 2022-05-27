using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerDeprecated : MonoBehaviour
{
    Rigidbody rb;
    private float moveSpeed;
    private float lastPositionX;
    private LevelGeneratorDeprecated levelGenerator;
    [SerializeField] private float defaultSpeed;
    [SerializeField] private Transform spherePosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        levelGenerator = GetComponent<LevelGeneratorDeprecated>();
        Accelerate(defaultSpeed);
        lastPositionX = transform.position.x;
    }

    private void Update()
    {
        if (transform.position.x > spherePosition.position.x) Accelerate(defaultSpeed);
        else 
        { 
            var TimeBetweenObjects = (spherePosition.position.x - transform.position.x) / defaultSpeed;
            Accelerate(defaultSpeed * (TimeBetweenObjects / 0.02f)); //0.02 stands for FixedUpdate interval 
            //The sphere, hypothetically, can still run away, but in that case should get destroyed by the barriers
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.right * moveSpeed * Time.fixedDeltaTime;
        if (transform.position.x - lastPositionX > levelGenerator.DistancePerObstacle)
        {
            lastPositionX = transform.position.x;
            levelGenerator.SpawnObstacle();
        }
    }

    public void Accelerate(float speed) //Had different purpose before, now too lazy to remove it
    {
        moveSpeed = speed;
    }
}

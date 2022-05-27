using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDestroyer : MonoBehaviour
{
    [SerializeField] private string obstacleTags;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == obstacleTags)
        {
            Destroy(collision.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPlatformController : MonoBehaviour
{
    [SerializeField] private LayerMask layer;
    [SerializeField] private float forceToAdd;
    [SerializeField] private ForceMode mode;
    [SerializeField] private Material inactive;
    private float forceToAddMultiplier;

    private void Start()
    {
        forceToAddMultiplier = LevelDifficulty.Instance.BoostExtra_m;
    }

    private bool isUsed = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (LayerMask.LayerToName(layer.value) == LayerMask.LayerToName(collision.gameObject.layer) || isUsed) return;
        SoundHandler.Instance.PlayAudio(SoundEffect.Boost);
        var tempRb = collision.gameObject.GetComponent<Rigidbody>();
        tempRb.velocity = Vector3.zero;
        tempRb.AddForce(Vector3.right * forceToAdd / 2 + Vector3.up * forceToAdd * forceToAddMultiplier / 2, mode);
        gameObject.GetComponent<MeshRenderer>().material = inactive;
        isUsed = true;
    }

    private Vector3 CalculateVelocity(Vector3 source, Vector3 target, float angle)
    {
        Vector3 direction = target - source;
        float h = direction.y;
        direction.y = 0;
        float distance = direction.magnitude;
        float a = angle * Mathf.Deg2Rad;
        direction.y = distance * Mathf.Tan(a);
        distance += h / Mathf.Tan(a);

        // calculate velocity
        float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return velocity * direction.normalized;
    }
}

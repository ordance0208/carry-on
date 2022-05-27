using UnityEngine;

public class PassForce : MonoBehaviour
{
    [SerializeField] private GameObject booster;
    private Rigidbody playerRb;
    private RotatorPlatformController rpc;

    private void Start()
    {
        rpc = GetComponent<RotatorPlatformController>();
    }

    private void Update()
    {
        if (rpc.inputX != 0 && playerRb)
        {
            PassSphereForce(booster.transform.localPosition.x >= 0 ? -rpc.inputX : rpc.inputX);
        }
    }

    private void PassSphereForce(float input)
    {
        playerRb.AddForce(booster.transform.up * input * 55f * Time.deltaTime, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        playerRb = collision.gameObject.GetComponent<Rigidbody>();
        playerRb.velocity *= 0.3f;
    }

    private void OnCollisionStay(Collision collision)
    {
        var tempPoint = collision.GetContact(0).point;
        booster.transform.position = tempPoint;
        booster.transform.localRotation = Quaternion.LookRotation(collision.transform.position - booster.transform.position);        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            playerRb = null;
    }
}

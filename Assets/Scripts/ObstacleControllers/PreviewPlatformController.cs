using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewPlatformController : MonoBehaviour
{
    [SerializeField] private Rigidbody[] cracksRb;
    [SerializeField] private MeshCollider[] cracksMc;
    [SerializeField] private Transform[] instantDisabled;
    [SerializeField] private float deactivatePeriod;
    [SerializeField] private float disablePeriod;

    private void Start()
    {
        GameManager.Instance.OnStartGame += ActivatePlatform;
    }

    private void ActivatePlatform()
    {
        foreach (Transform obj in instantDisabled)
        { obj.gameObject.SetActive(false); }
        SoundHandler.Instance.PlayAudio(SoundEffect.Crack);
        foreach (Rigidbody rb in cracksRb)
        {
            rb.isKinematic = false;
        }
        Invoke("DeactivatePlatform", deactivatePeriod);
    }

    private void DeactivatePlatform()
    {
        foreach(MeshCollider mc in cracksMc)
        {
            mc.enabled = false;
        }
        Invoke("DisablePlatform", disablePeriod);
    }

    private void DisablePlatform()
    {
        Destroy(gameObject);
    }

}

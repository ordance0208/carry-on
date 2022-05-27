using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCracked : MonoBehaviour
{
    [SerializeField] private Rigidbody[] rbs;
    [SerializeField] private MeshRenderer[] mrs;

    public void PlayerCrack(Material mat)
    {
        foreach (MeshRenderer mr in mrs)
        {
            mr.material = mat;
        }
        gameObject.SetActive(true);
        foreach (Rigidbody rb in rbs)
        {
            rb.isKinematic = false;
        }
    }
}

using System.Collections;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    [SerializeField] private Transform zappedParticles;
    [SerializeField] private SphereCollider col;
    [SerializeField] private MeshRenderer mr;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerCracked playerCracked;
    [SerializeField] private CameraController camDisabler;
    [SerializeField] private float destructionDelay;

    public bool PlayerDead { get; private set; }

    public void PreDestroyHandler(DeathType deathType)
    {
        if (deathType == DeathType.OutOfBounds)
        { StartCoroutine(PreDestroyOOB()); }
        else if (deathType == DeathType.Zapper)
        { StartCoroutine(PreDestroyZapper()); }
    }

    private IEnumerator PreDestroyOOB()
    {
        col.enabled = false;
        camDisabler.enabled = false;
        SoundHandler.Instance.PlayAudio(SoundEffect.Death);
        PlayerDead = true;
        yield return new WaitForSeconds(destructionDelay);
        GameManager.Instance.EndGame();
    }

    private IEnumerator PreDestroyZapper()
    {
        playerCracked.PlayerCrack(mr.material);
        mr.enabled = false;
        col.enabled = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        var zapParticle = Instantiate(zappedParticles, transform.position, Quaternion.identity);
        SoundHandler.Instance.PlayAudio(SoundEffect.Zap);
        PlayerDead = true;
        yield return new WaitForSeconds(destructionDelay);
        GameManager.Instance.EndGame();
    }
}

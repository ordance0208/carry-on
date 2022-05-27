using UnityEngine;

public enum DeathType { Zapper, OutOfBounds }

public class PlayerDestroyer : MonoBehaviour
{
    [SerializeField] private DeathType deathType;
    bool isDestroyed = false;
    

    private void OnTriggerEnter(Collider other)
    {
        if (isDestroyed) return;
        if (other.gameObject == LevelDifficulty.Instance.Player.gameObject)
        {
            isDestroyed = true;
            other.gameObject.GetComponent<SphereController>().PreDestroyHandler(deathType);
        }
    }
}

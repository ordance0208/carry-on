using UnityEngine;

public class CoinSpawn : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform coinSpawnPoint;
    [Range(0, 100)] [SerializeField] private float coinSpawnChance;

    private void Start()
    {       
        if(Random.Range(0, 2) <= coinSpawnChance / 100)
        {
            Instantiate(coinPrefab, coinSpawnPoint.position, Quaternion.identity);       
        }
    }
}

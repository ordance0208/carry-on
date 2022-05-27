using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] private List<ObstacleRarity> platformPrefabs = new List<ObstacleRarity>();
    [SerializeField] private GameObject startingPlatform;
    [SerializeField] private float distanceToSpawn = 30f;
    private GameObject lastPlatform;
    private int totalChanceIni = 0;
    private float distanceMultiplerX;
    
    private int moverID;

    private void Start()
    {
        GameManager.Instance.OnStartGame += GameStart;
        LevelDifficulty.Instance.DifficultyChanged += SetUpDistance;
        for (int i = 0; i < platformPrefabs.Count; i++)
        {
            if (platformPrefabs[i].ObstaclePrefab.GetComponentInChildren<PlatformInfo>().PlatfromType == PlatfromType.Moving)
                moverID = i;
        }
    }

    private void GameStart()
    {
        foreach (ObstacleRarity pack in platformPrefabs)
        { totalChanceIni += pack.ChanceToSpawn; }
        SpawnCalculation();
    }

    private void SetUpDistance()
    {
        distanceMultiplerX = LevelDifficulty.Instance.Distance_m;
    }

    private void FixedUpdate()
    {
        if (!lastPlatform) return;
        if(lastPlatform.transform.position.x - LevelDifficulty.Instance.Player.position.x < distanceToSpawn)
        {
            SpawnCalculation();
        }
    }

    private void SpawnCalculation()
    {
        if(!lastPlatform)
        {
            lastPlatform = Instantiate(startingPlatform, Vector3.zero, Quaternion.identity);
        }

        var lastPlatformInfo = lastPlatform.GetComponentInChildren<PlatformInfo>();
        var newPlatform = GetRandomObstacle();
        if (lastPlatformInfo.PlatfromType == PlatfromType.Booster)
        {
            newPlatform = platformPrefabs[moverID].ObstaclePrefab;
        }

            


        var newPlatformInfo = newPlatform.GetComponentInChildren<PlatformInfo>();

        var elevation = Random.Range(lastPlatformInfo.MinMaxElevation.x, lastPlatformInfo.MinMaxElevation.y);
        var distance = Random.Range(lastPlatformInfo.MinMaxDistance.x * distanceMultiplerX, 
            lastPlatformInfo.MinMaxDistance.y * distanceMultiplerX); //<---difficulty

        if (lastPlatformInfo.PlatfromType == PlatfromType.Moving && newPlatformInfo.PlatfromType == PlatfromType.Moving)
        {
            distance += 4;
        } else if (lastPlatformInfo.PlatfromType == PlatfromType.Moving ^ newPlatformInfo.PlatfromType == PlatfromType.Moving)
        {
            distance += 2;
        }

        var temp = Instantiate(newPlatform, new Vector3(-30, 0, 0), Quaternion.identity, transform);
        var tempInfo = temp.GetComponentInChildren<PlatformInfo>();
        temp.transform.position = lastPlatform.transform.position + new Vector3(lastPlatformInfo.GetScaleX / 2 + distance + tempInfo.GetScaleX / 2,
        elevation, 0);
        lastPlatform = temp;

    }

    private GameObject GetRandomObstacle()
    {
        GameObject spawnObject = null;
        int randomNumber = Random.Range(1, totalChanceIni + 1);
        foreach(ObstacleRarity pack in platformPrefabs)
        {
            if (randomNumber <= pack.ChanceToSpawn)
            {
                spawnObject = pack.ObstaclePrefab;
                break;
            }
            randomNumber -= pack.ChanceToSpawn;
        }

        return spawnObject;
    }
}

[System.Serializable]
public class ObstacleRarity
{
    public GameObject ObstaclePrefab;
    public int ChanceToSpawn;
}

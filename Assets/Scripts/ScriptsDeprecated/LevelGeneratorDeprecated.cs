using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratorDeprecated : MonoBehaviour
{
    [SerializeField] private Transform spawnerPivot;
    [SerializeField] private ObstacleSettings[] PrefabSettings; //Scroll very bottom for info
    //Calculate units moved in order to create a prefab, for ex: creates prefab every 300 units moved
    [Header("Empty space between 2 obstacles")]
    [SerializeField] private float distanceOffset;
    [HideInInspector] public float DistancePerObstacle = 1;
    private ObstacleSettings obstacleToSpawn;



    public void SpawnObstacle()
    {
        if (obstacleToSpawn != null)
        { Instantiate(obstacleToSpawn.PrefabToSpawn, RandomHeight(obstacleToSpawn.DistanceXY), Quaternion.identity); }
        obstacleToSpawn = PrefabSettings[Random.Range(0, PrefabSettings.Length)];
        DistancePerObstacle = distanceOffset + obstacleToSpawn.DistanceXY.x; //Takes only the x distances for spawning


    }

    private Vector3 RandomHeight(Vector2 height)
    {
        return new Vector3(spawnerPivot.position.x, Random.Range(spawnerPivot.position.y - height.y / 2, spawnerPivot.position.y + height.y / 2), spawnerPivot.position.z);
    }
}

[System.Serializable]
public class ObstacleSettings
{
    public GameObject PrefabToSpawn;
    public Vector2 DistanceXY;
}

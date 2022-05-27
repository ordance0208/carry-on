using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ZapperPlatformController))]
[RequireComponent(typeof(PlatformDimensionRandomizer))]
public class ZapperPlatformGenerator : MonoBehaviour
{
    private ZapperPlatformController zapController;
    [SerializeField] private GameObject zapPrefab;
    [SerializeField] private Transform machinePivot;
    [SerializeField] private PlatformDimensionRandomizer dimensions;
    [SerializeField] private Vector2 minMaxLeftZappers;
    [SerializeField] private Vector2 minMaxRightZappers;
    [SerializeField] private int offset;

    private void Awake()
    {
        zapController = GetComponent<ZapperPlatformController>();
        dimensions = GetComponent<PlatformDimensionRandomizer>();
    }
    private void Start()
    {   //Mora da bide ova vo start bidejki inaku GetDimensionX ne vrakja tocna vrednost
        int objectLength = dimensions.GetDimensionX();
        int randomLefts = (int)Random.Range(minMaxLeftZappers.x, minMaxLeftZappers.y);
        int randomRights = (int)Random.Range(minMaxRightZappers.x, minMaxRightZappers.y);    
        if (randomLefts > objectLength / 2) { randomLefts = objectLength / 2; }
        if (randomRights > objectLength / 2) { randomRights = objectLength / 2; }
        List<GameObject> listOfLefts = new List<GameObject>();
        for (int i = 0; i < randomLefts; i++)
        {
            var temp = Instantiate(zapPrefab, new Vector3(machinePivot.position.x - offset - (objectLength - offset) / 2 / 
                randomLefts * i + offset * 0.5f, machinePivot.position.y, machinePivot.position.z), Quaternion.identity, machinePivot);
            listOfLefts.Add(temp);
        }
        List<GameObject> listOfRights = new List<GameObject>();
        for (int i = 0; i < randomRights; i++)
        {
            var temp = Instantiate(zapPrefab, new Vector3(machinePivot.position.x + offset + (objectLength - offset) / 2 / 
                randomRights * i - offset * 0.5f, machinePivot.position.y, machinePivot.position.z), Quaternion.identity, machinePivot);
            listOfRights.Add(temp);
        }
        zapController.SetZapLists(listOfLefts, listOfRights);
    }
}

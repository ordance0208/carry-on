using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDimensionRandomizer : MonoBehaviour
{
    [SerializeField] private Transform[] objectToRandomizeSize;
    [SerializeField] private Vector2 minMaxRandomSizeX;
    [SerializeField] private Vector2 minMaxRandomSizeY;
    [SerializeField] private bool RandomX;
    [SerializeField] private bool RandomY;

    private void Awake()
    {
        var tempX = Random.Range(minMaxRandomSizeX.x, minMaxRandomSizeX.y + 1); //  Values are randomed here but assigned later so that all objects
        var tempY = Random.Range(minMaxRandomSizeY.x, minMaxRandomSizeY.y + 1); // remain with same height and/or width
        foreach (Transform obj in objectToRandomizeSize)                        
        {
            var tempSize = obj.localScale;
            if (RandomX) tempSize.x = tempX;
            if (RandomY) tempSize.y = tempY;
            obj.localScale = tempSize;
        }
    }

    public int GetDimensionX()
    {
        int longest = 0; 
        foreach (Transform obj in objectToRandomizeSize)
        {
            if (obj.localScale.x > longest) { longest = (int)obj.localScale.x; }
        }
        return longest;
    }

    public int GetDimensionY()
    {
        int longest = 0;
        foreach (Transform obj in objectToRandomizeSize)
        {
            if (obj.localScale.y > longest) { longest = (int)obj.localScale.y; }
        }
        return longest;
    }

}

using UnityEngine;

public enum PlatfromType { Moving, Rotating, Spinning, Zapper, Booster, Hover, Elevator};
public class PlatformInfo : MonoBehaviour
{
    [SerializeField] private PlatfromType platformType;
    [SerializeField] private Vector2 minMaxElevation;
    [SerializeField] private Vector2 minMaxDistance;

    public PlatfromType PlatfromType
    {
        get { return platformType; }
    }

    public Vector2 MinMaxElevation
    {
        get { return minMaxElevation; }
    }

    public Vector2 MinMaxDistance
    {
        get { return minMaxDistance; }
    }

    public float GetScaleX
    {
        get { return transform.lossyScale.x; }
    }
}


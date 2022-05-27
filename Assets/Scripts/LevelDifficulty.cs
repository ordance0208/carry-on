using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnDifficultyChanged();

public class LevelDifficulty : MonoBehaviour
{
    public OnDifficultyChanged DifficultyChanged;
    public Transform Player;
    private float currentDifficulty = 0;
    private Vector3 lastPosition;
    [SerializeField] private float distanceDifficulty;
    [Range(1, 10)] [SerializeField] private int maxDifficulty;

    private const float SPEED_MULTIPLIER = 0.25f;
    private const float SPEED_BASE = 1f;
    private float speed_m;
    public float Speed_m
    {
        set
        {
            speed_m = value;
        }
        get
        {
            return speed_m;
        }
    }

    private const float MAX_SPEED_MULTIPLIER = 0.6f;
    private const float MAX_SPEED_BASE = 1f;
    private float maxSpeed_m;
    public float MaxSpeed_m
    {
        set
        {
            maxSpeed_m = value;
        }
        get
        {
            return maxSpeed_m;
        }
    }

    private const float DISTANCE_MULTIPLIER = 0.40f;
    private const float DISTANCE_BASE = 1f;
    private float distance_m;
    public float Distance_m
    {
        set
        {
            distance_m = value;
        }
        get 
        {
            return distance_m;
        }
    }

    private const float BACKGROUND_COLOR_MULTIPLIER = 0.1f;
    private float backgroundColor_m;
    public float BackgroundColor_m
    {
        set
        {
            backgroundColor_m = value;
        }
        get
        {
            return backgroundColor_m;
        }
    }

    private const float BOOST_EXTRA_MULTIPLIER = 0.45f;
    private const float BOOST_EXTRA_BASE = 1f;
    private float boostExtra_m;
    public float BoostExtra_m
    {
        set
        {
            boostExtra_m = value;
        }
        get
        {
            return boostExtra_m;
        }
    }

    public static LevelDifficulty Instance;

    private void Awake()
    {
        Instance = this;
        Speed_m = SPEED_BASE + SPEED_MULTIPLIER * currentDifficulty;
        MaxSpeed_m = MAX_SPEED_BASE + MAX_SPEED_MULTIPLIER * currentDifficulty;
        Distance_m = DISTANCE_BASE + DISTANCE_MULTIPLIER * currentDifficulty;
        BackgroundColor_m = BACKGROUND_COLOR_MULTIPLIER * currentDifficulty;
        BoostExtra_m = BOOST_EXTRA_BASE + BOOST_EXTRA_MULTIPLIER * currentDifficulty;
    }

    private void Update()
    {
        DifficultyTracker();
    }
    
    private void DifficultyTracker()
    {
        if (currentDifficulty >= maxDifficulty) return;
        if (Player.position.x - lastPosition.x > distanceDifficulty)
        {
            distanceDifficulty *= 2;
            currentDifficulty++;
            Speed_m = SPEED_BASE + SPEED_MULTIPLIER * currentDifficulty;
            MaxSpeed_m = MAX_SPEED_BASE + MAX_SPEED_MULTIPLIER * currentDifficulty;
            Distance_m = DISTANCE_BASE + DISTANCE_MULTIPLIER * currentDifficulty;
            BackgroundColor_m = BACKGROUND_COLOR_MULTIPLIER * currentDifficulty;
            BoostExtra_m = BOOST_EXTRA_BASE + BOOST_EXTRA_MULTIPLIER * currentDifficulty;
            DifficultyChanged?.Invoke();
        }
    }

}

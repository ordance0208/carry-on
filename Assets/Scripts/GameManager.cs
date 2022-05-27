using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Start, Play, Dead, Paused};

public class GameManager : MonoBehaviour
{
    #region Events
    public Action OnStartGame;
    public Action OnGamePause;
    public Action OnEndGame;
    #endregion

    private Camera mainCam;
    private static bool restarted = false;
    private static int timesPlayed;
    [SerializeField] private int timesPlayedToShowAds;
    [SerializeField] private ShopCamera shopCamera;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private AdsManager adsManager;

    #region Singleton

    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public GameState _GameState { get; private set; }

    private void Start() 
    {
        adsManager.onVideoEnd += RewardPlayer;

        _GameState = GameState.Start;
        mainCam = Camera.main;
        if (restarted)
        {
            StartGame();
            TogglePause();
            TogglePause();
            restarted = false;
        }
    }

    private void OnDisable()
    {
        adsManager.onVideoEnd -= RewardPlayer;
    }

    public void StartGame()
    {
        _GameState = GameState.Play;
        LevelDifficulty.Instance.DifficultyChanged?.Invoke(); //Dvete neka stojat
        OnStartGame?.Invoke();
        Invoke("EnableLevelDificulty", 0.5f); //Treba dvete da stojat
        mainCam.GetComponent<CameraController>().enabled = true;
    }

    private void EnableLevelDificulty()
    {
        LevelDifficulty.Instance.DifficultyChanged?.Invoke();
    }

    public void EndGame()
    {
        _GameState = GameState.Dead;
        OnEndGame?.Invoke();
        timesPlayed++;

        if (timesPlayed == timesPlayedToShowAds)
        {
            adsManager.ShowInterstitialAd();
            timesPlayed = 0;
        }
    }

    private void RewardPlayer()
    {
        CurrencyManager.Instance.AddDoubleCoins();
        MainMenu();
    }

    public void RestartScene()
    {
        restarted = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void TogglePause()
    {
        _GameState = _GameState == GameState.Play ? GameState.Paused : GameState.Play;
        Time.timeScale = _GameState == GameState.Paused ? 0 : 1;
        OnGamePause?.Invoke();
    }

    public void MainMenuReward()
    {
        CurrencyManager.Instance.AddTemporaryCoins();
        MainMenu();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (_GameState == GameState.Paused)
        {
            TogglePause();
        }
    }

    public void OpenSkinSelector()
    {
        shopCamera.enabled = true;
        uiManager.ToggleSkinSelector();
    }
}

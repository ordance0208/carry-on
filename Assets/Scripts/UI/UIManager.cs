using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject playCanvas;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject settingsCanvas;
    [SerializeField] private GameObject skinSelectorCanvas;
    [SerializeField] private GameObject shopCanvas;
    [SerializeField] private GameObject gameOverCanvas;

    public static UIManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        GameManager.Instance.OnStartGame += StartGameCanvas;
        GameManager.Instance.OnGamePause += TogglePauseCanvas;
        GameManager.Instance.OnEndGame += GameOverCanvas;
    }

    public void UpdateUI(UIComponentEvaluation _component)
    {
        _component.TextField.text = $"{_component.TextPrefix}{_component.TextAmount} {_component.TextSuffix}";
    }

    public void GameOverCanvas()
    {
        gameOverCanvas.SetActive(true);
    }   

    private void StartGameCanvas()
    {
        menuCanvas.SetActive(false);
        playCanvas.SetActive(true);
    }

    private void TogglePauseCanvas()
    {
        pauseCanvas.SetActive(!pauseCanvas.activeSelf);
    }

    public void ToggleSettingsCanvas()
    {
        settingsCanvas.SetActive(!settingsCanvas.activeSelf);
        menuCanvas.SetActive(!menuCanvas.activeSelf);
    }

    public void ToggleSkinSelector()
    {
        menuCanvas.SetActive(!menuCanvas.activeSelf);
        skinSelectorCanvas.SetActive(!skinSelectorCanvas.activeSelf);
    }

    public void ToggleShop()
    {
        menuCanvas.SetActive(!menuCanvas.activeSelf);
        shopCanvas.SetActive(!shopCanvas.activeSelf);
    }
}

[System.Serializable]
public class UIComponentEvaluation
{
    [Tooltip("This is only used for visual identification in the inspector")]
    public string ComponentName;
    public Text TextField;
    public string TextAmount;
    public string TextPrefix;
    public string TextSuffix = "";
}

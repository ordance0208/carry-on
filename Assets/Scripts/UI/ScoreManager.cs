using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private UIComponentEvaluation score;
    [SerializeField] private Transform currentPosition;
    [SerializeField] private SphereController sphereStatus;
    //[SerializeField] private ScoreController scoreCheck;
    private float lastPosition;
    private float startingPosition;
    private int highScore;
    private const string highScoreLiteral = "HighScore";

    private void Awake()
    {
        startingPosition = currentPosition.position.x;
        if (PlayerPrefs.HasKey("HighScore")) highScore = PlayerPrefs.GetInt(highScoreLiteral);
    }

    private void Start()
    {
        GameManager.Instance.OnEndGame += CheckForHighScore;
    }

    private void CheckForHighScore()
    {
        if(int.Parse(score.TextAmount) > highScore)
        {
            highScore = int.Parse(score.TextAmount);
            PlayerPrefs.SetInt(highScoreLiteral, highScore);
        }
    }

    private void Update()
    {
        if(GameManager.Instance._GameState == GameState.Play)
        {
            if (currentPosition.position.x < lastPosition) return;
            if  (sphereStatus.PlayerDead) return;
            score.TextAmount = (lastPosition = (int)currentPosition.position.x - startingPosition).ToString();
            score.TextField.text.Trim();
            UIManager.Instance.UpdateUI(score);
        }
    }
}

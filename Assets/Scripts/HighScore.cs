using UnityEngine;

public class HighScore : MonoBehaviour
{
    [SerializeField] private UIComponentEvaluation highScore;
    private const string highScoreLiteral = "HighScore";

    private void Start()
    {
        if(PlayerPrefs.HasKey(highScoreLiteral))
        {
            foreach(Transform go in transform)
            {
                go.gameObject.SetActive(true);
                highScore.TextAmount = PlayerPrefs.GetInt(highScoreLiteral).ToString();
                UIManager.Instance.UpdateUI(highScore);
                highScore.TextField.text = highScore.TextField.text.Trim();
            }
        }
    }
}

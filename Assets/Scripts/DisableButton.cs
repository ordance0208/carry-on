using UnityEngine;

public class DisableButton : MonoBehaviour
{
    [SerializeField] private GameObject doubleButton;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject noThanksText;

    private void Start()
    {
        GameManager.Instance.OnEndGame += ToggleButton;
    }

    public void ToggleButton()
    {
        if (CurrencyManager.Instance.TempCoins == 0)
        {
            doubleButton.SetActive(false);
            noThanksText.SetActive(false);
            continueButton.SetActive(true);
        }
    }
}

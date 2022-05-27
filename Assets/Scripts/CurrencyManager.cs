using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    #region Singleton
    public static CurrencyManager Instance;
    #endregion

    [SerializeField] private Shop shop;
    [Space(5)]
    [SerializeField] private UIComponentEvaluation temporaryCoins;
    [SerializeField] private UIComponentEvaluation allCoins;
    [SerializeField] private int coins;
    [SerializeField] private int tempCoins;

    #region Getters
    public int Coins { get { return coins; } } 
    public int TempCoins { get { return tempCoins; } }
    #endregion

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

        tempCoins = 0;
        GetCoinsData();
    }

    //Debugging
    public void ResetCoins()
    {
        coins = 0;
        SaveCoinsData();
        UpdateCoins();
    }
    //Debugging

    private void Start()
    {
        GameManager.Instance.OnEndGame += UpdateTempCoinsUI;
        UpdateCoins();
    }

    private void UpdateTempCoinsUI()
    {
        temporaryCoins.TextAmount = tempCoins.ToString();
        UIManager.Instance.UpdateUI(temporaryCoins);
    }

    private void UpdateCoins()
    {
        allCoins.TextAmount = coins.ToString();
        UIManager.Instance.UpdateUI(allCoins);
        allCoins.TextField.text = allCoins.TextField.text.Trim();
    }

    private void GetCoins(int amount)
    {
        coins += amount;
        SaveCoinsData();
        UpdateCoins();
    }

    public void BuySkin(Skin skin)
    {
        if(Coins >= skin.skinPrice)
        {
            coins -= skin.skinPrice;
            skin.SkinBought();
            SaveCoinsData();
            UpdateCoins();
            shop.PopulateSkinSelector();
        }
    }

    public void CollectCoin()
    {
        tempCoins += 1;
    }

    public void AddDoubleCoins()
    {
        GetCoins(tempCoins * 2);
    }

    public void AddTemporaryCoins()
    {
        GetCoins(tempCoins);
    }

    private void GetCoinsData()
    {
        if(PlayerPrefs.HasKey("Coins"))
        {
            coins = PlayerPrefs.GetInt("Coins");
        } else
        {
            PlayerPrefs.SetInt("Coins", 0);
            coins = PlayerPrefs.GetInt("Coins");
        }
    }

    private void SaveCoinsData()
    {
        PlayerPrefs.SetInt("Coins", coins);
    }
}

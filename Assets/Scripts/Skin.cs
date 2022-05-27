using UnityEngine;

public enum Rarity { Common, Rare, Epic, Default};

[CreateAssetMenu(fileName = "New Skin")]
public class Skin : ScriptableObject
{
    public string skinName;
    public Rarity rarity;
    public int skinPrice;
    public Sprite skinSprite;
    public Material skinMaterial;
    public bool isOwned;
    public UIComponentEvaluation skinText;

    public void SkinBought()
    {
        isOwned = true;
        skinText.TextAmount = "OWNED";
        UIManager.Instance.UpdateUI(this.skinText);
        PlayerPrefs.SetInt($"{this.skinName}", Converter.BoolToInt(true));
    }

    public void BuyItem()
    {
        if (!this.isOwned) { CurrencyManager.Instance.BuySkin(this); }
    }

    private void OnEnable()
    {
        if (!PlayerPrefs.HasKey(this.skinName))
        {
            Debug.Log("Fired 1");
            if(this.rarity == Rarity.Default)
            {
                Debug.Log("Fired");
                PlayerPrefs.SetInt(this.skinName, 1);
            } else
            {
                Debug.Log("Fired 2");
                PlayerPrefs.SetInt(this.skinName, 0);
            }
        }

        isOwned = Converter.IntToBool(PlayerPrefs.GetInt(this.skinName));

        if (this.rarity == Rarity.Default) return;
        if(this.rarity == Rarity.Common) { this.skinPrice = 35; }
        else if(this.rarity == Rarity.Rare) { this.skinPrice = 55; }
        else { this.skinPrice = 75; }
    }
}

public static class Converter
{
    public static bool IntToBool(int value)
    {
        if(value == 0) return false;
        else if(value == 1) return true;
        else throw new System.Exception("Only 0 and 1 are valid numbers");
    }

    public static int BoolToInt(bool value)
    {
        if (value) return 1;
        else return 0;
    }
}

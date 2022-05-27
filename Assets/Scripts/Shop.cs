using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private ChangeSkin changeSkin;
    [SerializeField] private RectTransform scrollingPanel;
    [SerializeField] private RectTransform fullScreen;


    [SerializeField] private Button shopButtonPrefab;
    [SerializeField] private Button skinSelectorButtonPrefab;
    [SerializeField] private Transform skinSelectorPanel;
    [SerializeField] private Transform containerGroupPrefab;
    public ShopContainer[] Containers;
    [SerializeField] private Skin[] skins;
    public Skin[] Skins
    {
        get { return skins; }
    }

    private void Awake()
    {
        PopulateContainers();
    }

    private void PopulateContainers()
    {
        foreach (Skin skin in skins)
        {
            foreach (ShopContainer container in Containers)
            {
                if (skin.rarity == container.RarityName)
                { container.Skins.Add(skin); }
            }
        }
        PopulateShop();
        PopulateSkinSelector();
    }

    private void PopulateShop()
    {
        scrollingPanel.sizeDelta = new Vector2(scrollingPanel.sizeDelta.x, fullScreen.rect.height * Containers.Length);

        foreach (ShopContainer container in Containers)
        {
            var parentContainer = Instantiate(containerGroupPrefab, scrollingPanel);
            var textField = parentContainer.transform.GetChild(0).GetComponent<Text>();
            textField.text = container.RarityName.ToString();
            textField.color = container.RarityColor;

            container.Skins.ToList().ForEach(s => InstantiateShopButton(s, parentContainer));
        }       
    }

    private void InstantiateShopButton(Skin skin, Transform parentContainer)
    {
        if (skin.rarity == Rarity.Default) return;

        var button = Instantiate(shopButtonPrefab, parentContainer.transform.GetChild(1).transform);

        button.transform.GetChild(0).GetComponent<Image>().sprite = skin.skinSprite;
        var buttonText = button.transform.GetChild(1).GetComponent<Text>();
        buttonText.text = skin.isOwned ? "OWNED" : skin.skinPrice.ToString();
        button.interactable = skin.isOwned ? false : true;
        skin.skinText.TextField = buttonText;
        button.onClick.AddListener(skin.BuyItem);
        button.onClick.AddListener(delegate { SoundHandler.Instance.PlayAudio(SoundEffect.Click); });
    }

    public void PopulateSkinSelector()
    {
        foreach (Transform child in skinSelectorPanel.transform)
        {
            Destroy(child.gameObject);
        }

        var ownedSkins = skins.Where(s => s.isOwned);
        ownedSkins.ToList().ForEach(os => InstantiateSkinSelectorButton(os));
    }

    private void InstantiateSkinSelectorButton(Skin skin)
    {
        var button = Instantiate(skinSelectorButtonPrefab, skinSelectorPanel.transform);

        button.transform.GetChild(0).GetComponent<Image>().sprite = skin.skinSprite;
        button.onClick.AddListener(delegate { changeSkin.SkinChange(skin); });
        button.onClick.AddListener(delegate { SoundHandler.Instance.PlayAudio(SoundEffect.Click); });
    }
}

[System.Serializable]
public class ShopContainer
{
    public Rarity RarityName;
    public Color RarityColor;
    [HideInInspector] public List<Skin> Skins = new List<Skin>();
}

using System.Linq;
using UnityEngine;

public class ChangeSkin : MonoBehaviour
{
    [SerializeField] private Shop shop;

    private MeshRenderer _renderer;
    private const string lastSkinLiteral = "LastSkin";

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();

        if(PlayerPrefs.HasKey(lastSkinLiteral))
        {
            LastSavedSkin();
        } else
        {
            SetDefaultSkin();
        }
    }

    private void SetDefaultSkin()
    {
        var defaultSkin = shop.Skins.ToList().Find(s => s.rarity == Rarity.Default);
        PlayerPrefs.SetString(lastSkinLiteral, defaultSkin.skinName);
    }

    private void LastSavedSkin()
    {
        var lastSkin = shop.Skins.ToList().Find(s => s.skinName == PlayerPrefs.GetString(lastSkinLiteral));
        _renderer.material = lastSkin.skinMaterial;
    }

    public void SkinChange(Skin skin)
    {
        _renderer.material = skin.skinMaterial;
        PlayerPrefs.SetString(lastSkinLiteral, skin.skinName);
    }
}

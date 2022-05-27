using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private GameObject music;
    [SerializeField] private GameObject sfx;
    [Space(20)]
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Toggle sfxToggle;

    private void Awake()
    {
        CheckForKeyValue("SFX", sfxToggle, sfx);
        CheckForKeyValue("Music", musicToggle, music);
    }

    private void CheckForKeyValue(string keyName, Toggle toggleUI, GameObject go = null)
    {
        if(PlayerPrefs.HasKey(keyName))
        {
            var toggleValue = PlayerPrefs.GetInt(keyName);
            if (toggleValue == 0) toggleUI.isOn = false; else toggleUI.isOn = true;
        } else
        {
            PlayerPrefs.SetInt(keyName, toggleUI.isOn ? 1 : 0);
            if (go) go.SetActive(toggleUI.isOn);
        }
    }

    public void ToggleMusic()
    {
        PlayerPrefs.SetInt("Music", musicToggle.isOn ? 1 : 0);
        music.SetActive(musicToggle.isOn);
    }

    public void ToggleSFX()
    {
        PlayerPrefs.SetInt("SFX", sfxToggle.isOn ? 1 : 0);
        sfx.SetActive(sfxToggle.isOn);
    }
}

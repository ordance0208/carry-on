using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundEffect { Zap, Bounce, Boost, Death, Crack, CoinPickUp, Click }

public class SoundHandler : MonoBehaviour
{
    [SerializeField] private SoundEffectGroup[] sounds;
    [SerializeField] private AudioSource source;

    public static SoundHandler Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void PlayAudio(SoundEffect soundEffect)
    {
        foreach (SoundEffectGroup sound in sounds)
        { 
            if (sound.SoundEffect == soundEffect)
            {
                source.PlayOneShot(sound.Clip[Random.Range(0, sound.Clip.Length)], sound.Volume);
                break;
            }
        }
    }
}

[System.Serializable]
public class SoundEffectGroup
{
    public SoundEffect SoundEffect;
    public AudioClip[] Clip;
    [Range(0f,1f)] public float Volume = 1f;
}

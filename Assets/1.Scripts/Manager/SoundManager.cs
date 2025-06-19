using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [Header("Audio Source")]
    public AudioSource sfxSource;

    [Header("SFX Clips")]
    public List<AudioClip> sfxClips;

    private Dictionary<string, AudioClip> sfxDict;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySFX(string clipName)
    {
        if (sfxDict.TryGetValue(clipName, out var clip))
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"[SFXManager] Clip '{clipName}' not found!");
        }
    }

    public void SetVolume(float volume)
    {
        sfxSource.volume = Mathf.Clamp01(volume);
    }
}

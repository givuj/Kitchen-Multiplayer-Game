using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance{ get; private set; }
    private float volume = .3f;
    private AudioSource audioSource;
    private const string PLAY_VOLUME = "MusicVolume";
    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat(PLAY_VOLUME, .3f);
        audioSource.volume = volume;
    }
    public void ChangerVolum()
    {
        volume += .1f;
        if (volume > 1f)
        {
            volume = 0f;
        }
        audioSource.volume = volume;
        PlayerPrefs.SetFloat(PLAY_VOLUME, volume);//存储在内存中
        PlayerPrefs.Save();//存储在磁盘中
    }
    public float GetVolume()
    {
        return volume;
    }
}

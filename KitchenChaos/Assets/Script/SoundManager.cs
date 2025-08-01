using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    private const string PLAY_VOLUME = "SoundEffectsVolume";
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AutioClipRefsSO autioClipRefsSO;
    private float volume = 1f;
    private void Awake()
    {
        Instance = this;
        volume = PlayerPrefs.GetFloat(PLAY_VOLUME, 1f);
    }
    private void Start()
    {
        
        DeliveryManager.Instance.OnRecipeSuccess += Instance_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += Instance_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickSometing += Instance_OnPickSometing;
        BaseCounter.OnAnyObjectPlaceHere += ClearCounter_OnAnyObjectPlaceHere;
    }

    private void ClearCounter_OnAnyObjectPlaceHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(autioClipRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void Instance_OnPickSometing(object sender, System.EventArgs e)
    {
        PlaySound(autioClipRefsSO.objectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(autioClipRefsSO.chop, cuttingCounter.transform.position);
    }

    private void Instance_OnRecipeFailed(object sender, System.EventArgs e)
    {

        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(autioClipRefsSO.deliveryFail, deliveryCounter.transform.position);
    }

    private void Instance_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(autioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volum = 10f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volum);
    }
    private void PlaySound(AudioClip audioClip, Vector3 position, float volum2=1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volum2*volume);
    }
   
    public void ChangerVolum()
    {
        volume += .1f;
        if(volume>1f)
        {
            volume = 0f;
        }
        PlayerPrefs.SetFloat(PLAY_VOLUME,volume);//存储在内存中
        PlayerPrefs.Save();//存储在磁盘中
    }
    public float GetVolume()
    {
        return volume;
    }

}

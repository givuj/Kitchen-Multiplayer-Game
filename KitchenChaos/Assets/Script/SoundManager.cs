using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AutioClipRefsSO autioClipRefsSO;
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += Instance_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += Instance_OnRecipeFailed; ;
    }

    private void Instance_OnRecipeFailed(object sender, System.EventArgs e)
    {
      
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(autioClipRefsSO.deliveryFail,deliveryCounter.transform.position);
    }

    private void Instance_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(autioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volum = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volum);
    }
    private void PlaySound(AudioClip audioClip, Vector3 position, float volum = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volum);
    }
}

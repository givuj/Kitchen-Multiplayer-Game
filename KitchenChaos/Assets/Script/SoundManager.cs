using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AutioClipRefsSO autioClipRefsSO;
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
        PlaySound(autioClipRefsSO.objectDrop,baseCounter.transform.position);
    }

    private void Instance_OnPickSometing(object sender, System.EventArgs e)
    {
        PlaySound(autioClipRefsSO.objectPickup,Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(autioClipRefsSO.chop, cuttingCounter.transform.position);
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

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volum = 10f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volum);
    }
    private void PlaySound(AudioClip audioClip, Vector3 position, float volum )
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volum);
    }
}

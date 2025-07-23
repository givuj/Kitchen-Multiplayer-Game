using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject sizzlingParticles;
    [SerializeField] private GameObject stoveOnVisual;
    [SerializeField] private StoveCounter stoveCounter;
    public void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
       if(e.state == StoveCounter.State.Frying|| e.state == StoveCounter.State.Fried)
        {
            stoveOnVisual.SetActive(true);
            sizzlingParticles.SetActive(true); 
        }
        else
        {
            stoveOnVisual.SetActive(false);
            sizzlingParticles.SetActive(false);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectSO plateKichenObjectSO;
    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int platesSpawnedAmout;
    private int platesSpawnedAmoutMax = 4;
    
    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if(spawnPlateTimer>spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;
            if (platesSpawnedAmout < platesSpawnedAmoutMax)
            {
                platesSpawnedAmout++;
                OnPlateSpawned?.Invoke(this,EventArgs.Empty);
            }
        }
    }
    public override void Interact(Player player)
    {
        if(!player.HasKitchenObject())//手中没东西
        {
            if(platesSpawnedAmout>0)
            {
                platesSpawnedAmout--;
                KitchenObject.SpawnKitchenObject(plateKichenObjectSO, player);
                OnPlateRemoved?.Invoke(this,EventArgs.Empty);
            }
        }
    }
}

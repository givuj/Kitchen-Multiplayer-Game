using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    // Start is called before the first frame update
    public override void Interact(Player player)
    {
        if(player.HasKitchenObject())
        {
            if(player.GetKitchenObject().TryGetPlate(out PlateKichenObject plateKichenObject))
            {
                player.GetKitchenObject().DestorySelf();
            }
        }
    }
}

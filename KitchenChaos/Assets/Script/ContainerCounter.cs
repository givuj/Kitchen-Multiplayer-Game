using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;//通过kitchenObjectSO中prefab拿到对象

    public override void Interact(Player player)
    {
        //这个是从食物柜中拿食物

        Transform kitchenObjectTranform = Instantiate(kitchenObjectSO.prefab);

        kitchenObjectTranform.GetComponent<KitchenObject>().SetKitchenObjectParant(player);
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);


    }

}

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
        if (!player.HasKitchenObject())//手中没有食物才能从储物柜中拿食物
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);//食物放到人物手上
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Debug.Log("手上有食物");
        }
    }

}

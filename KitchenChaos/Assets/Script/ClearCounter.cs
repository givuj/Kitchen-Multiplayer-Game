using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
   

   
    public override void Interact(Player player)
    {
        //如果柜台没有物品就能放东西
        if(!HasKitchenObject())
        {
            if (player.HasKitchenObject())//人物手中有食物
            {
                player.GetKitchenObject().SetKitchenObjectParant(this);
            }
        }
        //如果柜台有物品
        else
        {
            if(player.HasKitchenObject())//人物手中没有食物
            {

            }
            else//手中没有食物就可以拿柜台上的食物
            {
                GetKitchenObject().SetKitchenObjectParant(player);
            }
        }
    }

}

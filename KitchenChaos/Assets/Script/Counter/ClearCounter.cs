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
            else
            {

            }
        }
        //如果柜台有物品
        else
        {
            if(player.HasKitchenObject())//人物手中有东西
            {
                if(player.GetKitchenObject().TryGetPlate(out PlateKichenObject plateKichenObject))//人物手中是盘子
                {
                  
                    if (plateKichenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))//将食物放入盘子中,不能重复放入
                    {
                        GetKitchenObject().DestorySelf();
                    }
                   
                }
                else//手中的不是盘子，手中是食物
                {
                    if(GetKitchenObject().TryGetPlate(out  plateKichenObject))//如果柜子上盘子，并且手中有食物
                    {
                        Debug.Log("桌子上是盘子");
                        if (plateKichenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))//将食物放入盘子中,不能重复放入
                        {
                            Debug.Log("1");
                            player.GetKitchenObject().DestorySelf();
                        }
                    }
                }
            }
            else//手中没有东西就可以拿柜台上的物品
            {
                GetKitchenObject().SetKitchenObjectParant(player);
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    // Start is called before the first frame update
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    public override void Interact(Player player)
    {
        //如果柜台没有物品就能放东西
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())//人物手中有食物
            {
                player.GetKitchenObject().SetKitchenObjectParant(this);
            }
        }
        //如果柜台有物品
        else
        {
            if (player.HasKitchenObject())//人物手中没有食物
            {

            }
            else//手中没有食物就可以拿柜台上的食物
            {
                GetKitchenObject().SetKitchenObjectParant(player);
            }
        }
    }


    public override void InteractF(Player player)
    {
        if(HasKitchenObject())
        {
            KitchenObjectSO outputkitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            GetKitchenObject().DestorySelf();
            
            KitchenObject.SpawnKitchenObject(outputkitchenObjectSO, this);
            
        }
    }

    //将食物剁碎
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach(CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if(cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO.output;
            }
        }
        return null;
    }
}

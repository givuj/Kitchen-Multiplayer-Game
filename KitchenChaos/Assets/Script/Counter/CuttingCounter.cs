using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter,IHasProgress
{
    // Start is called before the first frame update
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }
    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    private int cuttingProgress=0;//切菜的进程

    public override void Interact(Player player)
    {
        //如果柜台没有物品就能放东西
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())//人物手中有食物
            {
                
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))//看能不能切，有些食物是不能切的，就不能放在柜台上
                {
                    cuttingProgress = 0;
                    player.GetKitchenObject().SetKitchenObjectParant(this);
                  
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    { progressNormalized=(float)cuttingProgress/cuttingRecipeSO.cuttingProgressMax });

                }
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
        if(HasKitchenObject()&&HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))// 切一次不能切第二次
        {
            cuttingProgress++;
            OnCut?.Invoke(this,EventArgs.Empty);
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            KitchenObjectSO outputkitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
         
            OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
            { progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax });
            if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                GetKitchenObject().DestorySelf();

                KitchenObject.SpawnKitchenObject(outputkitchenObjectSO, this);
            }
        }
    }
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }
    //将食物剁碎
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        if(cuttingRecipeSO!=null)
        {
            return cuttingRecipeSO.output;
        }
        return null;
    }
    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}

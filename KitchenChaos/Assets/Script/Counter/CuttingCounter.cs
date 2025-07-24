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
    private int cuttingProgress=0;//�в˵Ľ���

    public override void Interact(Player player)
    {
        //�����̨û����Ʒ���ܷŶ���
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())//����������ʳ��
            {
                
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))//���ܲ����У���Щʳ���ǲ����еģ��Ͳ��ܷ��ڹ�̨��
                {
                    cuttingProgress = 0;
                    player.GetKitchenObject().SetKitchenObjectParant(this);
                  
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    { progressNormalized=(float)cuttingProgress/cuttingRecipeSO.cuttingProgressMax });

                }
            }
        }
        //�����̨����Ʒ
        else
        {
            if (player.HasKitchenObject())//��������û��ʳ��
            {

            }
            else//����û��ʳ��Ϳ����ù�̨�ϵ�ʳ��
            {
                GetKitchenObject().SetKitchenObjectParant(player);
            }
        }
    }


    public override void InteractF(Player player)
    {
        if(HasKitchenObject()&&HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))// ��һ�β����еڶ���
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
    //��ʳ�����
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

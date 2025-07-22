using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    // Start is called before the first frame update
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    public override void Interact(Player player)
    {
        //�����̨û����Ʒ���ܷŶ���
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())//����������ʳ��
            {
                player.GetKitchenObject().SetKitchenObjectParant(this);
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
        if(HasKitchenObject())
        {
            KitchenObjectSO outputkitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            GetKitchenObject().DestorySelf();
            
            KitchenObject.SpawnKitchenObject(outputkitchenObjectSO, this);
            
        }
    }

    //��ʳ�����
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

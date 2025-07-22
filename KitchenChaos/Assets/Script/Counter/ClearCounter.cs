using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
   

   
    public override void Interact(Player player)
    {
        //�����̨û����Ʒ���ܷŶ���
        if(!HasKitchenObject())
        {
            if (player.HasKitchenObject())//����������ʳ��
            {
                player.GetKitchenObject().SetKitchenObjectParant(this);
            }
        }
        //�����̨����Ʒ
        else
        {
            if(player.HasKitchenObject())//��������û��ʳ��
            {

            }
            else//����û��ʳ��Ϳ����ù�̨�ϵ�ʳ��
            {
                GetKitchenObject().SetKitchenObjectParant(player);
            }
        }
    }

}

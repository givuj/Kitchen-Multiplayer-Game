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
            else
            {

            }
        }
        //�����̨����Ʒ
        else
        {
            if(player.HasKitchenObject())//���������ж���
            {
                if(player.GetKitchenObject().TryGetPlate(out PlateKichenObject plateKichenObject))//��������������
                {
                  
                    if (plateKichenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))//��ʳ�����������,�����ظ�����
                    {
                        GetKitchenObject().DestorySelf();
                    }
                   
                }
                else//���еĲ������ӣ�������ʳ��
                {
                    if(GetKitchenObject().TryGetPlate(out  plateKichenObject))//������������ӣ�����������ʳ��
                    {
                        Debug.Log("������������");
                        if (plateKichenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))//��ʳ�����������,�����ظ�����
                        {
                            Debug.Log("1");
                            player.GetKitchenObject().DestorySelf();
                        }
                    }
                }
            }
            else//����û�ж����Ϳ����ù�̨�ϵ���Ʒ
            {
                GetKitchenObject().SetKitchenObjectParant(player);
            }
        }
    }

}

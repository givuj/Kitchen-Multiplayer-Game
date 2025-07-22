using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;//ͨ��kitchenObjectSO��prefab�õ�����

    public override void Interact(Player player)
    {
        //����Ǵ�ʳ�������ʳ��
        if (!player.HasKitchenObject())//����û��ʳ����ܴӴ��������ʳ��
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);//ʳ��ŵ���������
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Debug.Log("������ʳ��");
        }
    }

}

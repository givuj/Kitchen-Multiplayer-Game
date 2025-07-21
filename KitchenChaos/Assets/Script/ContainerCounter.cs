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

        Transform kitchenObjectTranform = Instantiate(kitchenObjectSO.prefab);

        kitchenObjectTranform.GetComponent<KitchenObject>().SetKitchenObjectParant(player);
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);


    }

}

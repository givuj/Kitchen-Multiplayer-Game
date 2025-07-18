using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour,IKitchenObjectParant
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private ClearCounter scendClearCounter;
    // Start is called before the first frame update
    private KitchenObject kitchenObject;

   
    public void Interact(Player player)
    {
        //ͨ��kitchenObjectSO���ʳ��ʵ��prefabͨ��ʵ���е�kitchenObject�ű�����������
        if (kitchenObject == null)
        {

            Transform kitchenObjectTranform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);

            kitchenObjectTranform.GetComponent<KitchenObject>().SetKitchenObjectParant(this);

        }
        //׿������ʳ����ǰ�e�ˣ�ʳ����Ҫ����ɫ������
        else
        {
            
            if (Player.Instance.GetKitchenObject()==null)
            {
                kitchenObject.SetKitchenObjectParant(player);
            }
        }
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return  kitchenObject != null;
    }
}

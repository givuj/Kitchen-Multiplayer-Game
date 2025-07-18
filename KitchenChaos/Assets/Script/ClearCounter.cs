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
        //通过kitchenObjectSO获得食物实体prefab通过实体中的kitchenObject脚本获得相关属性
        if (kitchenObject == null)
        {

            Transform kitchenObjectTranform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);

            kitchenObjectTranform.GetComponent<KitchenObject>().SetKitchenObjectParant(this);

        }
        //卓子上有食物，但是按e了，食物需要到角色的手上
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

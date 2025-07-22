using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//父类
public class BaseCounter : MonoBehaviour,IKitchenObjectParant
{

    [SerializeField] private Transform counterTopPoint;
    private KitchenObject kitchenObject;
    public virtual void Interact(Player player)
    {

    }
    public virtual void InteractF(Player player)
    {

    }

    //接口实现
    public Transform GetKitchenObjectFollowTransform()//获得顶点的位置
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
        return kitchenObject != null;
    }
}

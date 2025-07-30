using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//父类
public class BaseCounter : MonoBehaviour,IKitchenObjectParant
{

    [SerializeField] private Transform counterTopPoint;
    protected KitchenObject kitchenObject;
    public static event EventHandler OnAnyObjectPlaceHere;
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
        if(kitchenObject!=null)
        {
            OnAnyObjectPlaceHere?.Invoke(this,EventArgs.Empty);
        }
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

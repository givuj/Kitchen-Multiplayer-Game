using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//相当于给预制体封装一些属性
public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;//通过这个获得食物的名字和精灵
    private IKitchenObjectParant kitchenObjectParant;//看食物在哪个柜台，同时也是为食物变换柜台做准备
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    //设置食物放在哪里
    public void SetKitchenObjectParant(IKitchenObjectParant kitchenObjectParant)//多态实现给的其实是接口对象，只是ClearCounter或player实现了IKitchenObjectParant接口
    {
        if (this.kitchenObjectParant != null)//清除柜台之前的食物,之前柜台的食物数据还在
        {
            this.kitchenObjectParant.ClearKitchenObject();
        }
        this.kitchenObjectParant = kitchenObjectParant;
        if(kitchenObjectParant.HasKitchenObject())//如果新柜台还有食物的话不能放置
        {
            Debug.LogError("已经有食物了");
        }
        else
        {
            kitchenObjectParant.SetKitchenObject(this);
            transform.parent = kitchenObjectParant.GetKitchenObjectFollowTransform();//这段代码还会导致位置的改变
            transform.localPosition = Vector3.zero;
        }
    }
    public IKitchenObjectParant GetKitchenObjectParant()
    {
        return kitchenObjectParant;
    }
    public void DestorySelf()
    {
        kitchenObjectParant.ClearKitchenObject();
        Destroy(gameObject);

    }

    public bool TryGetPlate(out PlateKichenObject plateKichenObject)
    {
        if (this is PlateKichenObject)
        {
            plateKichenObject = this as PlateKichenObject;
            return true;
        }
        else
        {
            plateKichenObject = null;
            return false;
        }

         
    }

    //转移食物,第一个属性是食物，第二个属性是counter或者player
    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSo,IKitchenObjectParant kitchenObjectParant)
    {
        Transform KitchenObjectTransform = Instantiate(kitchenObjectSo.prefab);
        KitchenObject kitchenObject = KitchenObjectTransform.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjectParant(kitchenObjectParant);
        return kitchenObject;
    }
}

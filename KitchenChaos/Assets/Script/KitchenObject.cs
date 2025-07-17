using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//相当于给预制体封装一些属性
public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;//通过这个获得食物的名字和精灵
    private ClearCounter clearCounter;//看食物在哪个柜台，同时也是为食物变换柜台做准备
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
    public void SetClearCounter(ClearCounter clearCounter)
    {
        if (this.clearCounter != null)//清除柜台之前的食物,之前柜台的食物数据还在
        {
            Debug.Log("1");
            this.clearCounter.ClearKitchenObject();
        }
        this.clearCounter = clearCounter;
        if(clearCounter.HasKitchenObject())//如果新柜台还有食物的话不能放置
        {
            Debug.LogError("已经有食物了");
        }
        clearCounter.SetKitchenObject(this);
        transform.parent = clearCounter.GetKitchenObjectFollowTransform();//这段代码还会导致位置的改变
        transform.localPosition = Vector3.zero;
    }
    public ClearCounter GetClearCounter()
    {
        return clearCounter;
    }
}

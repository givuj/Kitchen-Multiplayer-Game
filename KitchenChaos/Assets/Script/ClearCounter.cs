using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private ClearCounter scendClearCounter;
    // Start is called before the first frame update
    private KitchenObject kitchenObject;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (kitchenObject != null)
            {
                kitchenObject.SetClearCounter(scendClearCounter);
            }
        }

    }
    public void Interact()
    {
        //通过kitchenObjectSO获得食物实体prefab通过实体中的kitchenObject脚本获得相关属性
        if (kitchenObject == null)
        {

            Transform kitchenObjectTranform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);

            kitchenObjectTranform.GetComponent<KitchenObject>().SetClearCounter(this);

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
        Debug.Log("2");
        return  kitchenObject != null;
    }
}

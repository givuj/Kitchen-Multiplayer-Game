using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKichenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO KitchenObjectSO;
    }
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;//盘子中能放入的食材
    // Start is called before the first frame update
    private List<KitchenObjectSO> kitchenObjectSOList;
    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)//盘子中添加食材，不能重复添加
    {
        if(!validKitchenObjectSOList.Contains(kitchenObjectSO))//不能放入这个食材，这个食材没有配方
        {
            return false;
        }
        if(kitchenObjectSOList.Contains(kitchenObjectSO))//已经放过了
        {
            return false;
        }
        else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);
            OnIngredientAdded?.Invoke(this,new OnIngredientAddedEventArgs { KitchenObjectSO = kitchenObjectSO});
            return true;
        }
    }
    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}

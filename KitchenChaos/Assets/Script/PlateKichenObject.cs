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
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;//�������ܷ����ʳ��
    // Start is called before the first frame update
    private List<KitchenObjectSO> kitchenObjectSOList;
    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)//���������ʳ�ģ������ظ����
    {
        if(!validKitchenObjectSOList.Contains(kitchenObjectSO))//���ܷ������ʳ�ģ����ʳ��û���䷽
        {
            return false;
        }
        if(kitchenObjectSOList.Contains(kitchenObjectSO))//�Ѿ��Ź���
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

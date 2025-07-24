using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKichenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;//�������ܷ����ʳ��
    // Start is called before the first frame update
    private List<KitchenObjectSO> kitchenObjectSOList;
    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)//���������ʳ�ģ������ظ����
    {
        if(!validKitchenObjectSOList.Contains(kitchenObjectSO))//���ܷ������ʳ��
        {
            return false;
        }
        if(kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }
        else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);
            return true;
        }
    }
}

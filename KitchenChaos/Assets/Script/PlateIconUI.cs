using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private PlateKichenObject plateKichenObject;
    [SerializeField] private Transform iconTemplate;
    private void Start()
    {
        plateKichenObject.OnIngredientAdded += PlateKichenObject_OnIngredientAdded;
    }

    private void PlateKichenObject_OnIngredientAdded(object sender, PlateKichenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }
    private void UpdateVisual()
    {
        foreach(Transform child in transform)
        {
            if (child == iconTemplate)
            {
                iconTemplate.gameObject.SetActive(false);
               continue;
            }
            Destroy(child.gameObject);
        }
        foreach (KitchenObjectSO kitchenObjectSO in plateKichenObject.GetKitchenObjectSOList())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
           
            iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSOSprite(kitchenObjectSO);
        }
    }
}

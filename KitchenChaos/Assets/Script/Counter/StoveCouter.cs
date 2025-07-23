using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter
{


    private enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;
    private float fryingTimer = 0;//�⿾��Ľ���
    private float bunringTimer = 0;//�⿾���Ľ���
    private State state;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;
    private void Start()
    {
        state = State.Idle;
    }
    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Frying:
                fryingTimer += Time.deltaTime;
                if (fryingTimer >= fryingRecipeSO.fryingTimerMax)
                {


                    GetKitchenObject().DestorySelf();
                    KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                    state = State.Fried;
                    burningRecipeSO = GetburningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                
                    bunringTimer = 0f;
                }
                break;
            case State.Fried:
                bunringTimer += Time.deltaTime;
                if (bunringTimer >= burningRecipeSO.BurningTimerMax)
                {


                    GetKitchenObject().DestorySelf();
                    KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
                    state = State.Burned;

                }
                break;
            case State.Burned:
                break;
        }


    }
    public override void Interact(Player player)
    {
        //�����̨û����Ʒ���ܷŶ���
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())//����������ʳ��
            {

                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))//���ܲ�����
                {
                    fryingTimer = 0;
                    fryingRecipeSO = GetfryingRecipeSOWithInput(player.GetKitchenObject().GetKitchenObjectSO());
                    player.GetKitchenObject().SetKitchenObjectParant(this);//���������¯����
                    state = State.Frying;


                }
            }
        }
        //�����̨����Ʒ
        else
        {
            if (player.HasKitchenObject())//��������û��ʳ��
            {

            }
            else//����û��ʳ��Ϳ����ù�̨�ϵ�ʳ��
            {
                GetKitchenObject().SetKitchenObjectParant(player);
            }
        }
    }

    //��û���䷽
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetfryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }


    //ͨ��ʳ�����䷽
    private FryingRecipeSO GetfryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }

    private BurningRecipeSO GetburningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }
}

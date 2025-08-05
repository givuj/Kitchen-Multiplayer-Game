using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter,IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    public enum State
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
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax });
                if (fryingTimer >= fryingRecipeSO.fryingTimerMax)
                {


                    GetKitchenObject().DestorySelf();
                    KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                    state = State.Fried;
                    burningRecipeSO = GetburningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                
                    bunringTimer = 0f;
                    OnStateChanged?.Invoke(this,new OnStateChangedEventArgs { state = state});
                }
                break;
            case State.Fried:
                bunringTimer += Time.deltaTime;
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = bunringTimer / burningRecipeSO.BurningTimerMax });
                if (bunringTimer >= burningRecipeSO.BurningTimerMax)
                {


                    GetKitchenObject().DestorySelf();
                    KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
                    state = State.Burned;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f });

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
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax });
                }
            }

        }
        //�����̨����Ʒ
        else
        {
            if (player.HasKitchenObject())//����������ʳ��
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKichenObject plateKichenObject))//��������������
                {

                    if (plateKichenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))//��ʳ�����������,�����ظ�����
                    {
                        GetKitchenObject().DestorySelf();
                        state = State.Idle;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f });
                    }


                }
            }
            else//����û��ʳ��Ϳ����ù�̨�ϵ�ʳ��
            {
                GetKitchenObject().SetKitchenObjectParant(player);
                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f });
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
    public bool IsFried()
    {
        return state == State.Fried;
    }
}

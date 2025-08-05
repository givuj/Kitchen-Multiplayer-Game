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
    private float fryingTimer = 0;//肉烤熟的进程
    private float bunringTimer = 0;//肉烤焦的进程
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
        //如果柜台没有物品就能放东西
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())//人物手中有食物
            {

                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))//看能不能烧
                {
                    fryingTimer = 0;
                    fryingRecipeSO = GetfryingRecipeSOWithInput(player.GetKitchenObject().GetKitchenObjectSO());
                    player.GetKitchenObject().SetKitchenObjectParant(this);//把肉饼放在炉子上
                    state = State.Frying;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax });
                }
            }

        }
        //如果柜台有物品
        else
        {
            if (player.HasKitchenObject())//人物手中有食物
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKichenObject plateKichenObject))//人物手中是盘子
                {

                    if (plateKichenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))//将食物放入盘子中,不能重复放入
                    {
                        GetKitchenObject().DestorySelf();
                        state = State.Idle;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f });
                    }


                }
            }
            else//手中没有食物就可以拿柜台上的食物
            {
                GetKitchenObject().SetKitchenObjectParant(player);
                state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progressNormalized = 0f });
            }

        }
    }

    //有没有配方
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetfryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }


    //通过食物获得配方
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

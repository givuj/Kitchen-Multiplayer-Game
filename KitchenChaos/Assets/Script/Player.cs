using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCounter
{
    public static Player Instance { get; private set; }
 

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public event EventHandler OnPickSometing;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }


    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterslayerMask;
    

    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    //private KitchenObject kitchenObject;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("error");
        }
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractActionF += GameInput_OnInteractActionF;
    }

    private void GameInput_OnInteractActionF(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) { return; }
        if (selectedCounter != null)
        {
            selectedCounter.InteractF(this);
        }
    }


    //按e触发的事件,触发的是ClearCounter对象的交互
    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) { return; }
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
        
    }

    //碰撞检测后获得碰撞物体的对象
    private void HandleInteraction()
    {
        Vector2 inputVector = gameInput.GameMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);//方向
        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;

        }
        float interactDistance = 2f;

        Debug.DrawRay(transform.position, lastInteractDir * interactDistance, Color.red, 1f);
        //靠近物体，物体变得虚化，离开物体虚化消失

        //1.检测到物体
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, counterslayerMask))
        {
            //获得到了相应类型的物体
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                //是新物体
                if (baseCounter != selectedCounter)
                {

                    //使其变虚化
                    SetSelectedCounter(baseCounter);

                }

            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        //2.没有检测到物体，或者离开物体
        else
        {
            SetSelectedCounter(null);
        }


    }

    //碰撞检查
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GameMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        //人物的碰撞检查
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        if (!canMove)
        {
            Vector3 moveDirx = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDir.x!=0&&!Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirx, moveDistance);
            if (canMove)
            {
                moveDir = moveDirx;
            }
            else
            {
                Vector3 moveDirz = new Vector3(0, 0, moveDir.z);
                canMove = moveDir.z!=0&&!Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirz, moveDistance);
                if (canMove)
                {
                    moveDir = moveDirz;
                }

            }
        }
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }
        isWalking = (moveDir != Vector3.zero);

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }
    public bool IsWalking()
    {
        return isWalking;
    }
   
    private void SetSelectedCounter(BaseCounter selectedCounterd)
    {
        this.selectedCounter = selectedCounterd;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = selectedCounterd });
    }

    //接口实现
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if(kitchenObject!=null)
        {
            OnPickSometing?.Invoke(this,EventArgs.Empty);
        }
    }


}

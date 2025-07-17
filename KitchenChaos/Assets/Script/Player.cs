using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }


    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }


    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterslayerMask;

    private bool isWalking;
    private Vector3 lastInteractDir;
    private ClearCounter selectedCounter;
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
    }


    //��e�������¼�,��������ClearCounter����Ľ���
    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact();
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    //��ײ��������ײ����Ķ���
    private void HandleInteraction()
    {
        Vector2 inputVector = gameInput.GameMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);//����
        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;

        }
        float interactDistance = 2f;

        Debug.DrawRay(transform.position, lastInteractDir * interactDistance, Color.red, 1f);
        //�������壬�������黯���뿪�����黯��ʧ

        //1.��⵽����
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, counterslayerMask))
        {
            //��õ�����Ӧ���͵�����
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                //��������
                if (clearCounter != selectedCounter)
                {

                    //ʹ����黯
                    SetSelectedCounter(clearCounter);

                }

            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        //2.û�м�⵽���壬�����뿪����
        else
        {
            SetSelectedCounter(null);
        }


    }

    //��ײ���
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GameMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        //�������ײ���
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = .7f;
        float playerHeight = 2f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        if (!canMove)
        {
            Vector3 moveDirx = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirx, moveDistance);
            if (canMove)
            {
                moveDir = moveDirx;
            }
            else
            {
                Vector3 moveDirz = new Vector3(0, 0, moveDir.z);
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirz, moveDistance);
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
    private void SetSelectedCounter(ClearCounter selectedCounterd)
    {
        this.selectedCounter = selectedCounterd;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs { selectedCounter = selectedCounterd });
    }
}

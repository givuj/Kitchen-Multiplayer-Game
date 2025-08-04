using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    private const string POPUP = "Popup";
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Color successColor;
    [SerializeField] private Color failedColor;
    [SerializeField] private Sprite failedSprite;
    [SerializeField] private Sprite successSprite;
    private Animator animator;
    private float duringTimeMax = 1f;
    private float duringTime = 0f;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += Instance_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += Instance_OnRecipeFailed;
        gameObject.SetActive(false);
       
    }

    private void Instance_OnRecipeFailed(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(POPUP);
        backgroundImage.color = failedColor;
        iconImage.sprite = failedSprite;
        messageText.text = "提交失败";
        while(duringTime>duringTimeMax)
        {
            gameObject.SetActive(false);
            duringTime += Time.deltaTime;
            
        }
        duringTime = 0f;

    }

    private void Instance_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(POPUP);
        backgroundImage.color = successColor;
        iconImage.sprite = successSprite;
        messageText.text = "提交成功";
        while (duringTime >duringTimeMax)
        {
            gameObject.SetActive(false);
            duringTime += Time.deltaTime;
        }
        duringTime = 0f;
    }
}

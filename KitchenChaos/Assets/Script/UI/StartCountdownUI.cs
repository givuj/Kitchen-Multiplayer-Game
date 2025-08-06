using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;

public class StartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    private void Start()
    {
        GameManager.Instance.OnStateChanged += Instance_OnStateChanged;
    }

    private void Instance_OnStateChanged(object sender, System.EventArgs e)
    {
        if(GameManager.Instance.IsCountdownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    private void Update()
    {
        countdownText.text = Mathf.Ceil(GameManager.Instance.CountdownToStartTimer()) .ToString();
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}

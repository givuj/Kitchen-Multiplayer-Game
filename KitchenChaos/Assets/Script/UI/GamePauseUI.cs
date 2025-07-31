using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resumeButton;
    private void Awake()
    {
        resumeButton.onClick.AddListener(() => { GameManager.Instance.PauseGame(); });
        mainMenuButton.onClick.AddListener(() => { Loader.Load(Loader.Scene.MainMenuScene); });
    }
    private void Start()
    {
        //订阅事件时，单场景切换时，GamePauseUI会被销毁，但是GameManager为单例跨场景使用，仍然保留事件的订阅者，就是GamePauseUI对象的指针，所以会导致指针泄露，要退订阅
        GameManager.Instance.OnGamePause += Instance_OnGamePause;
        GameManager.Instance.OnGameUnPause += Instance_OnGameUnPause;
        Hide();
    }
  

    private void Instance_OnGameUnPause(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void Instance_OnGamePause(object sender, System.EventArgs e)
    {
        Show();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionButton;
    private void Awake()
    {
        resumeButton.onClick.AddListener(() => { GameManager.Instance.PauseGame(); });
        mainMenuButton.onClick.AddListener(() => { Loader.Load(Loader.Scene.MainMenuScene); });
        optionButton.onClick.AddListener(() => { OptionUI.Instance.Show(); });
    }
    private void Start()
    {
        //�����¼�ʱ���������л�ʱ��GamePauseUI�ᱻ���٣�����GameManagerΪ�����糡��ʹ�ã���Ȼ�����¼��Ķ����ߣ�����GamePauseUI�����ָ�룬���Իᵼ��ָ��й¶��Ҫ�˶���
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

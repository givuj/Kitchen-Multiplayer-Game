using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public static OptionUI Instance { get; private set; }
    [SerializeField] private Button soundEffectsButtion;
    [SerializeField] private Button musicButtion;
    [SerializeField] private Button closeButtion;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;
    private void Awake()
    {
        Instance = this;
        soundEffectsButtion.onClick.AddListener(() => { SoundManager.Instance.ChangerVolum();
                                                        UpdateVisual();                    });
        musicButtion.onClick.AddListener(() => { MusicManager.Instance.ChangerVolum();
                                                        UpdateVisual();                    });
        closeButtion.onClick.AddListener(() => {
            Hide();
        });
    }
    private void Start()
    {
        GameManager.Instance.OnGameUnPause += Instance_OnGameUnPause;
        UpdateVisual();
        Hide();
    }

    private void Instance_OnGameUnPause(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        
        soundEffectsText.text = "Sound Effects:" + Mathf.Round(SoundManager.Instance.GetVolume()*10f);  
        musicText.text = "Music :" + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{
    [SerializeField] private ClearCounter clearCounter;
    [SerializeField] private GameObject VisualGameObject;
    // Start is called before the first frame update
    void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
        
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
      
        if (e.selectedCounter==clearCounter)
        {
          
            Show();
        }
        else
        {
         
            Hide();
        }
    }
    private void Show()
    {
        VisualGameObject.SetActive(true);
    }
    private void Hide()
    {
        VisualGameObject.SetActive(false);
    }
    // Update is called once per frame

}

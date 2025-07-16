using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatior : MonoBehaviour
{
    [SerializeField] private Player player;
    private Animator animatior;
    private void Awake()
    {

        animatior = GetComponent<Animator>();
        
    }
    private void Update()
    {
        animatior.SetBool("IsWalking", player.IsWalking());
       
    }
}

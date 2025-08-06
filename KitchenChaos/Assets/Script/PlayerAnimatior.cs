using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerAnimatior : NetworkBehaviour
{
    [SerializeField] private Player player;
    private Animator animatior;


    private void Awake()
    {

        animatior = GetComponent<Animator>();
        
    }
    private void Update()
    {
        if(!IsOwner)
        {
            return;
        }
        animatior.SetBool("IsWalking", player.IsWalking());
       
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    Animator playerAni;
    private void Start()
    {
        playerAni = GetComponent<Animator>();
    }
    public void JumpFalse()
    {
        playerAni.SetBool("jumpOn", false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    /*地面接地をしているかを判定*/

    private string groundTag = "Ground";
    private bool isGround = false;
    private bool  isGroundStay, isGroundExit,isGroundEnter;

    public PlayerController ctrl;
    
    public bool IsGround()
    {
        if (isGroundEnter || isGroundStay)
        {
            isGround = true;
        }
        else if (isGroundExit)
        {
            isGround = false;
        }

        isGroundEnter = false;
        isGroundStay = false;
        isGroundExit = false;

        return isGround;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == groundTag || collision.tag.Contains(groundTag))
        {
            isGroundEnter = true;
            ctrl.PlayerAnim("jumpOn", false);
            ctrl.PlayerAnim("walkOn", true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == groundTag || collision.tag.Contains(groundTag))
        {
            isGroundStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == groundTag || collision.tag.Contains(groundTag))
        {
            isGroundExit = true;
        }
    }
}

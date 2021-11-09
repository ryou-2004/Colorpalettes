using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelMove : MonoBehaviour
{
    private GameObject player;
    private GameObject groundObj;
    private GroundCheck groundCheck;
    private Vector3 playerPos;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        groundObj = GameObject.Find("GroundCheck");
        groundCheck = groundObj.GetComponent<GroundCheck>();
    }

    private void Update()
    {
        if (groundCheck.IsGround())
        {
            gameObject.transform.position = player.transform.position;
        }
    }
}

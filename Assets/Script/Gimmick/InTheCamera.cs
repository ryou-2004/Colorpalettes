using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InTheCamera : MonoBehaviour
{
    private Rigidbody2D rb;
    private void Awake()
    {
       rb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (this.gameObject.GetComponent<Renderer>().isVisible)//カメラ内に入ったとき
        {
            rb.bodyType = RigidbodyType2D.Dynamic;//重力がかかるようにする
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbCheck : MonoBehaviour
{
    private string climbTag = "ClimbGround";
    public Animator plaAni;
    private float x;
    GameObject player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    public static bool IsClimb { set; get; } = false;
    public static bool IsClimbNow { set; get; } = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&IsClimb)//壁と当たっていてジャンプしたとき
        {
            IsClimbNow = true;
            IsClimb = false;
            plaAni.SetBool("climbOn", true);//アニメーションスタート
            StartCoroutine(EndAni(3.35f));//アニメーション終了時にFalseにする
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(climbTag))
        {
            IsClimb = true;
            if (collision.gameObject.transform.position.x < transform.position.x)
            {
                x = -0.47725f;
            }//右にいるとき
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                x = 0.47725f;
            }
        }//当たった時
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(climbTag))
        {
            IsClimb = false;
        }//離れたとき
    }
    IEnumerator EndAni(float time)
    {
        yield return new WaitForSeconds(time);
        IsClimb = IsClimbNow = false;
        plaAni.SetBool("climbOn", false);
        //Transform transform = player.transform;
        //Vector3 pos = transform.position;
        Vector3 pos = player.transform.position;
        pos.x += x;
        pos.y += 1.556f;
        player.transform.position = pos;
    }
}

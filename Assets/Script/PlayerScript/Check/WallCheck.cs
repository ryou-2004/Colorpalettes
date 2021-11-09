using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    public float pushSpeed;
    public Animator plaAni;
    private string wall = "Wall";
    private bool hit;
    private GameObject collisionObj;
    private Rigidbody2D rb;
    public static bool isWall { set; get; }
    private void Update()
    {
        if (hit)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                plaAni.SetBool("touchOn", true);
                if (PositionCheck())
                {
                    if (Input.GetKey(KeyCode.A))
                    {
                        ObjMove(-1);
                        plaAni.SetBool("pushOn", true);
                    }//押す
                    else if (Input.GetKey(KeyCode.D))
                    {
                        //ObjMove(1);
                        Debug.Log("引いてる");
                    }//引く
                }
                else
                {
                    if (Input.GetKey(KeyCode.A))
                    {
                        //ObjMove(-1);
                        Debug.Log("引いてる");
                    }//引く
                    else if (Input.GetKey(KeyCode.D))
                    {
                        ObjMove(1);
                        plaAni.SetBool("pushOn", true);
                    }//押す
                }

                if(Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D))
                {
                    rb.bodyType = RigidbodyType2D.Dynamic;
                }
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                plaAni.SetBool("touchOn", false);
                plaAni.SetBool("pushOn", false);
                plaAni.SetBool("standOn", true);
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.velocity = Vector2.zero;
            }
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.velocity = Vector2.zero;
                plaAni.SetBool("pushOn", false);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains(wall))
        {
            collisionObj = collision.gameObject;
            rb = collisionObj.GetComponent<Rigidbody2D>();
            isWall = true;
            hit = true;
            plaAni.SetBool("walkOn", false);
            plaAni.SetBool("dashOn", false);
            plaAni.SetBool("standOn", false);
        }//アニメーションスタート
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Contains(wall))
        {
            collisionObj = null;
            isWall = false;
            hit = false;
            plaAni.SetBool("touchOn", false);
            plaAni.SetBool("pushOn", false);
        }
    }
    private bool PositionCheck()
    {
        if (collisionObj.transform.position.x < transform.position.x)
        {
            return true;
        }//当たったオブジェクトより自身が右にいたら
        else
        {
            return false;
        }//当たったオブジェクトより自身が左にいたら
    }
    private void ObjMove(int i)
    {
        collisionObj.transform.position += (Vector3)new Vector2(pushSpeed*i, 0f);
    }
}

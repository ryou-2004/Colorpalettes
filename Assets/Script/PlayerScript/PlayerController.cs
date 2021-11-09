using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("プレイヤーのスピード")] public float speed;
    [Header("ジャンプのスピード")] public float jumpSpeed;
    [Header("ジャンプ中の左右移動速度")] public float jumpHorizontal;
    [Header("地面の接触判定")] public GroundCheck ground;
    [Header("ダッシュスピード")] public float dashSpeed;
    [Header("ダッシュになるまでの時間")] public float dashTime;
    private bool isGround = false;
    private bool isKey = false;
    private bool spaceDown = false;
    private bool spaceKey = false;
    private float clickY;
    private GameObject paret;
    private float walkTime;

    private Rigidbody2D rb;

    public Animator playerAnim;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        paret = GameObject.FindWithTag("Paret");
    }
    private void Start()
    {
      //  paret = GameObject.FindWithTag("Paret");
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            isKey = false;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            isKey = true;
            if (isGround)
            {
                walkTime += Time.deltaTime;
            }
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            walkTime = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceDown = true;
        }
        else
        {
            spaceDown = false;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            spaceKey = true;
        }
        else
        {
            spaceKey = false;
        }
    }
    private void FixedUpdate()
    {
        isGround = ground.IsGround();

        float xSpeed = 0;
        if (!Goal.goalFlag)
        {
            if (isGround)
            {
                if (!ClimbCheck.IsClimbNow)//登ってなければ
                {
                    xSpeed = GetXSpeed();
                    if (!ClimbCheck.IsClimb)//登れる壁と接触してなければ
                    {
                        GetYSpeed();
                    }
                }
            }//地面と接触しているときの左右移動
            else
            {
                xSpeed = GetXSpeed();
                xSpeed *= jumpHorizontal;
            }
        }
        if (!WallCheck.isWall)
        {
            if (xSpeed > 0)
            {
                Transform tr = this.transform;
                Quaternion rotate = tr.rotation;
                rotate.y = 0;
                tr.rotation = rotate;

                Quaternion r = paret.transform.rotation;
                r.y = 0;
                paret.transform.rotation = r;
            }
            else if (xSpeed < 0)
            {
                Transform tr = this.transform;
                Quaternion rotate = tr.rotation;
                rotate.y = 180;
                tr.rotation = rotate;

                Quaternion r = paret.transform.rotation;
                r.y = 0;
                paret.transform.rotation = r;
            }
            if (!isKey)
            {
                playerAnim.SetBool("dashOn", false);
                PlayerAnim("walkOn", false);
            }
            if (xSpeed != 0)
            {
                if (dashTime < walkTime)
                {
                    playerAnim.SetBool("dashOn", true);
                    playerAnim.SetBool("walkOn", false);
                    xSpeed *= dashSpeed;
                }
                else
                {
                    playerAnim.SetBool("walkOn", true);
                }
            }
        }

        rb.velocity = new Vector2(xSpeed, rb.velocity.y);

    }
    private float GetXSpeed()
    {

        float horizontalKey = Input.GetAxis("Horizontal");

        horizontalKey *= speed;

        return horizontalKey;
    }

    private float GetYSpeed()
    {
        if (spaceDown)
        {
            clickY = transform.position.y;
        }
        if (spaceKey && isGround && !ClimbCheck.IsClimb)
        {
            StartCoroutine(Jump());
        }
        return rb.velocity.y;
    }

    IEnumerator Jump()
    {
        walkTime = 0;
        PlayerAnim("jumpOn", true);
        playerAnim.SetBool("dashOn", false);
        yield return new WaitForSeconds(0.16f);
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
    }
    public void PlayerAnim(string ani, bool b)
    {
        playerAnim.SetBool(ani, b);
    }
}
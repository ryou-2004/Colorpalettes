using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldPlayerController : MonoBehaviour
{
    #region//インスペクターで設定する
    [Header("移動速度")] public float speed;
    [Header("重力")] public float gravity;
    [Header("ジャンプ速度")] public float jumpSpeed;
    [Header("ジャンプする高さ")] public float jumpHeight;
    [Header("ジャンプ制限時間")] public float jumpLimitTime;
    [Header("接地判定")] public GroundCheck ground;
    [Header("頭をぶつけた判定")] public GroundCheck head;
    [Header("ダッシュの速さ表現")] public AnimationCurve dashCurve;
    [Header("ジャンプの速さ表現")] public AnimationCurve jumpCurve;
    [Header("パレット")] public GameObject paret;
    #endregion

    #region//プライベート変数
    private Animator anim = null;
    private Rigidbody2D rb = null;
    private bool isGround = false;
    private bool isHead = false;
    private bool isJump = false;
    private bool isRun = false;
    private bool jumpcontrol = false;
    private float jumpPos = 0.0f;
    private float dashTime, jumpTime;
    private float beforeKey;

    #endregion
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {

        isGround = ground.IsGround();
        isHead = head.IsGround();

        float xSpeed = GetXSpeed();
        float ySpeed = GetYSpeed();

        SetAnimation();

        rb.velocity = new Vector2(xSpeed, ySpeed);
    }
    private float GetYSpeed()
    {
        float verticalKey;
        if (!Goal.goalFlag)     //ゴールしてなかったら
        {
            verticalKey = Input.GetAxis("Vertical");
        }
        else
        {
            verticalKey = 0;
        }
        float ySpeed = -gravity;

        if (isGround)
        {
            if (verticalKey > 0 && jumpcontrol == false)
            {
                ySpeed = jumpSpeed;
                jumpPos = transform.position.y;
                isJump = true;
                jumpTime = 0.0f;
            }
            else
            {
                isJump = false;
            }
        }
        else if (isJump)
        {
            bool pushUpKey = verticalKey > 0;
            bool canHeight = jumpPos + jumpHeight > transform.position.y;
            bool canTime = jumpLimitTime > jumpTime;

            if (pushUpKey && canHeight && canTime && !isHead)
            {
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
            }
            else
            {
                isJump = false;
                jumpTime = 0.0f;
            }
        }
        if (isJump)
        {
            ySpeed *= jumpCurve.Evaluate(jumpTime);
        }
        return ySpeed;
    }
    private float GetXSpeed()
    {
        float horizontalKey;
        if (!Goal.goalFlag)
        {
            horizontalKey = Input.GetAxis("Horizontal");
        }
        else
        {
            horizontalKey = 0;
        }
        float xSpeed = 0.0f;
        if (horizontalKey > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            isRun = true;
            dashTime += Time.deltaTime;
            xSpeed = speed;

            paret.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (horizontalKey < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            isRun = true;
            dashTime += Time.deltaTime;
            xSpeed = -speed;

            paret.transform.eulerAngles = new Vector3(0, -180, 0);
        }
        else
        {
            isRun = false;
            xSpeed = 0.0f;
            dashTime = 0.0f;
        }
        if (horizontalKey > 0 && beforeKey < 0)
        {
            dashTime = 0.0f;
        }
        else if (horizontalKey < 0 && beforeKey > 0)
        {
            dashTime = 0.0f;
        }
        beforeKey = horizontalKey;
        xSpeed *= dashCurve.Evaluate(dashTime);
        beforeKey = horizontalKey;
        return xSpeed;
    }
    private void SetAnimation()
    {
        anim.SetBool("jump", isJump);
        anim.SetBool("ground", isGround);
        anim.SetBool("run", isRun);
    }

    #region//接触判定
    private bool isGroundEnter, isGroundStay, isGroundExit;
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
        isGroundEnter = true;
        if (jumpcontrol == true)
        {
            StartCoroutine("jumpReset");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isGroundStay = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGroundExit = true;
        jumpcontrol = true;
    }
    #endregion
    IEnumerator jumpReset()
    {
        yield return new WaitForSeconds(0.15f);
        jumpcontrol = false;
    }
}

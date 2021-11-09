using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1000f;
    [SerializeField] float jumpPower = 700f;

    public bool isGrounded;

    private Rigidbody2D rb;
    private Vector3 defalutScale;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        defalutScale = transform.localScale;
    }

    private void Update()
    {
        var input_h = Input.GetAxis("Horizontal");

        Walk(input_h);
        Turn(input_h);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isGrounded)
            {
                rb.AddForce(Vector2.up * jumpPower);
            }
        }
    }

    private void Walk(float inputValue)
    {
        if(inputValue != 0)
        {
            var mult = isGrounded ? 1f : 0.3f;
            rb.AddForce(Vector2.right * inputValue * moveSpeed * mult * Time.deltaTime);
        }
    }

    private void Turn(float inputValue)
    {
        if(inputValue > 0)
        {
            transform.localScale = defalutScale;
        }
        else if(inputValue < 0)
        {
            transform.localScale = new Vector3(-defalutScale.x, defalutScale.y, defalutScale.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}

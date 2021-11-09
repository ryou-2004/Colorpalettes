using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    [Header("境界座標")] public float boundary;
    private GameObject sameNameObj;
    private Rigidbody2D rb;

    private void Start()
    {
        gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
        rb = GetComponent<Rigidbody2D>();
        sameNameObj = GameObject.FindWithTag("Sita").transform.Find(transform.parent.name).gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Ground"))
        {
            if (rb.IsSleeping())
            {
                rb.bodyType = RigidbodyType2D.Kinematic;
                rb.velocity = Vector2.zero;
            }
            if (transform.position.y < boundary)
            {
                transform.SetParent(sameNameObj.transform, true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Ground"))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}

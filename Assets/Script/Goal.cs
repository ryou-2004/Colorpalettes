using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] Image fade;
    [SerializeField] float changeSpeed = 0.01f;
    [SerializeField] float changeTime = 1f;
    public static bool goalFlag;
    private float alpha;
    private void FixedUpdate()
    {
        if (goalFlag)
        {
            if (alpha <= changeTime)
            {
                alpha += changeSpeed;
                fade.color = new Color(0, 0, 0, alpha);
            }
            if (alpha >= changeTime)
            {
                SceneManager.LoadScene(sceneName);
                goalFlag = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            goalFlag = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class startAnimScript : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private string[] Contents;
    private int textNum = 0;
    private int a = 0;
    public Image image;
    private float b = 0;

    public GameObject canvas;
    public GameObject background;

    void Start()
    {
        background.SetActive(false);
        canvas.SetActive(true);

        image.color = new Color(0, 0, 0, 0);
        text.text = "";
        StartCoroutine(textLoed());
    }


    void Update()
    {      
    }

    private IEnumerator textLoed()
    {
        b = 1;
        for (int i = 0; i < 255; i++)
        {
            image.color = new Color(0, 0, 0, b);
            b -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(1f);

        for(int i = 0; i <  Contents.Length; i++)
        {
            text.text += Contents[textNum] + "\n";
            yield return new WaitForSeconds(1f);
            if(a >= 4)
            {
                yield return new WaitForSeconds(2f);
                text.text = "";
                a = 0;
                yield return new WaitForSeconds(0.5f);
            }
            textNum++;
            a++;
        }

        b = 0;
        for(int i = 0; i < 255; i++)
        {
            image.color = new Color(0, 0, 0, b);
            b += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        canvas.SetActive(false);
        background.SetActive(true);

        StartCoroutine(anim1());
    }

    private IEnumerator anim1()
    {
        yield return new WaitForSeconds(26f);
        SceneManager.LoadScene("day2");
    }
}

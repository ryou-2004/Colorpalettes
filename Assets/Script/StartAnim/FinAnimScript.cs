using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinAnimScript : MonoBehaviour
{
    [SerializeField]
    private Image fade;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(he());
    }

    public void Click()
    {
        StartCoroutine(hu());
    }
    IEnumerator he()
    {
        float a = 1;
        for (int i = 0; i < 255; i++)
        {
            fade.color = new Color(0, 0, 0, a);
            a -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator hu()
    {
        float b = 0;
        for (int i = 0; i < 255; i++)
        {
            fade.color = new Color(0, 0, 0, b);
            b += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        SceneManager.LoadScene("Title");
    }
}

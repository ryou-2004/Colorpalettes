using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleMenuScript : MonoBehaviour
{
    [Header("参照類")]
    /*参照*/
    [SerializeField] Animator pageAnim;
    [SerializeField] Image fadeOut;
    [SerializeField] GameObject fade;

    [Header("ステージ情報類")]
    /*パラメータ等*/
    private int pageNum = 0;
    [SerializeField] string[] stageDay;
    [SerializeField] string[] stageNum; 
    [SerializeField] string[] stageName;
    [SerializeField] Sprite[] stageImage;
    [Header("143文字まで")][SerializeField] string[] stageDescription;

    [Header("色変更類")]
    /*StartButtonの色変更等*/
    [SerializeField] Color basicColor, changeColor;

    [Header("TitlePageObject類")]
    /*タイトルページオブジェクト*/
    [SerializeField] GameObject GameTitleText;
    [SerializeField] GameObject Photo;
    [SerializeField] GameObject StartButton;

    [Header("ページオブジェクト（左）")]
    /*左ページオブジェクト*/
    [SerializeField] GameObject leftPage;
    [Header("　")]
    [SerializeField] Image stageImageObject;
    [SerializeField] Text stageNameText;
    [SerializeField] Text stageNumText;
    [SerializeField] GameObject stageUseColor;

    [Header("ページオブジェクト（右）")]
    /*右ページオブジェクト*/
    [SerializeField] GameObject rightPage;
    [Header("　")]
    [SerializeField] Text stageDescriptionText;
    [SerializeField] Text stageDayText;
    [SerializeField] GameObject stagePainting;
    public void EventText_Enter(Text ButtonText)
    {
        ButtonText.color = changeColor;
    }
    public void EventText_Exit(Text ButtonText)
    {
        ButtonText.color = basicColor;
    }

    private void Start()
    {
        StartCoroutine(fadeIn());
    }

    IEnumerator fadeIn()
    {
        float a = 1;
        for(int i = 0; i < 100; i++)
        {
            fadeOut.color = new Color(0, 0, 0, a);
            a -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        fade.SetActive(false);
    }

    public void OnStartButton()
    {
        pageNum = 0;
        GameTitleText.SetActive(false);
        Photo.SetActive(false);
        StartButton.SetActive(false);
        StartCoroutine(pageTo_left());
    }

    public void fadeOutScene()
    {
        fade.SetActive(true);
        float a = 0;
        for(int i = 0; i < 100; i++)
        {
            fadeOut.color = new Color(0, 0, 0, a);
            a += 0.01f;
        }
        SceneManager.LoadScene("StartScene");
    }

    private void Update()
    {
        stageNumText.text = $"stage {stageNum[pageNum]}";
        stageNameText.text = $"{stageName[pageNum]}";
        stageDayText.text = $"day {stageDay[pageNum]}";
        stageImageObject.sprite = stageImage[pageNum];
        stageDescriptionText.text = $"{stageDescription[pageNum]}";
    }

    private IEnumerator pageTo_left()
    {
        Debug.Log("left");
        pageAnim.SetBool("page_left", true);
        rightPage.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        leftPage.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        pageAnim.SetBool("page_left", false);
        yield return new WaitForSeconds(0.05f);
        leftPage.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        rightPage.SetActive(true);

    }
    private IEnumerator pageTo_right()
    {
        pageAnim.SetBool("page_right", true);
        leftPage.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        rightPage.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        pageAnim.SetBool("page_right", false);
        yield return new WaitForSeconds(0.05f);
        leftPage.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        rightPage.SetActive(true);
    }
}

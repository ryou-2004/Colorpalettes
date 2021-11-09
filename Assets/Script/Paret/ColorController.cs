using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ColorController : MonoBehaviour
{
    #region//インスペクターで設定する
    [Header("色設定(子どもの順で)")] public Color[] color;
    [Header("カラーパレットの選択中の大きさ")] public Vector3 paretScale;
    [Header("矢印の選択中の大きさ")] public Vector3 arrowScale;
    [Header("重なった時に代わる色")] public Color selectColor;

    [Header("筆のオブジェクト")] public GameObject brush;
    #endregion

    #region//パブリックでインスペクター非表示
    [System.NonSerialized]
    public static bool chengeBackGroundColor = false;

    [System.NonSerialized]
    public static Color[] colorStatic;
    #endregion

    #region//プライベート変数
    private GameObject[] colorChild;
    private GameObject colorObject;
    private GameObject[] arrowChild;
    private GameObject arrow;
    private GameObject backGroundUp, backGroundDown;

    private Vector3 defaultColorScale, defaultArrowScale;
    private Vector2 downPos, downNawPos, upPos;

    private string[] colorTag;
    private float mouseWheel;
    private float scaleTime;
    private int index;
    private int nowColorUp, nowColorDown;
    private bool paretOn;
    private bool bigFlag;
    private bool timeScaleFlag, timerFlag;

    private Animator brushAnim;

    private AudioController au;
    #endregion
    private int Index
    {
        get { return index; }
        set
        {
            if (value < 0)
                value = colorChild.Length - 1;
            else if (value >= colorChild.Length)
                value = 0;
            index = value;
        }
    }///パレットの端まで行ったら反対側に行くプロパティ
    private void Awake()
    {
        colorStatic = color;
        nowColorUp = -1;
        nowColorDown = -2;
    }
    private void Start()
    {
        {
            colorObject = GameObject.FindWithTag("Paret");
            backGroundUp = GameObject.FindWithTag("Up");
            backGroundDown = GameObject.FindWithTag("Down");
            arrow = GameObject.FindWithTag("Arrow");
        }//GameObject.FindWithTag
        colorChild = new GameObject[colorObject.transform.childCount];
        colorTag = new string[colorObject.transform.childCount];
        for (int i = 0; i < color.Length; i++)
        {
            colorChild[i] = colorObject.transform.GetChild(i).gameObject;
            colorChild[i].GetComponent<Renderer>().material.color = color[i];    //colorChildren[１]の色はcolor[1]でtagがcolorTag[1]
            colorTag[i] = colorChild[i].tag;
        }
        arrowChild = new GameObject[arrow.transform.childCount] ;
        for (int i = 0; i < arrow.transform.childCount; i++)
        {
            arrowChild[i] = arrow.transform.GetChild(i).gameObject;
        }
        defaultColorScale = colorChild[0].transform.localScale;
        defaultArrowScale = arrowChild[0].transform.localScale;

        brushAnim = brush.GetComponent<Animator>();
        brush.SetActive(false);

        //colorObject.SetActive(false);
        arrow.SetActive(false);
        colorObject.SetActive(false);

        au = GameObject.FindWithTag("GameController").GetComponent<AudioController>();
    }
    private void Update()
    {
        if (!Goal.goalFlag)     //ゴールしていない時に
        {
            if (Input.GetMouseButtonDown(1))
            {
                colorObject.SetActive(true);
                arrow.SetActive(true);
                downPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Time.timeScale = 0;
                timeScaleFlag = paretOn = true;
            }//パレット表示処理

            {
                if (timeScaleFlag)
                {
                    scaleTime += Time.unscaledDeltaTime;
                    if (Time.timeScale < 1)
                    {
                        Time.timeScale = Mathf.Lerp(0, 1, scaleTime / 3);
                    }
                    else
                    {
                        timeScaleFlag = false;
                        timerFlag = true;
                        scaleTime = 0;
                    }
                }
            }//スローモーション処理

            if (Input.GetMouseButton(1))    //ホイール処理＆カーソル処理
            {
                {
                    mouseWheel = Input.mouseScrollDelta.y;
                    if (bigFlag)
                    {
                        colorChild[Index].transform.localScale = defaultColorScale;
                        bigFlag = false;
                    }
                    if (mouseWheel > 0)
                    {
                        Index--;
                    }
                    else if (mouseWheel < 0)
                    {
                        Index++;
                    }
                    if (!bigFlag)
                    {
                        colorChild[Index].transform.localScale = paretScale;
                        arrow.transform.position = colorChild[Index].transform.position;
                        bigFlag = true;
                    }
                    mouseWheel = 0;
                }//ホイールの処理
                {
                    downNawPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    if (downPos.y < downNawPos.y)
                    {
                        arrowChild[0].transform.localScale = arrowScale;
                    }
                    else
                    {
                        arrowChild[0].transform.localScale = defaultArrowScale;
                    }
                    if (downNawPos.y < downPos.y)
                    {
                        arrowChild[1].transform.localScale = arrowScale;
                    }
                    else
                    {
                        arrowChild[1].transform.localScale = defaultArrowScale;
                    }
                }//カーソルの処理
            }

            if (paretOn)
            {
                if (Input.GetMouseButtonUp(1) || timerFlag)
                {
                    upPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    brush.SetActive(true);
                    StartCoroutine(colorChange());
                    StartCoroutine(offBrush());
                    au.BackChangeSE();
                }
            }//上下の反転処理＆色割り当て処理
        }
    }
    private void ColorCheck(bool upDown)
    {
        if (upDown)
        {
            backGroundUp.GetComponent<Renderer>().material.color = color[index];
            Disappear.ChengeColor(true, index);
            if (nowColorDown == index)
            {
                backGroundDown.GetComponent<Renderer>().material.color = selectColor;
                Disappear.ChengeColor(false);
            }
        }
        else
        {
            backGroundDown.GetComponent<Renderer>().material.color = color[index];
            Disappear.ChengeColor(false, index);
            if (nowColorUp == index)
            {
                backGroundUp.GetComponent<Renderer>().material.color = selectColor;
                Disappear.ChengeColor(true);
            }
        }
    }

    private IEnumerator colorChange()
    {
        if (downPos.y < upPos.y)
        {
            brushAnim.SetBool("BrushUpOn", true);
        }//クリックしたポジションより上なら

        if (downPos.y > upPos.y)
        {
            brushAnim.SetBool("BrushDownOn", true);
        }//クリックしたポジションより下なら

        chengeBackGroundColor = true;
        colorObject.SetActive(false);
        arrow.SetActive(false);
        Time.timeScale = 1;
        scaleTime = 0;
        paretOn = false;
        timeScaleFlag = false;
        timerFlag = false;

        yield return new WaitForSeconds(0.2f);
        if (downPos.y < upPos.y)
        {
            nowColorUp = index;
            ColorCheck(true);
        }//クリックしたポジションより上なら
        if (downPos.y > upPos.y)
        {
            nowColorDown = index;
            ColorCheck(false);
        }//クリックしたポジションより下なら

        colorChild[Index].transform.localScale = defaultColorScale;
        Index = 0;
    }

    private IEnumerator offBrush()
    {
        yield return new WaitForSeconds(0.8f);
        brush.SetActive(false);
        brushAnim.SetBool("BrushUpOn", false);
        brushAnim.SetBool("BrushDownOn", false);
    }
}

using UnityEngine;

public class Disappear : MonoBehaviour
{
    private static Color[] color;
    private  GameObject disappearUp, disappearDown;
    private static GameObject[] disappearObjectUp, disappearObjectDown;

    private void Start()
    {
        disappearObjectUp   = new GameObject[ColorController.colorStatic.Length];   //ColorControllerで設定したindex分の配列
        disappearObjectDown = new GameObject[ColorController.colorStatic.Length];
        disappearUp   = GameObject.FindWithTag("Disappear").transform.GetChild(0).gameObject;   //上
        disappearDown = GameObject.FindWithTag("Disappear").transform.GetChild(1).gameObject;   //下
        disappearObjectUp   = Getchild(disappearUp);    //上赤、下赤
        disappearObjectDown = Getchild(disappearDown);  //下赤、下青
        color = ColorController.colorStatic;
    }

    public static void ChengeColor(bool chenge,int colorIndex)
    {
        if (chenge)
        {
            disappearObjectUp[colorIndex].SetActive(false);
        }//上
        else
        {
            disappearObjectDown[colorIndex].SetActive(false);
        }//下
        for(int i = 0; i < color.Length; i++)
        {
            if (!(i==colorIndex))
            {
                if (chenge)
                {
                    disappearObjectUp[i].SetActive(true);
                }
                else
                {
                    disappearObjectDown[i].SetActive(true);
                }
            }
        }
    }
    public static void ChengeColor(bool chenge)
    {
        if (chenge)
        {
            ChengeSetActive(disappearObjectUp);
        }
        else
        {
            ChengeSetActive(disappearObjectDown);
        }
    }

    private static void ChengeSetActive(GameObject[] dissapearParent)
    {
        foreach(GameObject dissapearChild in dissapearParent)
        {
            dissapearChild.SetActive(true);
        }
    }
    private GameObject[] Getchild(GameObject obj)
    {
        GameObject[] ret = new GameObject[obj.transform.childCount];
        for(int i = 0; i < obj.transform.childCount; i++)
        {
            ret[i] = obj.transform.GetChild(i).transform.gameObject ;
        }
        return ret;
    }
}
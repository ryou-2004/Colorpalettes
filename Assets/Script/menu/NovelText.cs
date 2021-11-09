using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NovelText : MonoBehaviour
{
    [SerializeField] List<string> messageList = new List<string>();
    [SerializeField] Text text;
    [SerializeField] float novelSpeed;
    int novelListIndex = 0;

    void Start()
    {
        StartCoroutine(Novel());
    }

    private IEnumerator Novel()
    {
        int messageCount = 0;
        text.text = "";
        while(messageList[novelListIndex].Length > messageCount)
        {
            text.text += messageList[novelListIndex][messageCount];
            messageCount++;
            yield return new WaitForSeconds(novelSpeed);
        }

        novelListIndex++;
        if(novelListIndex < messageList.Count)
        {
            StartCoroutine(Novel());
        }
    }
}

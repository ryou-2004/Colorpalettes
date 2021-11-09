using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cpuConversation : MonoBehaviour
{
    public string[] sentences;
    [SerializeField] Text uiText;

    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharDisplay = 0.05f;

    private int currentSentenceNum = 0;
    private string currentSentence = string.Empty;
    private float timeUntilDisplay = 0;
    private float timeBeganDisplay = 1;
    private int lastUpdateCharCount = -1;

    [SerializeField] GameObject messageObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            messageObject.SetActive(true);
            uiText.text = "";
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            messageObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if ( other.gameObject.tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                SetNextSentence();
            }
        }
    }

    public void TextUpdate(bool _IsPush)
    {
        if(IsDisplayComplete())
        {
            if(currentSentenceNum < sentences.Length && _IsPush)
            {
                SetNextSentence();
            }
            else if(currentSentenceNum >= sentences.Length)
            {
                currentSentenceNum = 0;
            }
            else
            {
                if (_IsPush)
                {
                    timeUntilDisplay = 0;
                }
            }
        }

        int displayCharCount = (int)(Mathf.Clamp01((Time.time - timeBeganDisplay) / timeUntilDisplay) * currentSentence.Length);
        if(displayCharCount != lastUpdateCharCount)
        {
            uiText.text = currentSentence.Substring(0, displayCharCount);
            lastUpdateCharCount = displayCharCount;
        }
    }
    void SetNextSentence()
    {
        currentSentence = sentences[currentSentenceNum];
        timeUntilDisplay = currentSentence.Length * intervalForCharDisplay;
        timeBeganDisplay = Time.time;
        currentSentenceNum++;
        lastUpdateCharCount = 0;
    }
    bool IsDisplayComplete()
    {
        return Time.time > timeBeganDisplay + timeUntilDisplay;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMainSystem : MonoBehaviour
{
    [SerializeField]
    cpuConversation cpuConversation;

    bool IsTextPush = false;
    void Update()
    {
        cpuConversation.TextUpdate(IsTextPush);
        IsTextPush = false;
    }
    public void PushText()
    {
        this.IsTextPush = true;
    }
}

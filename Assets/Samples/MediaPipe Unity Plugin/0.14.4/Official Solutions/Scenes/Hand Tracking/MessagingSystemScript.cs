using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Drawing;

public class MessagingSystemScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI MessagingLine;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ClearMessage", 5, 0);
    }

    public void SetMessage(string _Message, UnityEngine.Color _Color)
    {
        CancelInvoke("ClearMessage");
        MessagingLine.text = _Message;
        MessagingLine.color = _Color;
        InvokeRepeating("ClearMessage", 5, 0);
    }

    public void ClearMessage()
    {
        MessagingLine.text = "";
    }
}
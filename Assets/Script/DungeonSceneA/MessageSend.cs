using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageSend : MonoBehaviour
{
    private List<string> msgStack = new List<string> ();
    private string _message;
    [HideInInspector]
    public string message
    {
        get
        {
            return _message;
        }
        set
        { //messageが入ったらメッセージ表示を同時に呼び出す
            if (charCount != 0 && !_message.Equals (value))
            { //表示中にさらにメッセージが入力されたらストックする
                msgStack.Add (value);
            }
            else
            {
                _message = value;
                StartCoroutine (DisplayMassage ());
            }

        }
    }
    private int charCount = 0;
    private WaitForSeconds waitForSeconds = new WaitForSeconds (0.1f);
    private TextMeshProUGUI messageWindowText;


    void Awake()
    {
        messageWindowText = transform.GetChild (0).gameObject.GetComponent<TextMeshProUGUI> ();
    }
    private char GetCharFromMessage ()
    {
        return message[charCount];
    }

    private IEnumerator DisplayMassage ()
    {
        messageWindowText.text = "";

        while (charCount < message.Length)
        {
            messageWindowText.text += GetCharFromMessage ();
            charCount++;
            yield return waitForSeconds;
        }
        // init count
        charCount = 0;

        if (msgStack.Count != 0)
        { //if stack exists , set message again
            yield return new WaitForSeconds (2);
            message = msgStack[0];
            msgStack.RemoveAt (0);
        }
        else
        {
            yield return new WaitForSeconds (5);
            gameObject.SetActive (false);
        }

    }
}
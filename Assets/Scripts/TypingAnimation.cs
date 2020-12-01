using UnityEngine;
using UnityEngine.UI;

public class TypingAnimation : MonoBehaviour
{
    public float timer = 10;
    private Text text;
    private string message;
    private float t = 0;
    private int c = 0;

    void Start()
    {
        text = GetComponent<Text>();
        Type(text.text);
    }

    public void Type(string _text)
    {
        message = _text;
        text.text = string.Empty;
        c = 0;
    }

    void Update()
    {
        t += Time.deltaTime;
        if(t > timer/100)
        {
            t = 0;
            PrintChar();
        }
    }

    void PrintChar()
    {
        if(c < message.Length)
        {
            text.text += message[c];
            c++;
        }
    }
}

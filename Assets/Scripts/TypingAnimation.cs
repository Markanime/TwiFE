using System.Collections.Generic;
using UnityEngine;

public class TypingAnimation : MonoBehaviour
{
    public float timer = 10;
    private TextController textController;
    private string message = string.Empty;
    private float t = 0;
    private int c = 0;
    public float idleSeconds = 0;
    void Start()
    {
        message = string.Empty;
        textController = new TextController(this);
        Type(textController.text);
    }

    public void Type(ChatterWithImages _chatter)
    {
        Type(_chatter.message);
        textController.SetChatter(_chatter);
    }
    public void Type(string _text)
    {
        message = _text.Length > textController.characterLimit ? _text.Substring(0, textController.characterLimit -1) : _text;
        textController.text = string.Empty;
        c = 0;
        idleSeconds = 0;
    }

    void Update()
    {
        t += Time.deltaTime;
        if(t > timer/100)
        {
            t = 0;
            PrintChar();
        }
        Idle();
    }

    void PrintChar()
    {
        if(c < message.Length)
        {
            textController.text += message[c];
            c++;
        }
    }
    void Idle()
    {
        if (c >= message.Length)
            idleSeconds += Time.deltaTime;
    }
}

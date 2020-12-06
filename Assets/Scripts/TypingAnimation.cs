using UnityEngine;

public class TypingAnimation : MonoBehaviour
{
    public float timer = 10;
    private TextController textController;
    private string message = string.Empty;
    private float t = 0;
    private int c = 0;

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
        message = _text;
        textController.text = string.Empty;
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
            textController.text += message[c];
            c++;
        }
    }
}

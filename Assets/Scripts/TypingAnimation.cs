using UnityEngine;

public class TypingAnimation : MonoBehaviour
{
    public float timer = 10;
    public float idleSeconds = 0;
    [HideInInspector]
    public int Length = 0;
    private TextController textController;
    private string text = string.Empty;
    private float t = 0;
    private int c = 0;
    private ChatterWithImages current;
    void Start()
    {
        text = string.Empty;
        textController = new TextController(this);
        Type(textController.text);
    }

    public void Type(ChatterWithImages _chatter)
    {
        Type(_chatter.message);
        Length = _chatter.Length();
        textController.SetChatter(_chatter);
        current = _chatter;
    }

    public void Type(string _text)
    {
        text = _text.Length > textController.characterLimit ? _text.Substring(0, textController.characterLimit -1) : _text;
        textController.text = string.Empty;
        c = 0;
        idleSeconds = 0;
    }
    public void SetChatter(ChatterWithImages _chatter) => Type(_chatter);

    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            t += Time.deltaTime;
            if (t > timer / 100)
            {
                t = 0;
                PrintChar();
            }
            Idle();
        }
    }

    void PrintChar()
    {
        if(c < text.Length)
        {
            do
            {
                textController.text += text[c];
                c++;
            } while (current.Contais(c).Key);
        }
    }
    void Idle()
    {
        if (c >= text.Length)
            idleSeconds += Time.deltaTime;
    }
}

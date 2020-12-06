using UnityEngine;

public class MessageSender : MonoBehaviour
{
    public TypingAnimation text; 
    private TwitchIRC irc;
    // Start is called before the first frame update
    void Awake()
    {
        irc = GetComponentInChildren<TwitchIRC>();
        irc.newChatMessageEvent.AddListener(PrintMessage);
    }

    // Update is called once per frame
    public void PrintMessage(Chatter chatter)
    {
        text.Type(new ChatterWithImages(chatter));
    }
}

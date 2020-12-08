using UnityEngine;
using UnityEngine.UI;
public class MessageSender : MonoBehaviour
{
    public TwifeMessageBox MessageBox;
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
        MessageBox.Push(new ChatterWithImages(chatter));
    }
}

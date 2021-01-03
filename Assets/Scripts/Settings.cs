using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public MessageSender messageSender;
    public TwitchIRC twichIRC;
    public InputField token;
    public InputField nickname;
    public InputField channel;
    public MessageBoxSelector messageBoxSelector;

    public void Start()
    {
        token.text = PlayerPrefs.GetString("oauth", string.Empty);
        nickname.text = PlayerPrefs.GetString("nickname", string.Empty);
        channel.text = PlayerPrefs.GetString("channel", string.Empty);

    }

    public void Save()
    {
        StartCoroutine(SaveRoutine());
    }
    public IEnumerator SaveRoutine()
    {
        twichIRC.Disconnect();
        yield return new WaitForEndOfFrame();
        twichIRC.details.oauth = token.text;
        PlayerPrefs.SetString("oauth", token.text);
        twichIRC.details.nick = nickname.text;
        PlayerPrefs.SetString("nickname", nickname.text);
        twichIRC.details.channel = channel.text;
        PlayerPrefs.SetString("channel", channel.text);
        messageSender.MessageBox = messageBoxSelector.selected;
        messageBoxSelector.selected.gameObject.SetActive(true);
        yield return new WaitForEndOfFrame();
        twichIRC.Connect();
        yield return new WaitForEndOfFrame();
        gameObject.SetActive(false);
    }

    public void Link()
    {
        Application.OpenURL("https://twitchapps.com/tmi/");
    }
}


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
    public GameObject button;

    public void Start()
    {
        Open();
        LoadSettings();
        LoadColor();
    }
    public void Open()
    {
        gameObject.SetActive(true);
        button.SetActive(!gameObject.activeSelf);
    }

    public void Close()
    {
        gameObject.SetActive(false);
        button.SetActive(!gameObject.activeSelf);
        LoadSettings();
        LoadColor();
        messageBoxSelector.LoadSettings();
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
        PlayerPrefs.SetString("msgBox", messageBoxSelector.selected.gameObject.name);
        messageBoxSelector.selected.gameObject.SetActive(true);
        PlayerPrefs.SetString("chroma", string.Format("{0}|{1}|{2}",Camera.main.backgroundColor.r.ToString(),
            Camera.main.backgroundColor.g.ToString(),
            Camera.main.backgroundColor.b.ToString()));
        yield return new WaitForEndOfFrame();
        twichIRC.Connect();
        yield return new WaitForEndOfFrame();
        Close();
    }

    public void Link()
    {
        Application.OpenURL("https://twitchapps.com/tmi/");
    }

    public void SetColor()
    {
        Camera.main.backgroundColor = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Image>().color;
    }

    private void LoadColor() {
        string color = PlayerPrefs.GetString("chroma", string.Empty);
        if (!string.IsNullOrEmpty(color))
        {
            string[] colors = color.Split('|');
            Camera.main.backgroundColor = new Color(float.Parse(colors[0]), float.Parse(colors[1]), float.Parse(colors[2]));
        }
    }

    private void LoadSettings()
    {
        token.text = PlayerPrefs.GetString("oauth", string.Empty);
        nickname.text = PlayerPrefs.GetString("nickname", string.Empty);
        channel.text = PlayerPrefs.GetString("channel", string.Empty);
    }
}


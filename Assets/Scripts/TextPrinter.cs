using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPrinter : MonoBehaviour
{
    public float size = 14;
    public Vector2 distance = new Vector2(0,0);
    public Font font;
    public Color color;

    [HideInInspector]
    public Vector2 RealSize => new Vector2(size + distance.x,size + distance.y);
    [HideInInspector]
    public string Text
    {
        get
        {
            return _text;
        }
        set
        {
            _text = value;
            RefreshCharacters();
        }
    }
    private string _text = string.Empty;
    private ChatterWithImages chatter;
    private List<GameObject> gameObjects;

    public int characterLimit => CharacterLimit();

    public void SetChatter(ChatterWithImages _chatter)
    {
        this.chatter = _chatter;
    }

    public int CharacterLimit()
    {
        var sizeDelta = GetComponent<RectTransform>().sizeDelta;
        return (int)(((sizeDelta.x / RealSize.x) - 1 )* ((sizeDelta.y / RealSize.y) - 1)); 
    }

    public string GetText() => _text;

    void Awake()
    {
        gameObjects = new List<GameObject>();
    }

    private void RefreshCharacters()
    {
        if (gameObjects.Count <= _text.Length)
        {
            for (int i = 0; i < _text.Length; i++)
            {
                string text = string.Format("{0}", _text[i]);
                if (i < gameObjects.Count)
                {
                    if (text != gameObjects[i].name)
                            ChangeChar(text, gameObjects[i]);
                }
                else
                {
                        gameObjects.Add(i > 0 ? BuildChar(text, gameObjects[i - 1].GetComponent<RectTransform>()) : BuildChar(text));
                }
            }
        }
        else
        {
            for (int i = gameObjects.Count - 1; i >= 0; i--)
            {
                if (i < _text.Length)
                {
                    string text = string.Format("{0}", _text[i]);
                    if (text != gameObjects[i].name)
                            ChangeChar(text, gameObjects[i]);
                }
                else
                {
                    MonoBehaviour.Destroy(gameObjects[i]);
                    gameObjects.RemoveAt(i);
                }
            }
        }
    }
    private void ChangeChar(string character, GameObject gameObject)
    {
        if (!gameObject.GetComponent<Text>())
        {
            MonoBehaviour.Destroy(gameObject.GetComponent<Image>());
            BuildChar(gameObject, character);
        }
        else
        {
            gameObject.GetComponent<Text>().text = character;
            gameObject.name = character;
        }
    }

    private GameObject BuildChar(string character, RectTransform prev)
    {
        int index = gameObjects.Count;
        var result = chatter.Contais(index);

        GameObject gameObject = BuildChar(character);
        var parentSize = gameObject.transform.parent.GetComponent<RectTransform>().sizeDelta;
        var rectTransform = gameObject.GetComponent<RectTransform>();
        float nextX = prev.anchoredPosition.x + RealSize.x;
        float X = nextX < parentSize.x/2 ? (result.Key ? prev.anchoredPosition.x : nextX) : rectTransform.anchoredPosition.x;
        float Y = nextX < parentSize.x/2 ? prev.anchoredPosition.y : prev.anchoredPosition.y - RealSize.y;
        rectTransform.anchoredPosition = new Vector2(X,Y);
        return gameObject;
    }

    private GameObject BuildChar(string character)
    {
        GameObject gameObject =  BuildChar(new GameObject(character),character);
        var parentSize = gameObject.transform.parent.GetComponent<RectTransform>().sizeDelta;
        var rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(size, size);
        rectTransform.anchoredPosition = new Vector2(parentSize.x / 2 * -1 + size, parentSize.y/2 - size);
        return gameObject;
    }

    private GameObject BuildChar(GameObject gameObject,string character)
    {
        gameObject.transform.SetParent(this.transform, false);
        int index = gameObjects.Count;
        var emoticonItem = chatter.Contais(index);
        bool condition = emoticonItem.Key ? emoticonItem.Value.GetIndex(index).startIndex == index : false;
        if (condition)
        {
            var i = gameObject.AddComponent<Image>();
            i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
            StartCoroutine(emoticonItem.Value.GetTexture(i));
            return i.gameObject;
        }
        else
        {
            var t = gameObject.AddComponent<Text>();
            t.text = emoticonItem.Key ? string.Empty : character;
            t.font = font;
            t.color = color;
            t.fontSize = (int)size -1;
            t.horizontalOverflow = HorizontalWrapMode.Overflow;
            t.verticalOverflow = VerticalWrapMode.Overflow;
            t.alignment = TextAnchor.LowerCenter;
            return t.gameObject;
        }
    }

}

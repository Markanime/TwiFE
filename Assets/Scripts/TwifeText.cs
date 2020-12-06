using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwifeText : MonoBehaviour
{
    public float size = 14;
    public Font font;
    public Color color;
    public float distance = 10;
    [HideInInspector]
    public string text
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
    public List<GameObject> gameObjects;

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
            BuildChar(gameObject, character);
        if (gameObject.GetComponent<Image>())
            MonoBehaviour.Destroy(gameObject.GetComponent<Image>());
        gameObject.GetComponent<Text>().text = character;
        gameObject.name = character;
    }

    private GameObject BuildChar(string character, RectTransform prev)
    {
        int index = gameObjects.Count - 1;
        var result = chatter.Contais(index);

        GameObject go = BuildChar(character);

        float dis = result.Key ? 0 : distance;
        go.GetComponent<RectTransform>().anchoredPosition = new Vector2(prev.anchoredPosition.x + dis, go.GetComponent<RectTransform>().anchoredPosition.y);
        return go;
    }

    private GameObject BuildChar(string character)
    {
        GameObject gameObject =  BuildChar(new GameObject(character),character);
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
        return gameObject;
    }

    private GameObject BuildChar(GameObject gameObject,string character)
    {
        gameObject.transform.SetParent(this.transform, false);
        

        int index = gameObjects.Count;
        var result = chatter.Contais(index);
        if (result.Key)
        {
            if (result.Value.GetIndex(index).startIndex == index)
            {
                var i = gameObject.AddComponent<Image>();
                StartCoroutine(result.Value.GetTexture(i));
                return i.gameObject;
            }
            else
            {
                var t = gameObject.AddComponent<Text>();
                t.text = string.Empty;
                t.font = font;
                t.color = color;
                t.fontSize = (int)size;
                t.horizontalOverflow = HorizontalWrapMode.Overflow;
                t.verticalOverflow = VerticalWrapMode.Overflow;
                return t.gameObject;
            }
        }
        else
        {
            var t = gameObject.AddComponent<Text>();
            t.text = character;
            t.font = font;
            t.color = color;
            t.fontSize = (int)size;
            t.horizontalOverflow = HorizontalWrapMode.Overflow;
            t.verticalOverflow = VerticalWrapMode.Overflow;
            return t.gameObject;
        }
    }
    
    private ChatterWithImages chatter;
    public void SetChatter(ChatterWithImages _chatter)
    {
        this.chatter = _chatter;
    }
}

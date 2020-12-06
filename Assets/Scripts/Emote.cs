using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Emote : MonoBehaviour
{
    public string id;
    public string scale;
    public Texture2D texture;
    [System.Serializable]
    public struct Index
    {
        public int startIndex, endIndex;
    }
    public Index[] indexes;

    public Emote Load(ChatterEmote emote)
    {
        name = emote.id;
        List<Index> indexes = new List<Emote.Index>();
        foreach (var index in emote.indexes)
            indexes.Add(new Emote.Index() { startIndex = index.startIndex, endIndex = index.endIndex });
        this.indexes = indexes.ToArray();
        scale = "3.0";
        id = emote.id;
        return this;
    }

    public Index GetIndex(int i)
    {
        foreach (var index in indexes)
            if (index.startIndex >= i && i <= index.endIndex )
                return index;
        return new Index();
    }


    public IEnumerator GetTexture(Image i)
    {
        string url = string.Format("https://static-cdn.jtvnw.net/emoticons/v2/{0}/default/light/{1}", id, scale);
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
        {
            webRequest.certificateHandler = new AcceptAllCertificates();
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                ///requestErrorOccurred = true;
            }
            else
            {
                texture = DownloadHandlerTexture.GetContent(webRequest);
                yield return new WaitForEndOfFrame();
                i.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0));
            }

            yield return 0;
        }

    }
}

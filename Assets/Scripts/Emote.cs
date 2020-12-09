using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Emote : Poolable
{
    public string scale;
    [System.Serializable]
    public struct Index
    {
        public int startIndex, endIndex;
    }
    public Index[] indexes;

    public Emote Load(ChatterEmote emote)
    {
        scale = "3.0";
        var loaded = base.Load(emote.id) as Emote;
        return loaded;
    }

    public void ChangeIndex(ChatterEmote emote)
    {
        List<Index> indexes = new List<Emote.Index>();
        foreach (var index in emote.indexes)
            indexes.Add(new Emote.Index() { startIndex = index.startIndex, endIndex = index.endIndex });
        this.indexes = indexes.ToArray();
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
        yield return GetTexture(url, i);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class ChatterWithImages
{
    public string message => chatter.message;
    public List<Texture2D> badges;
    public List<Emote> emotes;

    private Chatter chatter;
    public ChatterWithImages(Chatter _chatter)
    {
        chatter = _chatter;
        ProccessTags(chatter);
    }

    private void ProccessTags(Chatter chatter)
    {
        foreach (var badge in chatter.tags.badges)
        {
            //badge.id;
            //badge.version
        }
        emotes = new List<Emote>();
        foreach (var emote in chatter.tags.emotes)
        {
            emotes.Add(new GameObject().AddComponent<Emote>().Load(emote));
        }
    }

    public KeyValuePair<bool, Emote>  Contais(int index)
    {
        foreach(var emote in emotes)
        {
            foreach(var ind in emote.indexes)
            {
                if (index >= ind.startIndex && index  <= ind.endIndex)
                    return  new KeyValuePair<bool, Emote>(true,emote);
            }
        }
        return new KeyValuePair<bool, Emote>(false,null);
    }
}

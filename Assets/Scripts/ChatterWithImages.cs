using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChatterWithImages
{
    public string message => chatter.message;
    public Channel channel;
    public List<Texture2D> badges;
    public List<Emote> emotes;

    private Chatter chatter;
    public ChatterWithImages(Chatter _chatter)
    {
        chatter = _chatter;
        ProccessChatter(chatter);
    }

    private void ProccessChatter(Chatter chatter)
    {
        foreach (var badge in chatter.tags.badges)
        {
            //badge.id;
            //badge.version
        }
        emotes = new List<Emote>();
        chatter.tags.emotes.ForEach(e => emotes.Add(new GameObject().AddComponent<Emote>().Load(e)));
        channel = new GameObject().AddComponent<Channel>().Load(chatter.login) as Channel;
    }

    public void ChangeEmoteIndex()
    {
        foreach (var chatterEmote in chatter.tags.emotes) {
            foreach (var emote in emotes)
            {
                if (emote.id == chatterEmote.id)
                {
                    emote.ChangeIndex(chatterEmote);
                }
            }
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

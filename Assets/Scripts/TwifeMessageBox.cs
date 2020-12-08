using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class TwifeMessageBox : MonoBehaviour
{
    public Vector2 timerRange = new Vector2(2,10);
    public float maxSeconds = 3f;

    public Image channelIcon;
    public TypingAnimation text;
    public List<ChatterWithImages> queue = new List<ChatterWithImages>();
    public void Push(ChatterWithImages chatter)
    {
        queue.Add(chatter);
    }

    private void Update()
    {
        if (queue.Count > 0)
        {
            text.timer = timerRange.x;

            if (text.idleSeconds > maxSeconds)
            {
                Print(queue[0]);
                queue.RemoveAt(0);
            }
        }
        else
        {
            text.timer = timerRange.y;
        }
    }

    private void Print(ChatterWithImages chatter)
    {
        StartCoroutine(chatter.channel.GetTexture(channelIcon));
        text.Type(chatter);
    }
}

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
    public Text userName;
    private List<ChatterWithImages> queue = new List<ChatterWithImages>();
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
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
            if (text.idleSeconds > maxSeconds * 3)
            {
                animator.SetBool("hide",true);
            }
        }
    }

    private void Print(ChatterWithImages chatter)
    {
        if(chatter.channel.id != userName.text)
            animator.SetBool("hide", true);
        else
            animator.SetBool("hide", false);
        StartCoroutine(Chat(chatter));

    }

    IEnumerator Chat(ChatterWithImages chatter)
    {
        yield return chatter.channel.GetTexture(channelIcon);
        userName.text = chatter.channel.id;
        animator.SetBool("hide", false);
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("show"))
            animator.SetTrigger("show");
        text.Type(chatter);
    }
}

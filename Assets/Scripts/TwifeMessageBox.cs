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
    private bool printing = false;

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
        if (!printing)
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
                    animator.SetBool("hide", true);
                }
            }
        }
    }
    private void Print(ChatterWithImages chatter)
    {
        printing = true;
        StartCoroutine(PrintCoRoutine(chatter));

    }

    IEnumerator PrintCoRoutine(ChatterWithImages chatter)
    {
        //check if there is a new user
        bool newUser = chatter.channel.id != userName.text;
        animator.SetBool("hide", newUser);

        //give some time to hide the previus user
        yield return new WaitForEndOfFrame();
        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("hidden") && newUser)
            yield return new WaitForSeconds(1f);
        animator.SetBool("hide", false);

        //change the current user's image, name and message and show it
        chatter.ChangeEmoteIndex();
        userName.text = chatter.channel.id;
        yield return chatter.channel.GetTexture(channelIcon);
        if(!animator.GetCurrentAnimatorStateInfo(0).IsTag("showing"))
            animator.SetTrigger("show");
        text.SetChatter(chatter);
        printing = false;
    }
}

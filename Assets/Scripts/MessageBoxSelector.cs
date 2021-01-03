using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBoxSelector : MonoBehaviour
{
    public TwifeMessageBox[] options;
    public MessageBoxSelectorItem itemBase;
    [HideInInspector]
    public TwifeMessageBox selected;

    private void Start()
    {
        foreach(var option in options)
        {
            itemBase.Create(option);
        }
    }
    public void DeActiveAll()
    {
        foreach(MessageBoxSelectorItem item in GetComponentsInChildren<MessageBoxSelectorItem>())
        {
            item.DeActivate();
        }
    }
}

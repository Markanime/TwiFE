using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBoxSelector : MonoBehaviour
{
    public TwifeMessageBox[] options;
    public MessageBoxSelectorItem itemBase;
    [HideInInspector]
    public TwifeMessageBox selected;
    IEnumerator Start()
    {
        foreach(var option in options)
        {
            itemBase.Create(option);
        }
        yield return new WaitForEndOfFrame();
        LoadSettings();
    }
    public void DeActiveAll()
    {
        foreach(MessageBoxSelectorItem item in GetComponentsInChildren<MessageBoxSelectorItem>())
        {
            item.DeActivate();
        }
    }

    public void LoadSettings()
    {
        string saved = PlayerPrefs.GetString("msgBox", string.Empty);
        if (string.IsNullOrEmpty(saved))
        {
            GetComponentInChildren<MessageBoxSelectorItem>().Activate();
        }
        else
        {
            transform.Find(saved).GetComponent<MessageBoxSelectorItem>().Activate();
        }
    }
}

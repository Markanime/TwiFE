using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBoxSelectorItem : MonoBehaviour
{
    private TwifeMessageBox messageBox;
    private MessageBoxSelector mySelector;


    private void Start()
    {
        mySelector = transform.parent.GetComponent<MessageBoxSelector>();
    }

    public void AssingMessageBox(TwifeMessageBox tmb)
    {
        this.messageBox = tmb;
        this.name = tmb.gameObject.name;
        GetComponentInChildren<Text>().text = tmb.gameObject.name;
    }

    public void Create(TwifeMessageBox messageBox)
    {
        MessageBoxSelectorItem item = Instantiate(gameObject).GetComponent<MessageBoxSelectorItem>();
        item.transform.SetParent(transform.parent,false);
        item.gameObject.SetActive(true);
        item.AssingMessageBox(messageBox);
    }

    public void Activate()
    {
        mySelector.DeActiveAll();
        Button button = GetComponent<Button>();
        button.interactable = false;
        mySelector.selected = messageBox;
    }

    public void DeActivate()
    {
        Button button = GetComponent<Button>();
        button.interactable = true;
    }
}

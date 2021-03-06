﻿using UnityEngine;
using UnityEngine.UI;
//using TMPro;

class TextController
{
    private readonly TextPrinter twife;
    private readonly Text canvas;
    private readonly TextMesh mesh;
    private readonly InputField input;
    //private readonly TextMeshPro Pro;
    //private readonly TextMeshProUGUI uGUIPro;
    public string text
    {
        get 
        {
            if (twife) return twife.Text;
            //if (uGUIPro) return uGUIPro.text;
            //if (Pro) return Pro.text;
            if (mesh) return mesh.text;
            if (canvas) return canvas.text;
            return string.Empty;
        }
        set
        {
            if (twife) { twife.Text = value; return; }
            //if (uGUIPro) { uGUIPro.text = value; return; }
            //if (Pro) { Pro.text = value; return; }
            if (mesh) { mesh.text = value; return; }
            if (canvas) { canvas.text = value; return; }
        }
    }
    public int characterLimit => CharacterLimit();
    public TextController(MonoBehaviour consumer)
    {
        canvas = consumer.GetComponent<Text>();
        mesh = consumer.GetComponent<TextMesh>();
        //Pro = consumer.GetComponent<TextMeshPro>();
        //uGUIPro = consumer.GetComponent<TextMeshProUGUI>();
        input = consumer.GetComponent<InputField>();
        twife = consumer.GetComponent<TextPrinter>();
    }

    public void SetChatter(ChatterWithImages chatter)
    {
        if (twife) twife.SetChatter(chatter);
    }

    public int CharacterLimit()
    {
        if (twife) return twife.characterLimit;
        if (input) return input.characterLimit;
        return 280;
    }

}
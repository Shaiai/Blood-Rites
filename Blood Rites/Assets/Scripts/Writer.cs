using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Writer : MonoBehaviour
{
    private SpeakerUI speaker;
    private Text box;
    private string text;
    private float timer, delta;
    private int index;
    private bool isNarration, isInvis;
    

    public void buildWriter(SpeakerUI speaker, string text, float delta, bool isNarration)
    {
        this.speaker = speaker;
        this.text = text;
        this.delta = delta;
        this.isNarration = isNarration;
        index = 0;
    }

    public void buildWriter(Text box, string text, float delta, bool isNarration)
    {
        this.box = box;
        this.text = text;
        this.delta = delta;
        this.isNarration = isNarration;
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        while (timer <= 0f)
        {
            timer += delta;
            ++index;
            string fullText = text.Substring(0, index);
            if(!isNarration)
                speaker.Dialog = fullText;
            else
                box.text = fullText;

        }
        if(index >= text.Length)
        {
            enabled = false;
            return;
        }
    }
}

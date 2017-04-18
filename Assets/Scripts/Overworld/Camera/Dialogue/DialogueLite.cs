using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueLite : Dialogue
{

    public string[] dialogue;
    int progress;

	// Use this for initialization
	void Start () {
		
	}

    public override void triggerDialogue()
    {
        progress = 0;
        UpdateVars();
    }

    public override void UpdateVars()
    {
        if (progress < dialogue.Length)
        {
            text = dialogue[progress];
        }
        else
        {
            ended = true;
        }
    }

    public override void endedReset()
    {
        ended = false;
    }

    public override void dialogueStep(int choice)
    {
        progress++;
        UpdateVars();
    }
}

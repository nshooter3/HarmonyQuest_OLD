using UnityEngine;
using System.Collections;

public class Dialogue : MonoBehaviour {

    public int dialogueID;
    public string text;
    public string[] choices;
    public bool ended = false;
    public string DName, DPortrait, DMetadata, DTheme, mName, mData;
    public DialoguerInit dInit;

	// Use this for initialization
	void Start () {
        dInit = GameObject.FindObjectOfType<DialoguerInit>();
        triggerDialogue();
    }

    public void triggerDialogue()
    {
        dInit.dialogueID = dialogueID;
        dInit.triggerDialogue();
        UpdateVars();
    }

    public void dialogueStep(int choice)
    {
        dInit.dialogueStep(choice);
        UpdateVars();
    }

    public void UpdateVars()
    {
        text = dInit.text;
        choices = dInit.choices;
        ended = dInit.ended;
        DName = dInit.DName;
        DPortrait = dInit.DPortrait;
        DMetadata = dInit.DMetadata;
        DTheme = dInit.DTheme;
        if (dInit.mName != "")
        {
            mName = dInit.mName;
            mData = dInit.mData;
            dInit.mName = "";
            dInit.mData = "";
        }
    }

    public void endedReset()
    {
        ended = false;
        dInit.ended = false;
    }
}

using UnityEngine;
using System.Collections;

public class DialoguerInit : MonoBehaviour
{

    public int dialogueID;
    public string text;
    public string[] choices;
    public bool ended = false;
    public string DName, DPortrait, DMetadata, DTheme;
    public string mName, mData;

    void Awake()
    {
        Dialoguer.Initialize();
    }
    // Use this for initialization
    void Start()
    {
        Dialoguer.events.onStarted += onStarted;
        Dialoguer.events.onEnded += onEnded;
        Dialoguer.events.onTextPhase += onTextPhase;
        Dialoguer.events.onMessageEvent += onMessageEvent;        
        triggerDialogue();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void triggerDialogue()
    {
        Dialoguer.StartDialogue(dialogueID, DialoguerCallback);
    }

    public void dialogueStep(int choice)
    {
        if (choices == null || choice < 0 || choice >= choices.Length)
        {
            Dialoguer.ContinueDialogue();
        }
        else
        {
            Dialoguer.ContinueDialogue(choice);
        }
    }

    public void onTextPhase(DialoguerTextData data)
    {
        text = data.text;
        DName = data.name;
        DPortrait = data.portrait;
        DMetadata = data.metadata;
        DTheme = data.theme;
        choices = data.choices;
    }

    public void onMessageEvent(string name, string data)
    {
        mName = name;
        mData = data;
    }

    private void DialoguerCallback()
    {

    }

    private void onStarted()
    {

    }

    private void onEnded()
    {
        ended = true;
        Debug.Log("ended yeah");
    }

}

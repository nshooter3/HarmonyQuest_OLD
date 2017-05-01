using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public abstract class SceneScript : MonoBehaviour {

    public abstract void UpdateScene();
    public abstract void StartScene();

    //Actions to be carried out one at a time, in order
    public Queue<Action> actionQueue;
    Action action;

    //commandInProgress determines whether or not the object is ready for a new command
    // wait freezes the actionqueue until it is set back to false
    public bool commandInProgress, wait;

    //Used for wait function. If dur is greater than zero, a wait command is in progress.
    float dur;

    public static SceneScript instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        actionQueue = new Queue<Action>();
    }

    public void UpdateQueue()
    {
        if (!wait)
        {
            //Wait function update logic
            if (dur > 0)
            {
                dur -= Time.deltaTime;
                if (dur <= 0)
                {
                    instance.commandInProgress = false;
                }
            }
            else if (!commandInProgress)
            {
                if (actionQueue.Count > 0)
                {
                    action = actionQueue.Dequeue();
                    action();
                }
            }
        }
    }

    //Waits for dur seconds before being able to recieve a new command
    public void Wait(float dur)
    {
        SceneScript.instance.commandInProgress = true;
        this.dur = dur;
    }

    //Plays sound
    public void PlaySound(AudioSource sound)
    {
        sound.Play();
    }

    //Starts dialogue
    public void InitDialogue(PlayerMovementOverworld pMov, GameObject dialogue)
    {
        pMov.InitDialogue(dialogue);
    }

    public void ToggleOnDialogueUI()
    {
        FindObjectOfType<PlayerMovementOverworld>().ToggleOnDialogueUI();
    }

    public void ToggleOffDialogueUI()
    {
        //Debug.Log("toggle");
        FindObjectOfType<PlayerMovementOverworld>().ToggleOffDialogueUI();
    }

    public void PlayCutsceneSong(string song, float vol)
    {
        MusicManager.instance.PlayCutsceneSongInit(song, vol);
    }

}

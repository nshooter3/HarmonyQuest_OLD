﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CutsceneEvent : MonoBehaviour {

    /*
    NOTE: WHEN ATTACHED TO CAMERA, ONLY USE MOVE METHOD
    */

    Animator anim;

    //Determines whether or not the object is ready for a new command
    public bool commandInProgress;

    //Used for movement
    Vector3 start, destination;
    float speed;
    bool moving;

    //Used for waiting. If dur is greater than zero, a wait command is in progress.
    float dur;

    //Actions to be carried out one at a time, in order
    public Queue<Action> actionQueue;
    Action action;

    //Needs to be instantiated early, due to actionQueue being used in scene script start methods
    void Awake()
    {
        actionQueue = new Queue<Action>();
    }

	// Use this for initialization
	void Start () {
        commandInProgress = false;
        moving = false;
        speed = 1;
        dur = 0;
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (commandInProgress)
        {
            //Move update logic
            if (moving)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * speed);
                if (Vector2.Distance(start, transform.position) >= Vector2.Distance(start, destination))
                {
                    transform.position = destination;
                    moving = false;
                    commandInProgress = false;
                    if (anim != null)
                        anim.SetBool("IsMoving", false);
                }
            }

            //Wait update logic
            if (dur > 0)
            {
                dur -= Time.deltaTime;
                if (dur <= 0)
                {
                    commandInProgress = false;
                }
            }
        }
        else
        {
            if (actionQueue.Count > 0)
            {
                action = actionQueue.Dequeue();
                action();
            }
        }
	}

    //Moves gameObject towards destination
    public void Move(Vector3 destination, float speed, string dir)
    {
        commandInProgress = true;
        ChangeDirection(dir);
        start = gameObject.transform.position;
        this.destination = destination;
        this.speed = speed;
        moving = true;
        if (anim != null)
            anim.SetBool("IsMoving", true);
    }

    //Changes direction gameObject is facing (Happens instantly, follow up with wait command if delay is warranted)
    public void ChangeDirection(string dir)
    {
        if (anim != null)
        {
            switch (dir)
            {
                case "up":
                    anim.SetInteger("Direction", 0);
                    break;
                case "down":
                    anim.SetInteger("Direction", 2);
                    break;
                case "left":
                    anim.SetInteger("Direction", 3);
                    break;
                case "right":
                    anim.SetInteger("Direction", 1);
                    break;
            }
        }
    }

    //Calls animation trigger (Call wait command if pause is desired, will not acknowledge animation play time when awaiting next command)
    //STILL NEEDS TO BE TESTED
    public void PlayAnimation(string animTrigger)
    {
        if (anim != null)
        {
            anim.SetTrigger(animTrigger);
        }
    }

    //FOR PLAYER/NPCs ONLY, MUST HAVE EMOTE OBJECT
    public void PlayAnimationEmote(string animTrigger)
    {
        EmoteAnimator emote = GetComponentInChildren<EmoteAnimator>();
        if (emote != null)
        {
            emote.SetEmotion(animTrigger);
        }
    }

    //Waits for dur seconds before being able to recieve a new command
    public void Wait(float dur)
    {
        commandInProgress = true;
        this.dur = dur;
    }

    //Makes object visible
    public void EnableRenderer()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    //Makes object invisible
    public void DisableRenderer()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
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
        GameObject.FindObjectOfType<PlayerMovementOverworld>().ToggleOnDialogueUI();
    }

    public void ToggleOffDialogueUI()
    {
        GameObject.FindObjectOfType<PlayerMovementOverworld>().ToggleOffDialogueUI();
    }

    public void PlayCutsceneSong(string song, float vol)
    {
        MusicManager.MM.PlayCutsceneSongInit(song, vol);
    }

}
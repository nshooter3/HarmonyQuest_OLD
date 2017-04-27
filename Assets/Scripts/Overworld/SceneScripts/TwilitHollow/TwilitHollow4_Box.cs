using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TwilitHollow4_Box : SceneScript {

    public CutsceneEvent dad, cam;
    public ParticleSystem teleport;
    public CutsceneTrigger c1;

    public Transform d1, d2, d3, box, boxMount;
    Color dadCol;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        UpdateQueue();
        if (c1 != null && c1.isCol && GlobalVars.instance.saveData.hollowProgress == 4 && c1.gameObject.activeSelf &&
            PlayerMovementOverworld.instance.playerState == PlayerMovementOverworld.PlayerState.Default)
        {
            StartDialogue2();
        }
        if (PlayerMovementOverworld.instance.mName == "CutsceneTeleport")
        {
            CutsceneTeleport();
            PlayerMovementOverworld.instance.mName = "";
            PlayerMovementOverworld.instance.mData = "";
        }
        else if (PlayerMovementOverworld.instance.mName == "CutsceneEnd")
        {
            CutsceneEnd();
            PlayerMovementOverworld.instance.mName = "";
            PlayerMovementOverworld.instance.mData = "";
        }
    }

    // Use this for initialization
    public override void StartScene()
    {
        //GlobalVars.instance.saveData.hollowProgress = 5;
        teleport.transform.parent = dad.transform;
        dadCol = dad.GetComponent<SpriteRenderer>().color;
        if (GlobalVars.instance.saveData.hollowProgress == 3)
        {
            CutsceneStart();
        }
        else if (GlobalVars.instance.saveData.hollowProgress == 4)
        {
            dad.transform.position = d2.position;
        }
        else
        {
            c1.gameObject.SetActive(false);
        }
        if(GlobalVars.instance.saveData.hollowProgress > 4){
            box.position = boxMount.position;
        }
    }

    public override void UpdateScene()
    {

    }

    void CutsceneStart()
    {
        dad.transform.position = d1.position;
        PlayerMovementOverworld.instance.InitPlayerInteract();
        actionQueue.Enqueue(new Action(() => { dad.ChangeDirectionDiagonal(true, true); }));
        actionQueue.Enqueue(new Action(() => { Wait(1.5f); }));
        actionQueue.Enqueue(new Action(() => { dad.ChangeDirectionDiagonal(true, false); }));
        actionQueue.Enqueue(new Action(() => { Wait(1f); }));
        actionQueue.Enqueue(new Action(() => { InitDialogue(PlayerMovementOverworld.instance, gameObject); }));
    }

    void CutsceneTeleport()
    {
        actionQueue.Enqueue(new Action(() => { ToggleOffDialogueUI(); }));
        actionQueue.Enqueue(new Action(() => { Wait(1f); }));
        actionQueue.Enqueue(new Action(() => { GlobalFunctions.instance.DBZTeleport(dad.GetComponent<SpriteRenderer>()); }));
        actionQueue.Enqueue(new Action(() => { teleport.Play(); }));
        actionQueue.Enqueue(new Action(() => { Wait(1f); }));
        actionQueue.Enqueue(new Action(() => { dad.transform.position = d2.position; }));
        actionQueue.Enqueue(new Action(() => { dad.GetComponent<SpriteRenderer>().color = dadCol; }));
        actionQueue.Enqueue(new Action(() => { PlayerMovementOverworld.instance.InitPlayerDefaultFromDialogue(); }));
        actionQueue.Enqueue(new Action(() => { GlobalVars.instance.saveData.hollowProgress = 4; }));
    }

    void StartDialogue2()
    {
       InitDialogue(PlayerMovementOverworld.instance, gameObject);
       c1.gameObject.SetActive(false);
    }

    void CutsceneEnd()
    {
        actionQueue.Enqueue(new Action(() => { ToggleOffDialogueUI(); }));
        actionQueue.Enqueue(new Action(() => { dad.Move(d3.position, 4, "", false); }));
        actionQueue.Enqueue(new Action(() => { PlayerMovementOverworld.instance.InitPlayerDefaultFromDialogue(); }));
        actionQueue.Enqueue(() => GlobalVars.instance.saveData.hollowProgress = 5);
    }
}

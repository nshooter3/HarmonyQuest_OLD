using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TwilitHollow4_Box : SceneScript {

    public CutsceneEvent dad, cam;
    public ParticleSystem teleport;
    public CutsceneTrigger c1;

    public Transform d1, d2, d3;
    Color dadCol;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
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
        //GlobalVars.instance.saveData.hollowProgress = 3;
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
    }

    public override void UpdateScene()
    {

    }

    void CutsceneStart()
    {
        dad.transform.position = d1.position;
        PlayerMovementOverworld.instance.InitPlayerInteract();
        cam.actionQueue.Enqueue(new Action(() => { dad.ChangeDirectionDiagonal(true, true); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(1.5f); }));
        cam.actionQueue.Enqueue(new Action(() => { dad.ChangeDirectionDiagonal(true, false); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(1f); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.InitDialogue(PlayerMovementOverworld.instance, gameObject); }));
    }

    void CutsceneTeleport()
    {
        cam.actionQueue.Enqueue(new Action(() => { cam.ToggleOffDialogueUI(); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(1f); }));
        cam.actionQueue.Enqueue(new Action(() => { GlobalFunctions.instance.DBZTeleport(dad.GetComponent<SpriteRenderer>()); }));
        cam.actionQueue.Enqueue(new Action(() => { teleport.Play(); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(1f); }));
        cam.actionQueue.Enqueue(new Action(() => { dad.transform.position = d2.position; }));
        cam.actionQueue.Enqueue(new Action(() => { dad.GetComponent<SpriteRenderer>().color = dadCol; }));
        cam.actionQueue.Enqueue(new Action(() => { PlayerMovementOverworld.instance.InitPlayerDefaultFromDialogue(); }));
        cam.actionQueue.Enqueue(new Action(() => { GlobalVars.instance.saveData.hollowProgress = 4; }));
    }

    void StartDialogue2()
    {
       cam.InitDialogue(PlayerMovementOverworld.instance, gameObject);
       c1.gameObject.SetActive(false);
    }

    void CutsceneEnd()
    {
        cam.actionQueue.Enqueue(new Action(() => { cam.ToggleOffDialogueUI(); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(1f); }));
        cam.actionQueue.Enqueue(new Action(() => { dad.Move(d3.position, 4); }));
        cam.actionQueue.Enqueue(new Action(() => { PlayerMovementOverworld.instance.InitPlayerDefaultFromDialogue(); }));
        cam.actionQueue.Enqueue(() => GlobalVars.instance.saveData.hollowProgress = 5);
    }
}

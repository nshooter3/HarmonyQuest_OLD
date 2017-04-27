using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TwilitHollow5_Pond : SceneScript {

    public Transform d1, d2, d3, d4, d5, d6, d7, d8, d9, box1, box2, boxMount1, boxMount2;
    public CutsceneTrigger c1, c2, c3;
    public CutsceneEvent dad, cam;
    Color dadCol;
    public ParticleSystem teleport;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UpdateQueue();
        //Debug.Log("Hollow Progress: " + GlobalVars.instance.saveData.hollowProgress);
        if (c1 != null && c1.isCol && GlobalVars.instance.saveData.hollowProgress == 5 && c1.gameObject.activeSelf &&
            PlayerMovementOverworld.instance.playerState == PlayerMovementOverworld.PlayerState.Default)
        {
            CutsceneAskName();
        }
        else if (c2 != null && c2.isCol && GlobalVars.instance.saveData.hollowProgress == 6 && c2.gameObject.activeSelf &&
            PlayerMovementOverworld.instance.playerState == PlayerMovementOverworld.PlayerState.Default)
        {
            CutscenePuzzle();
        }
        else if (c3 != null && c3.isCol && GlobalVars.instance.saveData.hollowProgress == 7 && c3.gameObject.activeSelf &&
            PlayerMovementOverworld.instance.playerState == PlayerMovementOverworld.PlayerState.Default)
        {
            CutscenePuzzleComplete();
        }

        if (PlayerMovementOverworld.instance.mName == "CutscenePostIntro")
        {
            CutscenePostIntro();
            PlayerMovementOverworld.instance.mName = "";
            PlayerMovementOverworld.instance.mData = "";
        }
        else if (PlayerMovementOverworld.instance.mName == "CutsceneTeleport")
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
        //GlobalVars.instance.saveData.hollowProgress = 8;
        teleport.transform.parent = dad.transform;
        dadCol = dad.GetComponent<SpriteRenderer>().color;
        if (GlobalVars.instance.saveData.hollowProgress == 5)
        {
            CutsceneIntro();
        }
        else if (GlobalVars.instance.saveData.hollowProgress == 6)
        {
            dad.transform.position = d7.position;
        }
        else if (GlobalVars.instance.saveData.hollowProgress == 7)
        {
            dad.transform.position = d8.position;
        }
        if (GlobalVars.instance.saveData.hollowProgress > 7) {
            box1.position = boxMount1.position;
            box2.position = boxMount2.position;
        }
    }

    // Update is called once per frame
    public override void UpdateScene()
    {

    }

    void CutsceneIntro()
    {
        dad.transform.position = d1.position;
        actionQueue.Enqueue(() => dad.Move(d2.position, 4f));
        actionQueue.Enqueue(() => dad.ChangeDirectionDiagonal(true, false));
    }

    void CutsceneAskName()
    {
        c1.gameObject.SetActive(false);
        PlayerMovementOverworld.instance.InitPlayerInteract();
        actionQueue.Enqueue(() => Wait(0.5f));
        actionQueue.Enqueue(() => InitDialogue(PlayerMovementOverworld.instance, gameObject));
    }

    void CutscenePostIntro()
    {
        actionQueue.Enqueue(() => ToggleOffDialogueUI());
        actionQueue.Enqueue(() => PlayerMovementOverworld.instance.InitPlayerDefaultFromDialogue());
        actionQueue.Enqueue(() => GlobalVars.instance.saveData.hollowProgress = 6);
        actionQueue.Enqueue(() => dad.Move(d3.position, 5.5f));
        actionQueue.Enqueue(() => dad.Move(d4.position, 5.5f));
        actionQueue.Enqueue(() => dad.Move(d5.position, 5.5f));
        actionQueue.Enqueue(() => dad.Move(d6.position, 5.5f));
        actionQueue.Enqueue(() => dad.Move(d7.position, 5.5f));
        actionQueue.Enqueue(() => dad.ChangeDirectionDiagonal(true, false));
    }

    void CutscenePuzzle()
    {
        c2.gameObject.SetActive(false);
        PlayerMovementOverworld.instance.InitPlayerInteract();
        actionQueue.Enqueue(() => Wait(0.5f));
        actionQueue.Enqueue(() => InitDialogue(PlayerMovementOverworld.instance, gameObject));
    }

    void CutsceneTeleport()
    {
        actionQueue.Enqueue(() => ToggleOffDialogueUI());
        actionQueue.Enqueue(() => Wait(1f));
        actionQueue.Enqueue(() => GlobalFunctions.instance.DBZTeleport(dad.GetComponent<SpriteRenderer>()));
        actionQueue.Enqueue(() => teleport.Play());
        actionQueue.Enqueue(() => Wait(1f));
        actionQueue.Enqueue(() => dad.transform.position = d8.position);
        actionQueue.Enqueue(() => dad.GetComponent<SpriteRenderer>().color = dadCol);
        actionQueue.Enqueue(() => PlayerMovementOverworld.instance.InitPlayerDefaultFromDialogue());
        actionQueue.Enqueue(() => GlobalVars.instance.saveData.hollowProgress = 7);
    }

    void CutscenePuzzleComplete()
    {
        c3.gameObject.SetActive(false);
        PlayerMovementOverworld.instance.InitPlayerInteract();
        actionQueue.Enqueue(() => Wait(0.5f));
        actionQueue.Enqueue(() => InitDialogue(PlayerMovementOverworld.instance, gameObject));
    }

    void CutsceneEnd()
    {
        actionQueue.Enqueue(() => ToggleOffDialogueUI());
        actionQueue.Enqueue(() => PlayerMovementOverworld.instance.InitPlayerDefaultFromDialogue());
        actionQueue.Enqueue(() => GlobalVars.instance.saveData.hollowProgress = 8);
        actionQueue.Enqueue(() => dad.Move(d9.position, 4f));
    }
}

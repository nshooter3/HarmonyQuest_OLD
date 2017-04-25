using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TwilitHollow5_Pond : SceneScript {

    public Transform d1, d2, d3, d4, d5, d6, d7, d8, d9;
    public CutsceneTrigger c1, c2, c3;
    public CutsceneEvent dad, cam;
    Color dadCol;
    public ParticleSystem teleport;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("Hollow Progress: " + GlobalVars.instance.saveData.hollowProgress);
        if (c1 != null && c1.isCol && GlobalVars.instance.saveData.hollowProgress == 4 && c1.gameObject.activeSelf &&
            PlayerMovementOverworld.instance.playerState == PlayerMovementOverworld.PlayerState.Default)
        {
            CutsceneAskName();
        }
        else if (c2 != null && c2.isCol && GlobalVars.instance.saveData.hollowProgress == 5 && c2.gameObject.activeSelf &&
            PlayerMovementOverworld.instance.playerState == PlayerMovementOverworld.PlayerState.Default)
        {
            CutscenePuzzle();
        }
        else if (c3 != null && c3.isCol && GlobalVars.instance.saveData.hollowProgress == 6 && c3.gameObject.activeSelf &&
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
        teleport.transform.parent = dad.transform;
        dadCol = dad.GetComponent<SpriteRenderer>().color;
        GlobalVars.instance.saveData.hollowProgress = 4;
        if (GlobalVars.instance.saveData.hollowProgress == 4)
        {
            CutsceneIntro();
        }
    }

    // Update is called once per frame
    public override void UpdateScene()
    {

    }

    void CutsceneIntro()
    {
        dad.transform.position = d1.position;
        dad.actionQueue.Enqueue(() => dad.Move(d2.position, 4f));
        dad.actionQueue.Enqueue(() => dad.ChangeDirectionDiagonal(true, false));
    }

    void CutsceneAskName()
    {
        c1.gameObject.SetActive(false);
        PlayerMovementOverworld.instance.InitPlayerInteract();
        dad.actionQueue.Enqueue(() => cam.Wait(0.5f));
        dad.actionQueue.Enqueue(() => cam.InitDialogue(PlayerMovementOverworld.instance, gameObject));
    }

    void CutscenePostIntro()
    {
        dad.actionQueue.Enqueue(() => cam.ToggleOffDialogueUI());
        dad.actionQueue.Enqueue(() => PlayerMovementOverworld.instance.InitPlayerDefaultFromDialogue());
        dad.actionQueue.Enqueue(() => GlobalVars.instance.saveData.hollowProgress = 5);
        dad.actionQueue.Enqueue(() => dad.Move(d3.position, 5.5f));
        dad.actionQueue.Enqueue(() => dad.Move(d4.position, 5.5f));
        dad.actionQueue.Enqueue(() => dad.Move(d5.position, 5.5f));
        dad.actionQueue.Enqueue(() => dad.Move(d6.position, 5.5f));
        dad.actionQueue.Enqueue(() => dad.Move(d7.position, 5.5f));
        dad.actionQueue.Enqueue(() => dad.ChangeDirectionDiagonal(true, false));
    }

    void CutscenePuzzle()
    {
        c2.gameObject.SetActive(false);
        PlayerMovementOverworld.instance.InitPlayerInteract();
        dad.actionQueue.Enqueue(() => cam.Wait(0.5f));
        dad.actionQueue.Enqueue(() => cam.InitDialogue(PlayerMovementOverworld.instance, gameObject));
    }

    void CutsceneTeleport()
    {
        dad.actionQueue.Enqueue(() => dad.ToggleOffDialogueUI());
        dad.actionQueue.Enqueue(() => dad.Wait(1f));
        dad.actionQueue.Enqueue(() => GlobalFunctions.instance.DBZTeleport(dad.GetComponent<SpriteRenderer>()));
        dad.actionQueue.Enqueue(() => teleport.Play());
        dad.actionQueue.Enqueue(() => dad.Wait(1f));
        dad.actionQueue.Enqueue(() => dad.transform.position = d8.position);
        dad.actionQueue.Enqueue(() => dad.GetComponent<SpriteRenderer>().color = dadCol);
        dad.actionQueue.Enqueue(() => PlayerMovementOverworld.instance.InitPlayerDefaultFromDialogue());
        dad.actionQueue.Enqueue(() => GlobalVars.instance.saveData.hollowProgress = 6);
    }

    void CutscenePuzzleComplete()
    {
        c3.gameObject.SetActive(false);
        PlayerMovementOverworld.instance.InitPlayerInteract();
        dad.actionQueue.Enqueue(() => cam.Wait(0.5f));
        dad.actionQueue.Enqueue(() => cam.InitDialogue(PlayerMovementOverworld.instance, gameObject));
    }

    void CutsceneEnd()
    {
        dad.actionQueue.Enqueue(() => cam.ToggleOffDialogueUI());
        dad.actionQueue.Enqueue(() => PlayerMovementOverworld.instance.InitPlayerDefaultFromDialogue());
        dad.actionQueue.Enqueue(() => GlobalVars.instance.saveData.hollowProgress = 7);
        dad.actionQueue.Enqueue(() => dad.Move(d9.position, 4f));
    }
}

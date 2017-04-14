using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Hallway : SceneScript
{
    /*
    public CutsceneTrigger c1, c2, c3, c4;
    public GameObject dialogue;
    PlayerMovementOverworld playerMov;
    int progress = 0;
    public CutsceneEvent npc, cam, player;
    public List<Transform> d, c;
    public AudioSource runAway;

    public void Start()
    {
        
        UpdateScene();
        playerMov = GameObject.FindWithTag("Player").GetComponent<PlayerMovementOverworld>();

        if (GlobalVars.GV.saveData.hallwayProgress == -1)
        {
            InitNPCMovement();
            GlobalVars.GV.saveData.hallwayProgress = 0;
            GlobalVars.GV.SetDialoguerVars();
        }
        else if (GlobalVars.GV.saveData.hallwayProgress < 4)
        {
            npc.transform.position = d[0].transform.position;
        }
        else
        {
            npc.actionQueue.Enqueue(new Action(() => { npc.DisableRenderer(); }));
        }
        

    }
    */
    public override void StartScene()
    {
    }
    public override void UpdateScene()
    {
        
    }
    /*
    public void Update()
    {
        
        if (playerMov.playerState == PlayerMovementOverworld.PlayerState.Default)
        {
            if ((c1.isCol && GlobalVars.GV.saveData.hallwayProgress == 0)
            || (c2.isCol && GlobalVars.GV.saveData.hallwayProgress == 1)
            || (c3.isCol && GlobalVars.GV.saveData.hallwayProgress == 2))
            {
                playerMov.InitDialogue(dialogue);
            }
            else if(c4.isCol && GlobalVars.GV.saveData.hallwayProgress == 3)
            {
                NPCCutscene();
            }
        }

        if (playerMov.mName == "NPCEnd")
        {
            Debug.Log("yeah");
            NPCEnd();
            playerMov.mName = "";
            playerMov.mData = "";
        }
        else if (playerMov.mName == "NPCEnd2")
        {
            Debug.Log("yeah2");
            NPCEnd2();
            playerMov.mName = "";
            playerMov.mData = "";
        }
    }

    void InitNPCMovement()
    {
        npc.actionQueue.Enqueue(new Action(() => { npc.Wait(0.75f); }));
        npc.actionQueue.Enqueue(new Action(() => { npc.ChangeDirection("up"); }));
        npc.actionQueue.Enqueue(new Action(() => { npc.PlayAnimationEmote("Surprise"); }));
        npc.actionQueue.Enqueue(new Action(() => { npc.Wait(0.5f); }));
        npc.actionQueue.Enqueue(new Action(() => { npc.PlaySound(runAway); }));
        npc.actionQueue.Enqueue(new Action(() => { npc.Move(d[0].position, 6, "down"); }));
    }

    void NPCCutscene()
    {
        playerMov.InitPlayerInteract();

        d[0].transform.position = new Vector3(playerMov.transform.position.x, d[0].transform.position.y, d[0].transform.position.z);
        d[1].transform.position = new Vector3(playerMov.transform.position.x, d[1].transform.position.y, d[1].transform.position.z);

        npc.transform.position = d[0].transform.position;

        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(0.5f); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Move(new Vector3(cam.transform.position.x, c[0].position.y, -10), 1.7f, ""); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(1.5f); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Move(new Vector3(cam.transform.position.x, playerMov.transform.position.y - 1, -10), 1.5f, ""); }));

        npc.actionQueue.Enqueue(new Action(() => { npc.Wait(3); }));
        npc.actionQueue.Enqueue(new Action(() => { npc.PlayCutsceneSong("Baxter_Temp", 0.2f); }));
        npc.actionQueue.Enqueue(new Action(() => { npc.ChangeDirection("up"); }));
        npc.actionQueue.Enqueue(new Action(() => { npc.Wait(0.5f); }));
        npc.actionQueue.Enqueue(new Action(() => { npc.Move(new Vector3(playerMov.transform.position.x, playerMov.transform.position.y - 0.5f), 2, "up"); }));
        npc.actionQueue.Enqueue(new Action(() => { npc.Wait(1); }));
        npc.actionQueue.Enqueue(new Action(() => { npc.InitDialogue(playerMov, dialogue); } ));
    }

    public void NPCEnd()
    {
        playerMov.ToggleOffDialogueUI();
        npc.actionQueue.Enqueue(new Action(() => { npc.Wait(0.5f); }));
        npc.actionQueue.Enqueue(new Action(() => { npc.ChangeDirection("down"); }));
        npc.actionQueue.Enqueue(new Action(() => { npc.Wait(0.5f); }));
        npc.actionQueue.Enqueue(new Action(() => { npc.Move(d[1].transform.position, 6, "down"); }));
        npc.actionQueue.Enqueue(new Action(() => { npc.Wait(1f); }));
        npc.actionQueue.Enqueue(new Action(() => { npc.Move(new Vector3(playerMov.transform.position.x, playerMov.transform.position.y - 2f), 2, "up"); }));
        npc.actionQueue.Enqueue(new Action(() => { npc.Wait(0.5f); }));
        npc.actionQueue.Enqueue(new Action(() => { npc.ToggleOnDialogueUI(); }));
    }

    public void NPCEnd2()
    {
        playerMov.ToggleOffDialogueUI();
        npc.actionQueue.Enqueue(new Action(() => { npc.Wait(1); }));
        npc.actionQueue.Enqueue(new Action(() => { npc.Move(d[1].position, 6, "down"); }));
        npc.actionQueue.Enqueue(new Action(() => { npc.DisableRenderer(); }));

        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(2f); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Move(new Vector3(playerMov.transform.position.x, playerMov.transform.position.y, -10), 1, ""); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.ToggleOnDialogueUI(); }));

    }*/

}
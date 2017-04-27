using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TwilitHollow2_Boulder : SceneScript {

    public GameObject rock;
    public CutsceneEvent dad, cam;
    public Transform d1, d2, d3, d4, d5, c1;
    public ParticleSystem rockExplode, rockSmoke;
    public CutsceneTrigger ct1;
    Vector3 camPos;
    

    bool rockExploded = false;

    public void Start()
    {

    }

    // Use this for initialization
    public override void StartScene()
    {
        //TODO Temp, get rid of this later
        //GlobalVars.instance.saveData.hollowProgress = 2;
        //End temp
        
        if (GlobalVars.instance.saveData.hollowProgress == 1)
        {
            GlobalVars.instance.saveData.hollowProgress = 2;
            dad.GetComponent<Collider2D>().enabled = false;
            dad.transform.position = d1.position;
            actionQueue.Enqueue(new Action(() => { dad.Move(d2.position, 4); }));
            actionQueue.Enqueue(new Action(() => { dad.Move(d3.position, 4); }));
        }
        else if (GlobalVars.instance.saveData.hollowProgress == 2)
        {
            dad.GetComponent<Collider2D>().enabled = false;
            dad.transform.position = d3.position;
            dad.ChangeDirectionDiagonal(false, true);
        }
        else
        {
            rock.SetActive(false);
            ct1.gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        UpdateQueue();
        if (ct1.gameObject.activeSelf && ct1.isCol && PlayerMovementOverworld.instance.playerState == PlayerMovementOverworld.PlayerState.Default && GlobalVars.instance.saveData.hollowProgress == 2)
        {
            CutsceneInit();
        }
        if (PlayerMovementOverworld.instance.mName == "CutsceneBreakRock")
        {
            CutsceneBreakRock();
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

    public override void UpdateScene()
    {

    }

    void CutsceneInit()
    {
        camPos = cam.transform.position;
        ct1.gameObject.SetActive(false);
        PlayerMovementOverworld.instance.InitPlayerInteract();
        actionQueue.Enqueue(new Action(() => { cam.Move(c1.position, 1f); }));
        actionQueue.Enqueue(new Action(() => { Wait(0.5f); }));
        actionQueue.Enqueue(new Action(() => { dad.ChangeDirectionDiagonal(true, false); }));
        actionQueue.Enqueue(new Action(() => { Wait(1); }));
        actionQueue.Enqueue(new Action(() => { InitDialogue(PlayerMovementOverworld.instance, gameObject); }));
    }

    void CutsceneBreakRock()
    {
        actionQueue.Enqueue(new Action(() => { ToggleOffDialogueUI(); }));
        actionQueue.Enqueue(new Action(() => { Wait(0.5f); }));
        actionQueue.Enqueue(new Action(() => { dad.Move(d4.position, 4); }));
        actionQueue.Enqueue(new Action(() => { Wait(1f); }));
        actionQueue.Enqueue(new Action(() => { GlobalFunctions.instance.AdjustPositionOverTime(new Vector3 (dad.transform.position.x, dad.transform.position.y + 0.15f, dad.transform.position.z), 
            dad.transform.position, 0.1f, dad.transform); }));
        actionQueue.Enqueue(new Action(() => { StartCoroutine(BlowUpRock()); }));
        actionQueue.Enqueue(new Action(() => { Wait(1.5f); }));
        actionQueue.Enqueue(new Action(() => { dad.ChangeDirectionDiagonal(true, false); }));
        actionQueue.Enqueue(new Action(() => { Wait(1f); }));
        actionQueue.Enqueue(new Action(() => { ToggleOnDialogueUI(); }));
    }

    void CutsceneEnd()
    {
        actionQueue.Enqueue(new Action(() => { ToggleOffDialogueUI(); }));
        actionQueue.Enqueue(new Action(() => { Wait(0.5f); }));
        actionQueue.Enqueue(new Action(() => { dad.Move(d5.position, 4); }));
        actionQueue.Enqueue(new Action(() => { cam.Move(camPos, 1f); }));
        actionQueue.Enqueue(new Action(() => { PlayerMovementOverworld.instance.InitPlayerDefaultFromDialogue(); }));
        actionQueue.Enqueue(new Action(() => {GlobalVars.instance.saveData.hollowProgress = 3; }));
    }

    IEnumerator BlowUpRock()
    {
        SFXManager.instance.Spawn("RockBreak");
        rockExplode.Play();
        rockSmoke.Play();
        rockExploded = true;
        yield return new WaitForSeconds(.25f);
        rock.SetActive(false);
    }
}

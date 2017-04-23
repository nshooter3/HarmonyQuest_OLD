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
            dad.actionQueue.Enqueue(new Action(() => { dad.Move(d2.position, 4); }));
            dad.actionQueue.Enqueue(new Action(() => { dad.Move(d3.position, 4); }));
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
        cam.actionQueue.Enqueue(new Action(() => { cam.Move(c1.position, 1f); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(0.5f); }));
        cam.actionQueue.Enqueue(new Action(() => { dad.ChangeDirectionDiagonal(true, false); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(1); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.InitDialogue(PlayerMovementOverworld.instance, gameObject); }));
    }

    void CutsceneBreakRock()
    {
        cam.actionQueue.Enqueue(new Action(() => { dad.ToggleOffDialogueUI(); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(0.5f); }));
        cam.actionQueue.Enqueue(new Action(() => { dad.Move(d4.position, 4); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(1f); }));
        cam.actionQueue.Enqueue(new Action(() => { GlobalFunctions.instance.AdjustPositionOverTime(new Vector3 (dad.transform.position.x, dad.transform.position.y + 0.15f, dad.transform.position.z), 
            dad.transform.position, 0.1f, dad.transform); }));
        cam.actionQueue.Enqueue(new Action(() => { StartCoroutine(BlowUpRock()); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(1.5f); }));
        cam.actionQueue.Enqueue(new Action(() => { dad.ChangeDirectionDiagonal(true, false); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(1f); }));
        cam.actionQueue.Enqueue(new Action(() => { dad.ToggleOnDialogueUI(); }));
    }

    void CutsceneEnd()
    {
        cam.actionQueue.Enqueue(new Action(() => { dad.ToggleOffDialogueUI(); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(0.5f); }));
        cam.actionQueue.Enqueue(new Action(() => { dad.Move(d5.position, 4); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(1f); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Move(camPos, 1f); }));
        cam.actionQueue.Enqueue(new Action(() => { PlayerMovementOverworld.instance.InitPlayerDefaultFromDialogue(); }));
        cam.actionQueue.Enqueue(new Action(() => {GlobalVars.instance.saveData.hollowProgress = 3; }));
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

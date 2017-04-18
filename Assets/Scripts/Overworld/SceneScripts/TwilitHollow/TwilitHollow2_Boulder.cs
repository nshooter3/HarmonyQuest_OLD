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

    bool rockExploded = false;

    // Use this for initialization
    public override void StartScene()
    {
        //TODO Temp, get rid of this later
        GlobalVars.instance.saveData.hollowProgress = 1;
        //End temp

        if (GlobalVars.instance.saveData.hollowProgress == 1)
        {
            dad.transform.position = d1.position;
            dad.actionQueue.Enqueue(new Action(() => { dad.Move(d2.position, 4); }));
            dad.actionQueue.Enqueue(new Action(() => { dad.Move(d3.position, 4); }));
        }
        else
        {
            rock.SetActive(false);
            ct1.gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (ct1.isCol)
        {
            CutsceneInit();
        }
        if (PlayerMovementOverworld.instance.mName == "CutsceneWait1")
        {
            //CutsceneWait1();
            PlayerMovementOverworld.instance.mName = "";
            PlayerMovementOverworld.instance.mData = "";
        }
    }

    public override void UpdateScene()
    {

    }

    void CutsceneInit()
    {
        PlayerMovementOverworld.instance.InitPlayerInteract();
        dad.actionQueue.Enqueue(new Action(() => { cam.Move(c1.position, 1f); }));
        dad.actionQueue.Enqueue(new Action(() => { dad.Wait(3); }));
        dad.actionQueue.Enqueue(new Action(() => { dad.ChangeDirectionDiagonal(true, false); }));
        dad.actionQueue.Enqueue(new Action(() => { dad.Wait(1); }));
    }

    IEnumerator BlowUpRock()
    {
        rockExplode.Play();
        rockSmoke.Play();
        rockExploded = true;
        yield return new WaitForSeconds(.25f);
        rock.SetActive(false);
    }
}

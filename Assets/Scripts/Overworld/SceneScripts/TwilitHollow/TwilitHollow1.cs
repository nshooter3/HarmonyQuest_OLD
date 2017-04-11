using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TwilitHollow1 : SceneScript {

    public CutsceneEvent dad, cam;
    //Camera mounts
    public Transform cMount1, cMount2, cMount3;
    public FishingRod rod;

	// Use this for initialization
	void Start () {

    }

    void InitFishingCutscene()
    {
        PlayerMovementOverworld.instance.InitPlayerInteract();
        cam.transform.position = cMount1.position;
        cam.actionQueue.Enqueue(new Action(() => { cam.Move(cMount2.position, 1.5f); }));
        cam.actionQueue.Enqueue(new Action(() => { rod.Interact(); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(5); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Move(cMount3.position, 1.5f); }));
        cam.actionQueue.Enqueue(new Action(() => { PlayerMovementOverworld.instance.InitPlayerDefault(); }));
    }
	
	// Update is called once per frame
	public override void UpdateScene () {
		
	}
}

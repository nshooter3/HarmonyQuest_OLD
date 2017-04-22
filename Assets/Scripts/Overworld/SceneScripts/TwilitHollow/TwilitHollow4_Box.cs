using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TwilitHollow4_Box : SceneScript {

    public CutsceneEvent dad, cam;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // Use this for initialization
    public override void StartScene()
    {
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(2f); }));
        cam.actionQueue.Enqueue(new Action(() => {
            GlobalFunctions.instance.DBZTeleport(dad.GetComponent<SpriteRenderer>());
        }));
    }

    // Update is called once per frame
    public override void UpdateScene()
    {

    }
}

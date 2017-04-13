using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TwilitHollow1 : SceneScript {

    public CutsceneEvent dad, cam, player;
    public GameObject bike, bikePile, bikeDes, fishingBike, fishingBaby;
    Vector3 bikePileLower;
    //Camera mounts
    public Transform cMount1, cMount2, cMount3, dMount1, dMount2, dMount3;
    public FishingRod rod;
    public TextMesh author;
    Vector4 fadedCol, normCol;

	// Use this for initialization
	void Start () {
        normCol = author.color;
        fadedCol = normCol;
        fadedCol.w = 0;
        author.color = fadedCol;
        bikePileLower = new Vector3(bikePile.transform.position.x, bikePile.transform.position.y - 0.05f, bikePile.transform.position.z);
        InitFishingCutscene();
    }

    void InitFishingCutscene()
    {
        PlayerMovementOverworld.instance.InitPlayerInteract();
        cam.actionQueue.Enqueue(new Action(() => { player.DisableRenderer(); }));
        cam.transform.position = cMount1.position;
        dad.transform.position = dMount1.position;
        cam.actionQueue.Enqueue(new Action(() => { cam.Move(cMount2.position, 1.5f); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(1f); }));
        cam.actionQueue.Enqueue(new Action(() => { GlobalFunctions.instance.AdjustColorOverTimeTextMesh(fadedCol, normCol, 1, author); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(3.5f); }));
        cam.actionQueue.Enqueue(new Action(() => { GlobalFunctions.instance.AdjustColorOverTimeTextMesh(normCol, fadedCol, 1, author); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(2f); }));
        cam.actionQueue.Enqueue(new Action(() => { rod.Interact(); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(3.5f); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Move(cMount3.position, 2f); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(1); }));
        cam.actionQueue.Enqueue(new Action(() => { dad.ChangeDirectionDiagonal(false, false); }));
        cam.actionQueue.Enqueue(new Action(() => { fishingBike.SetActive(false); }));
        cam.actionQueue.Enqueue(new Action(() => { bike.SetActive(true); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(1); }));
        cam.actionQueue.Enqueue(new Action(() => { GlobalFunctions.instance.AdjustPositionOverTime(bike.transform.position, bikeDes.transform.position, 0.35f, bike.transform); }));
        cam.actionQueue.Enqueue(new Action(() => { GlobalFunctions.instance.AdjustRotationOverTime(bike.transform.eulerAngles, bikeDes.transform.eulerAngles, 0.35f, bike.transform); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(0.45f); }));
        cam.actionQueue.Enqueue(new Action(() => { GlobalFunctions.instance.AdjustPositionOverTime(bikePileLower, bikePile.transform.position, 0.1f, bikePile.transform); }));
        cam.actionQueue.Enqueue(new Action(() => { SFXManager.instance.Spawn("BikeCrash"); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(1.55f); }));
        cam.actionQueue.Enqueue(new Action(() => { dad.ChangeDirectionDiagonal(true, false); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(1f); }));
        cam.actionQueue.Enqueue(new Action(() => { rod.Interact(); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(5f); }));
        cam.actionQueue.Enqueue(new Action(() => { rod.Interact("baby"); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(3.5f); }));
        cam.actionQueue.Enqueue(new Action(() => { SFXManager.instance.Spawn("CryingBaby1"); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(4f); }));
        cam.actionQueue.Enqueue(new Action(() => { dad.GetComponentInChildren<EmoteAnimator>().SetEmotion("Surprise") ; }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(0.5f); }));
        cam.actionQueue.Enqueue(new Action(() => { fishingBaby.SetActive(false); }));
        cam.actionQueue.Enqueue(new Action(() => { dad.Move(dMount2.position, 4); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(0.5f); }));
        cam.actionQueue.Enqueue(new Action(() => { dad.ChangeDirectionDiagonal(true, false); }));
        cam.actionQueue.Enqueue(new Action(() => { player.EnableRenderer(); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(1f); }));
        cam.actionQueue.Enqueue(new Action(() => { SFXManager.instance.Spawn("CryingBaby2"); }));
        cam.actionQueue.Enqueue(new Action(() => { cam.Wait(5f); }));
        cam.actionQueue.Enqueue(new Action(() => { dad.Move(dMount3.position, 4); }));

        cam.actionQueue.Enqueue(new Action(() => { PlayerMovementOverworld.instance.InitPlayerDefault(); }));
    }
	
	// Update is called once per frame
	public override void UpdateScene () {
		
	}
}

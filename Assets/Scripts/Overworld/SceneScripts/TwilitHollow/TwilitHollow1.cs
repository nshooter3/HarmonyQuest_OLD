using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TwilitHollow1 : SceneScript {

    public CutsceneEvent dad, cam, player;
    public GameObject bike, bikePile, bikeDes, fishingBike, fishingBaby;
    Vector3 bikePileLower;
    public GameObject dialogue;
    //Camera mounts
    public Transform cMount1, cMount2, cMount3, dMount1, dMount2, dMount3;
    public FishingRod rod;
    public TextMesh author;
    Vector4 fadedCol, normCol;

	// Use this for initialization
	public override void StartScene () {
        normCol = author.color;
        fadedCol = normCol;
        fadedCol.w = 0;
        author.color = fadedCol;
        bikePileLower = new Vector3(bikePile.transform.position.x, bikePile.transform.position.y - 0.05f, bikePile.transform.position.z);
        if (GlobalVars.instance.saveData.hollowProgress == 0)
        {
            InitFishingCutscene();
        }
    }

    void Update()
    {
        UpdateQueue();
        if (PlayerMovementOverworld.instance.mName == "CutsceneWait1")
        {
            CutsceneWait1();
            PlayerMovementOverworld.instance.mName = "";
            PlayerMovementOverworld.instance.mData = "";
        }
        else if (PlayerMovementOverworld.instance.mName == "CutsceneReelIn1")
        {
            CutsceneReelIn1();
            PlayerMovementOverworld.instance.mName = "";
            PlayerMovementOverworld.instance.mData = "";
        }
        else if (PlayerMovementOverworld.instance.mName == "CutsceneThrowBike")
        {
            CutsceneThrowBike();
            PlayerMovementOverworld.instance.mName = "";
            PlayerMovementOverworld.instance.mData = "";
        }
        else if (PlayerMovementOverworld.instance.mName == "CutsceneLowerBait1")
        {
            CutsceneLowerBait1();
            PlayerMovementOverworld.instance.mName = "";
            PlayerMovementOverworld.instance.mData = "";
        }
        else if (PlayerMovementOverworld.instance.mName == "CutsceneGrabBaby")
        {
            CutsceneGrabBaby();
            PlayerMovementOverworld.instance.mName = "";
            PlayerMovementOverworld.instance.mData = "";
        }
        else if (PlayerMovementOverworld.instance.mName == "CutsceneLookAround")
        {
            CutsceneLookAround();
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

    void InitFishingCutscene()
    {
        PlayerMovementOverworld.instance.InitPlayerInteract();
        actionQueue.Enqueue(new Action(() => { player.DisableRenderer(PlayerMovementOverworld.instance.currentSprite); }));
        cam.transform.position = cMount1.position;
        //Debug.Log(cam.transform.position);
        dad.transform.position = dMount1.position;
        actionQueue.Enqueue(new Action(() => { cam.Move(cMount2.position, 1.5f); }));
        actionQueue.Enqueue(new Action(() => { cam.Wait(1f); }));
        actionQueue.Enqueue(new Action(() => { GlobalFunctions.instance.AdjustColorOverTimeTextMesh(fadedCol, normCol, 1, author); }));
        actionQueue.Enqueue(new Action(() => { cam.Wait(3.5f); }));
        actionQueue.Enqueue(new Action(() => { GlobalFunctions.instance.AdjustColorOverTimeTextMesh(normCol, fadedCol, 1, author); }));
        actionQueue.Enqueue(new Action(() => { cam.Wait(2f); }));
        actionQueue.Enqueue(new Action(() => { cam.InitDialogue(PlayerMovementOverworld.instance, dialogue);})) ;
    }

    void CutsceneWait1()
    {
        //CutsceneWait1
        actionQueue.Enqueue(new Action(() => { cam.ToggleOffDialogueUI(); }));
        actionQueue.Enqueue(new Action(() => { cam.Wait(1.5f); }));
        actionQueue.Enqueue(new Action(() => { dad.GetComponentInChildren<EmoteAnimator>().SetEmotion("Surprise"); }));
        actionQueue.Enqueue(new Action(() => { cam.Wait(0.75f); }));
        actionQueue.Enqueue(new Action(() => { cam.ToggleOnDialogueUI(); }));
    }

    void CutsceneReelIn1()
    {
        // CutsceneReelIn1
        actionQueue.Enqueue(new Action(() => { cam.ToggleOffDialogueUI(); }));
        actionQueue.Enqueue(new Action(() => { rod.Interact(); }));
        actionQueue.Enqueue(new Action(() => { cam.Wait(3.5f); }));
        actionQueue.Enqueue(new Action(() => { cam.ToggleOnDialogueUI(); }));
    }

    void CutsceneThrowBike()
    {
        //CutsceneThowBike
        actionQueue.Enqueue(new Action(() => { cam.ToggleOffDialogueUI(); }));
        actionQueue.Enqueue(new Action(() => { cam.Move(cMount3.position, 2f); }));
        actionQueue.Enqueue(new Action(() => { cam.Wait(1); }));
        actionQueue.Enqueue(new Action(() => { SFXManager.instance.Spawn("Wop"); }));
        actionQueue.Enqueue(new Action(() => { dad.ChangeDirectionDiagonal(false, false); }));
        actionQueue.Enqueue(new Action(() => { fishingBike.SetActive(false); }));
        actionQueue.Enqueue(new Action(() => { bike.SetActive(true); }));
        actionQueue.Enqueue(new Action(() => { cam.Wait(1); }));
        actionQueue.Enqueue(new Action(() => { SFXManager.instance.Spawn("Whoosh"); }));
        actionQueue.Enqueue(new Action(() => { GlobalFunctions.instance.AdjustPositionOverTime(bike.transform.position, bikeDes.transform.position, 0.35f, bike.transform); }));
        actionQueue.Enqueue(new Action(() => { GlobalFunctions.instance.AdjustRotationOverTime(bike.transform.eulerAngles, bikeDes.transform.eulerAngles, 0.35f, bike.transform); }));
        actionQueue.Enqueue(new Action(() => { cam.Wait(0.45f); }));
        actionQueue.Enqueue(new Action(() => { GlobalFunctions.instance.AdjustPositionOverTime(bikePileLower, bikePile.transform.position, 0.1f, bikePile.transform); }));
        actionQueue.Enqueue(new Action(() => { SFXManager.instance.Spawn("BikeCrash"); }));
        actionQueue.Enqueue(new Action(() => { cam.Wait(1.55f); }));
        actionQueue.Enqueue(new Action(() => { dad.ChangeDirectionDiagonal(true, false); }));
        actionQueue.Enqueue(new Action(() => { cam.Wait(1f); }));
        actionQueue.Enqueue(new Action(() => { cam.ToggleOnDialogueUI(); }));
    }

    void CutsceneLowerBait1(){
        //CutsceneLowerBait1
        actionQueue.Enqueue(new Action(() => { cam.ToggleOffDialogueUI(); }));
        actionQueue.Enqueue(new Action(() => { cam.Wait(1f); }));
        actionQueue.Enqueue(new Action(() => { rod.Interact(); }));
        actionQueue.Enqueue(new Action(() => { cam.Wait(5f); }));
        actionQueue.Enqueue(new Action(() => { rod.Interact("baby"); }));
        actionQueue.Enqueue(new Action(() => { cam.Wait(3.5f); }));
        actionQueue.Enqueue(new Action(() => { SFXManager.instance.Spawn("CryingBaby1"); }));
        actionQueue.Enqueue(new Action(() => { cam.Wait(4f); }));
        actionQueue.Enqueue(new Action(() => { dad.GetComponentInChildren<EmoteAnimator>().SetEmotion("Surprise"); }));
        actionQueue.Enqueue(new Action(() => { cam.Wait(0.5f); }));
        actionQueue.Enqueue(new Action(() => { cam.ToggleOnDialogueUI(); }));
    }

    void CutsceneGrabBaby()
    {
        //CutsceneGrabBaby
        actionQueue.Enqueue(new Action(() => { cam.ToggleOffDialogueUI(); }));
        actionQueue.Enqueue(new Action(() => { cam.Wait(0.5f); }));
        actionQueue.Enqueue(new Action(() => { fishingBaby.SetActive(false); }));
        actionQueue.Enqueue(new Action(() => { dad.Move(dMount2.position, 4); }));
        actionQueue.Enqueue(new Action(() => { cam.Wait(0.5f); }));
        actionQueue.Enqueue(new Action(() => { dad.ChangeDirectionDiagonal(true, false); }));
        actionQueue.Enqueue(new Action(() => { player.EnableRenderer(PlayerMovementOverworld.instance.currentSprite); }));
        actionQueue.Enqueue(new Action(() => { cam.Wait(1f); }));
        actionQueue.Enqueue(new Action(() => { SFXManager.instance.Spawn("CryingBaby2"); }));
        actionQueue.Enqueue(new Action(() => { cam.Wait(5f); }));
        actionQueue.Enqueue(new Action(() => { cam.ToggleOnDialogueUI(); }));
    }

    void CutsceneLookAround()
    {
        //CutsceneLookAround
        actionQueue.Enqueue(new Action(() => { cam.ToggleOffDialogueUI(); }));
        actionQueue.Enqueue(new Action(() => { cam.Wait(1); }));
        actionQueue.Enqueue(new Action(() => { dad.ChangeDirectionDiagonal(false, false); }));
        actionQueue.Enqueue(new Action(() => { cam.Wait(0.5f); }));
        actionQueue.Enqueue(new Action(() => { dad.ChangeDirectionDiagonal(false, true); }));
        actionQueue.Enqueue(new Action(() => { cam.Wait(0.5f); }));
        actionQueue.Enqueue(new Action(() => { dad.ChangeDirectionDiagonal(true, true); }));
        actionQueue.Enqueue(new Action(() => { cam.Wait(0.5f); }));
        actionQueue.Enqueue(new Action(() => { dad.ChangeDirectionDiagonal(true, false); }));
        actionQueue.Enqueue(new Action(() => { cam.Wait(1); }));
        actionQueue.Enqueue(new Action(() => { cam.ToggleOnDialogueUI(); }));
    }

    void CutsceneEnd()
    {
        //CutsceneEnd
        actionQueue.Enqueue(new Action(() => { cam.ToggleOffDialogueUI(); }));
        actionQueue.Enqueue(new Action(() => { cam.Wait(1); }));
        actionQueue.Enqueue(new Action(() => { dad.Move(dMount3.position, 4); }));
        actionQueue.Enqueue(new Action(() => { PlayerMovementOverworld.instance.InitPlayerDefaultFromDialogue(); }));
        actionQueue.Enqueue(new Action(() => {GlobalVars.instance.saveData.hollowProgress = 1;}));
    }

    // Update is called once per frame
    public override void UpdateScene () {
        
    }
}

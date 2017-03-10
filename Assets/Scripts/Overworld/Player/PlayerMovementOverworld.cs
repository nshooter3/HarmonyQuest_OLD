﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovementOverworld : MonoBehaviour {

    public static PlayerMovementOverworld instance;

    public float speed, actualSpeed;

    //Passed into RaycastX and RaycastY, determines width of parallel raycasts
    public float raycastRange;

    public GameObject interact, up, down, left, right;

    public Dialogue dlog;

    public Textbox textbox;

    private Animator anim;

    private Transform playerPos;

    public enum PlayerState { Default, Interacting, Cutscene, Battle };
    public PlayerState playerState;

    public GameObject[] interactables;

    Rigidbody2D rb;

    RaycastHit2D result;

    SceneScript sceneScript;

    //Freezes player while screen is black and song is switching
    private float freezeTimer;

    //Used to save/load files
    public DataManager dataManager;

    //Used to fade screen for scene transitions
    public FadeInFadeOut fade;

    //Used to update the camera state based on the player state
    public bool cameraCutsceneFlag, cameraDefaultFlag;

    //Used for dialoguer message events
    public string mName, mData;

    //Sets to true when the dialogue UI needs to disappear for a cutscene
    public bool cutsceneFlag;

    //Holds the player's emote gameobject for easy access
    public EmoteAnimator emote;

    //Both player colliders
    private CircleCollider2D circCol;
    private BoxCollider2D boxCol;

    void Awake()
    {
        Dialoguer.Initialize();
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        speed = 0.06f;//Stored value
        actualSpeed = speed;//Modified value that is actually used
        raycastRange = 0.05f;
        playerPos = gameObject.transform;
        rb = playerPos.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerState = PlayerState.Default;
        ToggleColliders(true);
        sceneScript = FindObjectOfType<SceneScript>();
        MusicManager.MM.Init();

        freezeTimer = 1;

        cameraCutsceneFlag = false;
        cameraDefaultFlag = false;

        cutsceneFlag = false;

        emote = playerPos.GetComponent<EmoteAnimator>();
        dlog = GameObject.FindObjectOfType<Dialogue>();
        textbox = GameObject.FindObjectOfType<Textbox>();
        dataManager = GameObject.FindObjectOfType<DataManager>();
        fade = GameObject.FindObjectOfType<FadeInFadeOut>();

        //Debug.Log(GlobalVars.GV.destination);

        //Set player start location
        SpawnSpot[] temp = FindObjectsOfType<SpawnSpot>();
        for (int i = 0; i < temp.Length; i++){
            if (temp[i].spawnName == GlobalVars.GV.saveData.destination)
            {
                playerPos.transform.position = temp[i].gameObject.transform.position;
                anim.SetInteger("Direction", temp[i].spawnDir);
            }
        }
    }

    void Update()
    {
        if (freezeTimer > 0)
        {
            freezeTimer -= Time.deltaTime;
        }
        else
        {
            CheckForKeyInput();
        }
    }

    private void Move(Vector3 dir)
    {
        //Applies movement based on vector3 from CheckForMove
        rb.velocity =  dir * actualSpeed * 50;
    }

    private void CheckForKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (playerState == PlayerState.Default)
        {
            CheckForDefaultInput();
        }
        else if(playerState == PlayerState.Interacting && !cutsceneFlag)
        {
            CheckForInteractableInput();
        }
    }

    private void CheckForInteractableInput() {
        dlog.UpdateVars();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (textbox.textLoading)
            {
                textbox.finishText();
                textbox.setNoChoiceTimer();
            }
            else if(!textbox.choiceUIMoving && !textbox.isNoChoiceTimerRunning())
            {
                AdvanceDialogue();
            }

            if (dlog != null && dlog.mName != "")
            {
                mName = dlog.mName;
                mData = dlog.mData;
                dlog.mName = "";
                dlog.mData = "";
            }

        }

        if (textbox.choicesActive && !textbox.choiceUIMoving) {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                textbox.choiceUp();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                textbox.choiceDown();
            }
        }
    }


    //Sets the text for the textbox dialogue and choices
    private void SetTextbox()
    {
        textbox.SetText(dlog.text, dlog.DName, dlog.DPortrait, dlog.DMetadata, dlog.DTheme);
        textbox.SetChoices(dlog.choices);
        //Debug.Log(dlog.text);
    }

    private void CheckForDefaultInput()
    {
        anim.SetBool("IsMoving", true);
        //Check for various directional keys/combinations for movement
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            Move(new Vector3(0.67f, 0.67f, 0));
            anim.SetInteger("Direction", 0);
            interact.transform.position = up.transform.position;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            Move(new Vector3(0.67f, -0.67f, 0));
            anim.SetInteger("Direction", 2);
            interact.transform.position = down.transform.position;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            Move(new Vector3(-0.67f, 0.67f, 0));
            anim.SetInteger("Direction", 0);
            interact.transform.position = up.transform.position;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            Move(new Vector3(-0.67f, -0.67f, 0));
            anim.SetInteger("Direction", 2);
            interact.transform.position = down.transform.position;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Move(new Vector3(1, 0, 0));
            anim.SetInteger("Direction", 1);
            interact.transform.position = right.transform.position;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Move(new Vector3(-1, 0, 0));
            anim.SetInteger("Direction", 3);
            interact.transform.position = left.transform.position;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            Move(new Vector3(0, 1, 0));
            anim.SetInteger("Direction", 0);
            interact.transform.position = up.transform.position;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Move(new Vector3(0, -1, 0));
            anim.SetInteger("Direction", 2);
            interact.transform.position = down.transform.position;
        }
        else
        {
            Move(new Vector3(0, 0, 0));
            anim.SetBool("IsMoving", false);
        }

        //Used to set speed depending on whether or not the player is dashing
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            actualSpeed = speed * 1.5f;
            //dataManager.Save(1);
        }
        else
        {
            actualSpeed = speed;
        }


        //Checks for interactable objects
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameObject temp = CheckForInteractable();
            if (temp != null)
            {
                InitDialogue(temp);
                //MusicManager.MM.LowerMusicInit(MusicManager.MM.sceneMusic.volume);
            }
        }
    }

    private GameObject CheckForInteractable()
    {
        /*for (int i = 0; i < interactables.Length; i++)
        {
            if (interactables[i].gameObject.GetComponent<BoxCollider2D>().bounds.Intersects(interact.GetComponent<BoxCollider2D>().bounds))
                return interactables[i];
        }*/

        switch (anim.GetInteger("Direction"))
        {
            case 0:
                result = GlobalFunctions.GF.RaycastY(transform, Vector2.up, 0.25f, 1 << LayerMask.NameToLayer("Interactable"), 0.05f);
                break;
            case 1:
                result = GlobalFunctions.GF.RaycastX(transform, Vector2.right, 0.334f, 1 << LayerMask.NameToLayer("Interactable"), 0.05f);
                break;
            case 2:
                result = GlobalFunctions.GF.RaycastY(transform, Vector2.down, 0.461f, 1 << LayerMask.NameToLayer("Interactable"), 0.05f);
                break;
            case 3:
                result = GlobalFunctions.GF.RaycastX(transform, Vector2.left, 0.368f, 1 << LayerMask.NameToLayer("Interactable"), 0.05f);
                break;
        }
        if (result.collider == null)
            return null;
        return result.collider.gameObject;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Checks for doors
        Door door = col.gameObject.GetComponent<Door>();
        if (col.gameObject.layer == 10 && door != null)
        {
            playerState = PlayerState.Cutscene;
            ToggleColliders(false);
            anim.SetBool("IsMoving", false);
            fade.FadeOut();
            door.Teleport();
            //TODO Set up teleportation destination
        }
    }

    //Starts dialogue
    public void InitDialogue(GameObject temp)
    {
        InitPlayerInteract();
        textbox.ToggleOn();
        dlog = temp.GetComponent<Dialogue>();
        dlog.triggerDialogue();
        SetTextbox();
    }

    public void InitPlayerInteract()
    {
        playerState = PlayerState.Interacting;
        ToggleColliders(false);
        cameraCutsceneFlag = true;
        anim.SetBool("IsMoving", false);
        sceneScript.UpdateScene();
    }

    public void InitPlayerDefaultFromDialogue()
    {
        textbox.ToggleOff();
        dlog.endedReset();
        InitPlayerDefault();
    }

    public void InitPlayerDefault()
    {
        GlobalVars.GV.GetUpdatedDialoguerVars();
        playerState = PlayerState.Default;
        ToggleColliders(true);
        cameraDefaultFlag = true;
        sceneScript.UpdateScene();
        MusicManager.MM.RaiseMusicInit(MusicManager.MM.sceneMusic.volume);
    }

    public void ToggleOnDialogueUI()
    {
        textbox.ToggleOn();
        cutsceneFlag = false;
        AdvanceDialogue();
    }

    public void ToggleOffDialogueUI()
    {
        textbox.ToggleOff();
        cutsceneFlag = true;
    }

    public void AdvanceDialogue()
    {
        if (textbox.choicesActive)
        {
            dlog.dialogueStep(textbox.choice);
            dlog.UpdateVars();
            //textbox.playSelectionSFX();
        }
        else
        {
            dlog.dialogueStep(0);
            dlog.UpdateVars();
        }

        if (!dlog.ended)
        {
            SetTextbox();
        }
        else
        {
            InitPlayerDefaultFromDialogue();
        }
    }

    //Call this to enable/disable player colliders
    public void ToggleColliders(bool toggle)
    {
        circCol.enabled = toggle;
        boxCol.enabled = toggle;
    }

}

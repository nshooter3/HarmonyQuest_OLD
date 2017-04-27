using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CutsceneEvent : MonoBehaviour {

    /*
    NOTE: WHEN ATTACHED TO CAMERA, ONLY USE MOVE METHOD
    */

    Animator anim;

    //Whether or not to change direction automatically upon movement, and whether or not sprite directions are diagonal
    public bool autoChangeDirection = true, animDiag = false;

    //Used for movement
    Vector3 start, destination;
    Vector3 direction;
    float speed;
    bool moving, freezeQueue;

    //Needs to be instantiated early, due to actionQueue being used in scene script start methods
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

	// Use this for initialization
	void Start () {
        direction = Vector2.zero;
        SceneScript.instance.commandInProgress = false;
        moving = false;
        speed = 1;
    }
	
	// Update is called once per frame
	void Update () {
        //Move update logic
        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * speed);
            if (Vector2.Distance(start, transform.position) >= Vector2.Distance(start, destination))
            {
                transform.position = destination;
                moving = false;
                if (freezeQueue == true)
                {
                    freezeQueue = false;
                    SceneScript.instance.commandInProgress = false;
                }
                if (anim != null)
                    anim.SetBool("IsMoving", false);
            }
        }
	}

    //Moves gameObject towards destination
    public void Move(Vector3 destination, float speed, string dir = "", bool freezeQueue = true, CutsceneEvent obj = null)
    {
        if (obj != null)
        {
            obj.Move(destination, speed, dir, freezeQueue, null);
        }
        else
        {
            start = gameObject.transform.position;
            this.destination = destination;
            this.speed = speed;
            moving = true;
            if (anim != null)
                anim.SetBool("IsMoving", true);
            direction = destination - transform.position;
            this.freezeQueue = freezeQueue;
            SceneScript.instance.commandInProgress = this.freezeQueue;
            if (dir != "")
            {
                ChangeDirection(dir);
            }
            else if (autoChangeDirection)
            {
                UpdateDirectionFromMovement();
            }
        }
    }

    void UpdateDirectionFromMovement()
    {
        if (anim != null)
        {
            //Updates direction depending on whether the sprites faces up/down/left/right or diagonally
            if (!animDiag)
            {
                if (direction.y > 0)
                {
                    anim.SetInteger("Direction", 0);
                }
                else if (direction.y < 0)
                {
                    anim.SetInteger("Direction", 2);
                }
                else if (direction.x > 0)
                {
                    anim.SetInteger("Direction", 1);
                }
                else
                {
                    anim.SetInteger("Direction", 3);
                }
            }
            else
            {
                if (direction.x < 0)
                {
                    anim.SetBool("FacingLeft", true);
                    //Debug.Log("LEFT");
                }
                else if (direction.x > 0)
                {
                    anim.SetBool("FacingLeft", false);
                    //Debug.Log("RIGHT");
                }
                if (direction.y < 0)
                {
                    anim.SetBool("FacingUp", false);
                    //Debug.Log("DOWN");
                }
                else if (direction.y > 0)
                {
                    anim.SetBool("FacingUp", true);
                    //Debug.Log("UP");
                }
            }
        }
    }

    //Changes direction gameObject is facing (Happens instantly, follow up with wait command if delay is warranted)
    public void ChangeDirection(string dir)
    {
        if (anim != null)
        {
            switch (dir)
            {
                case "up":
                    anim.SetInteger("Direction", 0);
                    break;
                case "down":
                    anim.SetInteger("Direction", 2);
                    break;
                case "left":
                    anim.SetInteger("Direction", 3);
                    break;
                case "right":
                    anim.SetInteger("Direction", 1);
                    break;
            }
        }
    }

    //Changes direction gameObject is facing (Happens instantly, follow up with wait command if delay is warranted)
    public void ChangeDirectionDiagonal(bool left, bool up)
    {
        if (anim != null)
        {
            anim.SetBool("FacingLeft", left);
            anim.SetBool("FacingUp", up);
        }
    }

    //Calls animation trigger (Call wait command if pause is desired, will not acknowledge animation play time when awaiting next command)
    //STILL NEEDS TO BE TESTED
    public void PlayAnimation(string animTrigger)
    {
        if (anim != null)
        {
            anim.SetTrigger(animTrigger);
        }
    }

    //FOR PLAYER/NPCs ONLY, MUST HAVE EMOTE OBJECT
    public void PlayAnimationEmote(string animTrigger)
    {
        EmoteAnimator emote = GetComponentInChildren<EmoteAnimator>();
        if (emote != null)
        {
            emote.SetEmotion(animTrigger);
        }
    }

    //Makes object visible
    public void EnableRenderer(GameObject obj = null)
    {
        if (obj == null)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            obj.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    //Makes object invisible
    public void DisableRenderer(GameObject obj = null)
    {
        if (obj == null)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            obj.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}

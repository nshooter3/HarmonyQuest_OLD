using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    //InputManager does not update on its own. Public update function needs to be called. This is to prevent race conditions

    //TODO Add gamepad support

    public bool confirmPress = false, backPress = false, menuPress = false, upPress = false, downPress = false, leftPress = false, rightPress = false, shiftPress = false;
    public bool confirmRelease = false, backRelease = false, menuRelease = false, upRelease = false, downRelease = false, leftRelease = false, rightRelease = false, shiftRelease = false;
    public bool confirmHeld = false, backHeld = false, menuHeld = false, upHeld = false, downHeld = false, leftHeld = false, rightHeld = false, shiftHeld = false;
    public bool shoot1Press = false, shoot1Held = false, shoot1Release = false, shoot2Press = false, shoot2Held = false, shoot2Release = false, weaponSwap = false, dash = false;

    public static InputManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Call this function to update
    public void UpdateInput()
    {
        ResetVars();
        UpdateKeyboard();
        //Make a gamepad input handler ya dingus
    }

    //Updates vars based on keyboard input
    private void UpdateKeyboard()
    {

        //Up
        if (Input.GetKey(KeyCode.UpArrow)){
            upHeld = true;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            upPress = true;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            upRelease = true;
        }

        //Down
        if (Input.GetKey(KeyCode.DownArrow))
        {
            downHeld = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            downPress = true;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            downRelease = true;
        }

        //Left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            leftHeld = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leftPress = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            leftRelease = true;
        }

        //Right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rightHeld = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightPress = true;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            rightRelease = true;
        }

        //Confirm
        if (Input.GetKey(KeyCode.A))
        {
            confirmHeld = true;
            shoot1Held = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            confirmPress = true;
            shoot1Press = true;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            confirmRelease = true;
            shoot1Release = true;
        }

        //Back
        if (Input.GetKey(KeyCode.S))
        {
            backHeld = true;
            shoot2Held = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            backPress = true;
            shoot2Press = true;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            backRelease = true;
            shoot2Release = true;
        }

        //Menu
        if (Input.GetKey(KeyCode.D))
        {
            menuHeld = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            menuPress = true;
            weaponSwap = true;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            menuRelease = true;
        }

        //Shift
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            shiftHeld = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            shiftPress = true;
            dash = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            shiftRelease = true;
        }
    }

    public Vector3 CheckDirectionalMovement()
    {
        //Check for various directional keys/combinations for movement
        if (rightHeld && upHeld)
        {
            return new Vector3(0.67f, 0.67f, 0);
        }
        else if (rightHeld && downHeld)
        {
            return new Vector3(0.67f, -0.67f, 0);
        }
        else if (leftHeld && upHeld)
        {
            return new Vector3(-0.67f, 0.67f, 0);
        }
        else if (leftHeld && downHeld)
        {
            return new Vector3(-0.67f, -0.67f, 0);
        }
        else if (rightHeld)
        {
            return new Vector3(1, 0, 0);
        }
        else if (leftHeld)
        {
            return new Vector3(-1, 0, 0);
        }
        else if (upHeld)
        {
            return new Vector3(0, 1, 0);
        }
        else if (downHeld)
        {
            return new Vector3(0, -1, 0);
        }
        else
        {
            return new Vector3(0, 0, 0);
        }
    }

    //Resets all public variables
    private void ResetVars()
    {
        confirmPress = false;
        backPress = false;
        menuPress = false;
        confirmRelease = false;
        backRelease = false;
        menuRelease = false;
        confirmHeld = false;
        backHeld = false;
        menuHeld = false;
        upPress = false;
        upRelease = false;
        upHeld = false;
        downPress = false;
        downRelease = false;
        downHeld = false;
        leftPress = false;
        leftRelease = false;
        leftHeld = false;
        rightPress = false;
        rightRelease = false;
        rightHeld = false;
        shiftPress = false;
        shiftRelease = false;
        shiftHeld = false;
        shoot1Press = false;
        shoot2Press = false;
        shoot1Release = false;
        shoot2Release = false;
        shoot1Held = false;
        shoot2Held = false;
        weaponSwap = false;
        dash = false;
    }
}

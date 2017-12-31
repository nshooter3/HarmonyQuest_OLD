using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBattle : MonoBehaviour {

    public static PlayerMovementBattle instance;

    //Player's prepared weapons
    public PlayerWeapon weapon1, weapon2, weapon3, weapon4;
    //Used to determine which weapon loadout the player has active (weapons 1 and 2, or weapons 3 and 4)
    private int loadout = 1;
    private bool swappingWeapons = false;

    //variables used to store player's base speed and current speed
    public float baseSpeed, curSpeed;

    //Vars for dashing duration and direction
    private float dashTimer = 0, maxDashTimer = 0.05f, dashCooldown = 0, maxDashCooldown = 0.1f;
    private Vector3 dashDir;

    //Player's initial scale
    private Vector3 initScale;

    //Player components
    private Rigidbody2D rb;
    private SpriteRenderer ren;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        baseSpeed = 4.5f;
        curSpeed = baseSpeed;
        initScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        ren = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        InputManager.instance.UpdateInput();
        CheckForKeyInput();
    }

    private void Move(Vector3 dir)
    {
        //Applies movement based on vector3 from CheckForMove
        rb.velocity = dir * curSpeed;
    }

    private void CheckForKeyInput()
    {
        //If player is dashing, move based on that
        if (dashTimer > 0)
        {
            Move(dashDir * 1.3f);
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                //PlayerHitbox.instance.GetComponent<BoxCollider2D>().enabled = true;
                dashCooldown = maxDashCooldown;
                ren.color = new Color(1, 1, 1, 1);
            }
        }
        //Logic for movement when not in the middle of a dash
        else
        {
            if (dashCooldown > 0)
            {
                dashCooldown -= Time.deltaTime;
            }

            Move(InputManager.instance.CheckDirectionalMovement());

            //Used to initiate dash
            if (InputManager.instance.dash && rb.velocity.magnitude > 0 && dashCooldown <= 0 && !isWeaponActive())
            {
                dashTimer = maxDashTimer;
                //PlayerHitbox.instance.GetComponent<BoxCollider2D>().enabled = false;
                dashDir = rb.velocity;
                ren.color = new Color(1, 1, 1, 0.1f);
                StartCoroutine(SizePulse());
            }
            //Used to check for weapon usage. Alters weapons based on which loadout is active
            else if (swappingWeapons == false)
            {
                if (loadout == 1)
                {
                    weapon1.CheckForInput(1);
                    if (!weapon1.weaponActive)
                    {
                        weapon2.CheckForInput(2);
                    }
                }
                else if (loadout == 2)
                {
                    weapon3.CheckForInput(1);
                    if (!weapon3.weaponActive)
                    {
                        weapon4.CheckForInput(2);
                    }
                }
                if (InputManager.instance.weaponSwap && !isWeaponActive())
                {
                    SwapWeaponLoadout();
                }
            }
        }
    }

    private void SwapWeaponLoadout()
    {
        //swappingWeapons = true;
        //TODO Swapout animations
        if (loadout == 1)
        {
            Debug.Log("loadout 2!");
            loadout = 2;
        }
        else if (loadout == 2)
        {
            Debug.Log("loadout 1!");
            loadout = 1;
        }
        //swappingWeapons = false;
    }

    //Size effect to juice up dodges
    IEnumerator SizePulse()
    {
        float rotDir = (Random.Range(0, 2) - 0.5f) * 40;
        for (float f = 1; f >= 0; f -= 0.1f)
        {
            transform.eulerAngles = Vector3.Lerp(Vector3.zero, new Vector3(0, 0, rotDir), f);
            transform.localScale = initScale * (1f + f / 2f);
            yield return new WaitForSeconds(0.005f);
        }
        transform.eulerAngles = Vector3.zero;
        yield return null;
    }

    //Use this to determine whether or not a weapon is active
    public bool isWeaponActive()
    {
        return (weapon1.weaponActive == true || weapon2.weaponActive == true ||
                     weapon3.weaponActive == true || weapon4.weaponActive == true);
    }
}

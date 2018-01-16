using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBattle : MonoBehaviour {

    public static PlayerMovementBattle instance;

    //The player's speed modifier
    private float speed = 3.5f;

    //Player's prepared weapons
    private PlayerWeapon weapon1, weapon2, weapon3, weapon4;
    //Used to determine which weapon loadout the player has active (weapons 1 and 2, or weapons 3 and 4)
    private int loadout = 1;
    private bool swappingWeapons = false;

    //Vars for dashing duration and direction
    private float dashTimer = 0, maxDashTimer = 0.04f, dashCooldown = 0, maxDashCooldown = 0.2f;
    private Vector3 dashDir;
    bool dashing = false;

    //Counter used to gradually slow the player to a stop when they attack, then gradually speed up coming out of it
    private float immobilizationCountdown, maxImmobilizationCountdown = 0.1f;
    public float speedUpCountdown, maxSpeedUpCountdown = 0.1f;

    //Player's initial scale
    private Vector3 initScale;

    //Player components
    private Rigidbody2D rb;
    private SpriteRenderer ren;

    //var to track how many bombs the player has dropped, and how many can be active at a time
    public int bombCount, maxBombCount = 3;

    public PlayerWeaponPool playerWeaponPool;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        initScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        ren = GetComponent<SpriteRenderer>();
        //Init weapons
        weapon1 = playerWeaponPool.GetWeaponFromIndex(GlobalVars.instance.saveData.weapon1);
        weapon1.transform.parent = transform;
        weapon1.transform.localPosition = Vector3.zero;
        weapon1.weaponID = 0;
        weapon2 = playerWeaponPool.GetWeaponFromIndex(GlobalVars.instance.saveData.weapon2);
        weapon2.transform.parent = transform;
        weapon2.transform.localPosition = Vector3.zero;
        weapon2.weaponID = 1;
        weapon3 = playerWeaponPool.GetWeaponFromIndex(GlobalVars.instance.saveData.weapon3);
        weapon3.transform.parent = transform;
        weapon3.transform.localPosition = Vector3.zero;
        weapon3.weaponID = 2;
        weapon4 = playerWeaponPool.GetWeaponFromIndex(GlobalVars.instance.saveData.weapon4);
        weapon4.transform.parent = transform;
        weapon4.transform.localPosition = Vector3.zero;
        weapon4.weaponID = 3;
    }
	
	// Update is called once per frame
	void Update () {
        InputManager.instance.UpdateInput();
        CheckForKeyInput();
    }

    private void Move()
    {
        Vector3 dir = InputManager.instance.CheckDirectionalMovement() * speed;
        if (isWeaponImobilizingPlayer())
        {
            //Applies movement based on vector3 from CheckForMove
            rb.velocity = dir * Mathf.Lerp(0, 1, immobilizationCountdown/ maxImmobilizationCountdown);
        }
        else if (dashing)
        {
            rb.velocity = dashDir * 5.0f;
        }
        else
        {
            //Applies movement based on vector3 from CheckForMove
            rb.velocity = dir * Mathf.Lerp(1, 0, speedUpCountdown / maxSpeedUpCountdown);
        }
    }

    public void Dash()
    {
        dashTimer = maxDashTimer;
        //PlayerHitbox.instance.GetComponent<BoxCollider2D>().enabled = false;
        dashDir = InputManager.instance.CheckDirectionalMovement() * speed;
        ren.color = new Color(1, 1, 1, 0.1f);
        StartCoroutine(SizePulse());
        dashing = true;
        speedUpCountdown = 0;
    }

    //Kick off timer that gradually slows the player to a stop
    public void StartImmobilization(float maxImmobilizationCountdown)
    {
        this.maxImmobilizationCountdown = maxImmobilizationCountdown;
        immobilizationCountdown = maxImmobilizationCountdown;
        speedUpCountdown = 0;
    }

    private void CheckForKeyInput()
    {
        Move();
        if (immobilizationCountdown > 0)
        {
            immobilizationCountdown -= Time.deltaTime;
            if (immobilizationCountdown <= 0)
            {
                immobilizationCountdown = 0;
                speedUpCountdown = maxSpeedUpCountdown;
            }
        }
        else if (speedUpCountdown > 0 && !isWeaponActive())
        {
            speedUpCountdown -= Time.deltaTime;
            if (speedUpCountdown <= 0)
            {
                //stuff
            }
        }
        //Dash logic
        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                //PlayerHitbox.instance.GetComponent<BoxCollider2D>().enabled = true;
                dashCooldown = maxDashCooldown;
                ren.color = new Color(1, 1, 1, 1);
                dashing = false;
            }
        }
        //Logic for movement when not in the middle of a dash
        else
        {
            if (dashCooldown > 0)
            {
                dashCooldown -= Time.deltaTime;
            }

            //Used to initiate dash
            if (InputManager.instance.dash)
            {
                if (!isWeaponActive() && rb.velocity.magnitude > 0 && dashCooldown <= 0)
                {
                    Dash();
                }
                else
                {
                    print("abort weapons!");
                    weapon1.AbortWeapon();
                    weapon2.AbortWeapon();
                    weapon3.AbortWeapon();
                    weapon4.AbortWeapon();
                }
            }
            //Used to check for weapon usage. Alters weapons based on which loadout is active
            if (swappingWeapons == false)
            {
                if (loadout == 1)
                {
                    if (!weapon2.weaponActive)
                    {
                        weapon1.CheckForInput(1, dashing);
                    }
                    if (!weapon1.weaponActive)
                    {
                        weapon2.CheckForInput(2, dashing);
                    }
                }
                else if (loadout == 2)
                {
                    if (!weapon4.weaponActive)
                    {
                        weapon3.CheckForInput(1, dashing);
                    }
                    if (!weapon3.weaponActive)
                    {
                        weapon4.CheckForInput(2, dashing);
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
        if (GlobalVars.instance.saveData.twoLoadouts)
        {
            //swappingWeapons = true;
            //TODO Swapout animations
            if (loadout == 1)
            {
                loadout = 2;
                WeaponIconManager.instance.SwapLoadouts(false);
            }
            else if (loadout == 2)
            {
                loadout = 1;
                WeaponIconManager.instance.SwapLoadouts(true);
            }
            //swappingWeapons = false;
        }
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

    //Use this to determine whether or not a weapon is preventing the player from moving
    public bool isWeaponImobilizingPlayer()
    {
        return (weapon1.playerImmobilized == true || weapon2.playerImmobilized == true ||
                     weapon3.playerImmobilized == true || weapon4.playerImmobilized == true);
    }
}

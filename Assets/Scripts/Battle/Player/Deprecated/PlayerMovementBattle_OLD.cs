using UnityEngine;
using System.Collections;

public class PlayerMovementBattle_OLD : MonoBehaviour
{

    /*public static PlayerMovementBattle instance;

    public float speed, actualSpeed;

    //The cost of a dash in stamina
    private float dashCost = 10f;
    //How quickly stamina comes back, and how long it takes to start regenerating
    private float staminaRegenRate = 25, regenCooldown, maxRegenCooldown = 0.5f;

    public Transform rBullet, lBullet;

    private Transform playerPos;

    private float bulletCooldownMax, bulletCooldownCur;

    Vector3 initScale;

    public SpriteRenderer ren;

    ParticleSystem dodge;
    public ParticleSystem shieldBreak, shieldHit;

    //Vars for dashing duration and direction
    float dashTimer = 0, maxDashTimer = 0.05f, dashCooldown = 0, maxDashCooldown = 0.1f;
    Vector3 dashDir;

    public Transform shootLeft, shootUpperLeft, shootLowerLeft, shootRight, shootUpperRight, shootLowerRight;


    Rigidbody2D rb;

    //Generic var for functions that need to store stuff
    float temp;

    //Used to prevent specials from activating too early after running out of stamina
    float specialRegen = 0, maxspecialRegen = 0.5f;

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
        initScale = transform.localScale;
        speed = 6f;//Stored value
        actualSpeed = speed;//Modified value that is actually used
        playerPos = gameObject.transform;
        bulletCooldownMax = 0.1f;
        bulletCooldownCur = 0;
        rb = GetComponent<Rigidbody2D>();
        ren = GetComponent<SpriteRenderer>();
        dodge = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        InputManager.instance.UpdateInput();
        CheckForKeyInput();
    }

    private void Move(Vector3 dir)
    {
        //Adjusts speed based on whether or not shield is active
        if (PlayerShield.instance.active || PlayerKillzone.instance.active)
        {
            temp = 0.5f;
        }
        else
        {
            temp = 0.75f;
        }
        //Applies movement based on vector3 from CheckForMove
        rb.velocity = dir * actualSpeed * temp;
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
                PlayerHitbox.instance.GetComponent<BoxCollider2D>().enabled = true;
                dashCooldown = maxDashCooldown;
                ren.color = new Color(1, 1, 1, 1);
            }
        }
        else
        {
            if (dashCooldown > 0)
            {
                dashCooldown -= Time.deltaTime;
                if (dashCooldown <= 0)
                {
                    dodge.Stop();
                }
            }
            CheckDirectionalMovement();

            //Used to set speed depending on whether or not the player is dashing
            if (InputManager.instance.dash && rb.velocity.magnitude > 0 && dashCooldown <= 0 && !PlayerShield.instance.active && !PlayerKillzone.instance.active)
            {
                if (BattleUIHandler.instance.stamina > 0)
                {
                    BattleUIHandler.instance.DecreaseStamina(dashCost);
                    regenCooldown = maxRegenCooldown;
                    dashTimer = maxDashTimer;
                    PlayerHitbox.instance.GetComponent<BoxCollider2D>().enabled = false;
                    dashDir = rb.velocity;
                    ren.color = new Color(1, 1, 1, 0.1f);
                    dodge.Play();
                    StartCoroutine(SizePulse());
                }
                else
                {
                    StartCoroutine(BattleUIHandler.instance.AlphaFlash(BattleUIHandler.instance.staminaFlash.GetComponent<SpriteRenderer>()));
                }
            }
            else
            {
                if (BattleUIHandler.instance.stamina > 0)
                {
                    if (specialRegen > 0)
                    {
                        specialRegen -= Time.deltaTime;
                    }
                }
                if (PlayerShield.instance.active)
                {
                    BattleUIHandler.instance.DecreaseStamina(Time.deltaTime * staminaRegenRate);
                    if (BattleUIHandler.instance.stamina <= 0)
                    {
                        //Punish player for losing all stamina with shield up
                        BattleUIHandler.instance.stamina = -50f;
                        shieldBreak.Play();
                        shieldHit.Play();
                        BattleCam.instance.GenericCamShake();
                        StartCoroutine(BattleUIHandler.instance.AlphaFlash(BattleUIHandler.instance.staminaFlash.GetComponent<SpriteRenderer>()));
                        PlayerShield.instance.ToggleActive(false);
                        specialRegen = maxspecialRegen;
                    }
                    regenCooldown = 0;
                }
                else if (PlayerKillzone.instance.active)
                {
                    BattleUIHandler.instance.DecreaseStamina(Time.deltaTime * staminaRegenRate * 2);
                    if (BattleUIHandler.instance.stamina <= 0)
                    {
                        PlayerKillzone.instance.ToggleActive(false);
                        specialRegen = maxspecialRegen;
                        StartCoroutine(BattleUIHandler.instance.AlphaFlash(BattleUIHandler.instance.staminaFlash.GetComponent<SpriteRenderer>()));
                    }
                    regenCooldown = 0;
                }
                else if (regenCooldown > 0)
                {
                    regenCooldown -= Time.deltaTime;
                }
                else
                {
                    BattleUIHandler.instance.IncreaseStamina(Time.deltaTime * staminaRegenRate);
                }
            }
        }
        //Used to determine firing patterns
        if (InputManager.instance.shield)
        {
            bulletCooldownCur = 0;
            if (!PlayerShield.instance.active && BattleUIHandler.instance.stamina > 0 && specialRegen <= 0)
            {
                PlayerShield.instance.ToggleActive(true);
            }
        }
        else
        {
            if (PlayerShield.instance.active)
            {
                PlayerShield.instance.ToggleActive(false);
            }
            if (InputManager.instance.killzone)
            {
                bulletCooldownCur = 0;
                if (!PlayerKillzone.instance.active && BattleUIHandler.instance.stamina > 0 && specialRegen <= 0)
                {
                    PlayerKillzone.instance.ToggleActive(true);
                }
            }
            else
            {
                if (PlayerKillzone.instance.active)
                {
                    PlayerKillzone.instance.ToggleActive(false);
                }

                if (InputManager.instance.bomb)
                {
                    if (!InputManager.instance.waitForBombRelease)
                    {
                        bulletCooldownCur = 0;
                        if (Bomb.instance.idle)
                        {
                            if (BattleUIHandler.instance.stamina > 0)
                            {
                                Bomb.instance.Plant(transform.position);
                                InputManager.instance.waitForBombRelease = true;
                            }
                            else
                            {
                                StartCoroutine(BattleUIHandler.instance.AlphaFlash(BattleUIHandler.instance.staminaFlash.GetComponent<SpriteRenderer>()));
                            }
                        }
                        else if (Bomb.instance.planted)
                        {
                            Bomb.instance.Boom();
                            InputManager.instance.waitForBombRelease = true;
                        }
                    }
                }
                else if (bulletCooldownCur <= 0)
                {
                    if (InputManager.instance.shoot1)
                    {
                        BulletPool.instance.SpawnNormalBullet(shootLeft.position);
                        BulletPool.instance.SpawnNormalBullet(shootRight.position);
                        bulletCooldownCur = bulletCooldownMax;
                    }
                    else if (InputManager.instance.shoot2)
                    {
                        BulletPool.instance.SpawnNormalBullet(transform.position, new Vector3(0, -1, 0));
                        BulletPool.instance.SpawnNormalBullet(shootUpperLeft.position, new Vector3(-1, 1, 0));
                        BulletPool.instance.SpawnNormalBullet(shootUpperRight.position, new Vector3(1, 1, 0));
                        bulletCooldownCur = bulletCooldownMax * 2f;
                    }
                    else if (InputManager.instance.weaponSwap)
                    {
                        BulletPool.instance.SpawnNormalBullet(shootLowerLeft.position, new Vector3(-1, -1, 0));
                        BulletPool.instance.SpawnNormalBullet(shootLowerRight.position, new Vector3(1, -1, 0));
                        bulletCooldownCur = bulletCooldownMax;
                    }
                }
                else
                {
                    bulletCooldownCur -= Time.deltaTime;
                }
            }
        }
    }

    private void CheckDirectionalMovement()
    {
        //Check for various directional keys/combinations for movement
        if (InputManager.instance.rightHeld && InputManager.instance.upHeld)
        {
            Move(new Vector3(0.67f, 0.67f, 0));
        }
        else if (InputManager.instance.rightHeld && InputManager.instance.downHeld)
        {
            Move(new Vector3(0.67f, -0.67f, 0));
        }
        else if (InputManager.instance.leftHeld && InputManager.instance.upHeld)
        {
            Move(new Vector3(-0.67f, 0.67f, 0));
        }
        else if (InputManager.instance.leftHeld && InputManager.instance.downHeld)
        {
            Move(new Vector3(-0.67f, -0.67f, 0));
        }
        else if (InputManager.instance.rightHeld)
        {
            Move(new Vector3(1, 0, 0));
        }
        else if (InputManager.instance.leftHeld)
        {
            Move(new Vector3(-1, 0, 0));
        }
        else if (InputManager.instance.upHeld)
        {
            Move(new Vector3(0, 1, 0));
        }
        else if (InputManager.instance.downHeld)
        {
            Move(new Vector3(0, -1, 0));
        }
        else
        {
            Move(new Vector3(0, 0, 0));
        }
    }

    public void setBulletCooldown(float val)
    {
        bulletCooldownMax = val;
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
    }*/
}

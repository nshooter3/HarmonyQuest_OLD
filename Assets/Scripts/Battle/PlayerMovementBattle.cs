using UnityEngine;
using System.Collections;

public class PlayerMovementBattle : MonoBehaviour {

    public static PlayerMovementBattle instance;

    public float speed, actualSpeed;

    //The cost of a dash in stamina
    private float dashCost = 10f;
    //How quickly stamina comes back, and how long it takes to start regenerating
    private float staminaRegenRate = 25, regenCooldown, maxRegenCooldown = 0.5f;

    public Transform rBullet, lBullet;

    private Transform playerPos;

    private float bulletCooldownMax, bulletCooldownCur;

    Vector3 initScale;

    SpriteRenderer ren;

    ParticleSystem dodge;

    //Vars for dashing duration and direction
    float dashTimer = 0, maxDashTimer = 0.05f, dashCooldown = 0, maxDashCooldown = 0.1f;
    Vector3 dashDir;

    public Transform shootLeft, shootUpperLeft, shootLowerLeft, shootRight, shootUpperRight, shootLowerRight;
    

    Rigidbody2D rb;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    // Use this for initialization
    void Start () {
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
	void Update () {
        InputManager.instance.UpdateInput();
        CheckForKeyInput();
	}

    private void Move(Vector3 dir)
    {
        //Applies movement based on vector3 from CheckForMove
        rb.velocity = dir * actualSpeed*0.75f;
    }

    private void CheckForKeyInput()
    {
        if (dashTimer > 0)
        {
            Move(dashDir * 1.3f);
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
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
            if (InputManager.instance.shiftPress && rb.velocity.magnitude > 0 && dashCooldown <= 0)
            {
                if (BattleUIHandler.instance.stamina > 0)
                {
                    BattleUIHandler.instance.DecreaseStamina(dashCost);
                    regenCooldown = maxRegenCooldown;
                    dashTimer = maxDashTimer;
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
                if (regenCooldown > 0)
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
        if (bulletCooldownCur <= 0)
        {
            if (InputManager.instance.confirmHeld)
            {
                BulletPool.instance.SpawnNormalBullet(shootLeft.position);
                BulletPool.instance.SpawnNormalBullet(shootRight.position);
                bulletCooldownCur = bulletCooldownMax;
            }
            else if (InputManager.instance.backHeld)
            {
                BulletPool.instance.SpawnNormalBullet(transform.position, new Vector3(0, -1, 0));
                BulletPool.instance.SpawnNormalBullet(shootUpperLeft.position, new Vector3(-1,1,0));
                BulletPool.instance.SpawnNormalBullet(shootUpperRight.position, new Vector3(1, 1, 0));
                bulletCooldownCur = bulletCooldownMax*2f;
            }
            else if (InputManager.instance.menuHeld)
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
        float rotDir = (Random.Range(0, 2) - 0.5f)*40;
        for (float f = 1; f >= 0; f -= 0.1f)
        {
            transform.eulerAngles = Vector3.Lerp(Vector3.zero, new Vector3(0, 0, rotDir), f);
            transform.localScale = initScale * (1f + f/2f);
            yield return new WaitForSeconds(0.005f);
        }
        transform.eulerAngles = Vector3.zero;
        yield return null;
    }
}

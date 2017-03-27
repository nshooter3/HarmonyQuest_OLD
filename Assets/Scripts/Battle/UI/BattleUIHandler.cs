using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIHandler : MonoBehaviour {

    public static BattleUIHandler instance;

    //References to the UI in need of manipulation
    public GameObject healthMain, staminaMain, enemyMain, healthDrop, staminaDrop, enemyDrop,
        healthAll, staminaAll, enemyAll, staminaFlash;
    //Values for attributes
    public float health, healthMax, stamina, staminaMax, enemy, enemyMax;
    //Max length of the actual bars in unity units
    private float healthLength, staminaLength, enemyLength;
    //Whether or not yellow drop bar is shrinking
    bool healthDropping, staminaDropping, enemyDropping;
    //The delay before the bar starts dropping
    float healthDropDelay, staminaDropDelay, enemyDropDelay, maxDelay = 0.5f;
    Vector3 initScaleHealth, initScaleStamina, initScaleEnemy;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

	// Use this for initialization
	void Start () {

        //TEMP SCRIPT FOR HEALTH/STAMINA/ENEMY. Need to hook this up to datamanager and enemy info later
        healthMax = 100;
        staminaMax = 100;
        enemyMax = 1000;
        //TEMP

        health = healthMax;
        stamina = staminaMax;
        enemy = enemyMax;

        healthLength = healthMain.transform.localScale.x;
        staminaLength = staminaMain.transform.localScale.x;
        enemyLength = enemyMain.transform.localScale.x;

        initScaleHealth = healthAll.transform.localScale;
        initScaleStamina = staminaAll.transform.localScale;
        initScaleEnemy = enemyAll.transform.localScale;
    }

    public void DecreaseHealth(float damage)
    {
        health = Mathf.Max(0, health - damage);
        healthMain.transform.localScale = new Vector3(healthLength * (health / healthMax), healthMain.transform.localScale.y, healthMain.transform.localScale.z);
        if (healthDropping != true)
        {
            healthDropping = true;
            healthDropDelay = maxDelay;
        }
    }

    public void IncreaseHealth(float healing)
    {
        health = Mathf.Min(healthMax, health + healing);
        healthMain.transform.localScale = new Vector3(healthLength * (health / healthMax), healthMain.transform.localScale.y, healthMain.transform.localScale.z);
        if(healthDrop.transform.localScale.x < healthMain.transform.localScale.x)
            healthDrop.transform.localScale = new Vector3(healthLength * (health / healthMax), healthMain.transform.localScale.y, healthMain.transform.localScale.z);
    }

    public void DecreaseStamina(float damage)
    {
        stamina = stamina - damage;
        staminaMain.transform.localScale = new Vector3(staminaLength * (Mathf.Max(0, stamina) / staminaMax), staminaMain.transform.localScale.y, staminaMain.transform.localScale.z);
        if (staminaDropping != true)
        {
            staminaDropping = true;
            staminaDropDelay = maxDelay;
        }
    }

    public void IncreaseStamina(float healing)
    {
        stamina = Mathf.Min(staminaMax, stamina + healing);
        staminaMain.transform.localScale = new Vector3(staminaLength * (Mathf.Max(0, stamina) / staminaMax), staminaMain.transform.localScale.y, staminaMain.transform.localScale.z);
        if (staminaDrop.transform.localScale.x < staminaMain.transform.localScale.x)
            staminaDrop.transform.localScale = new Vector3(staminaLength * (Mathf.Max(0, stamina) / staminaMax), staminaMain.transform.localScale.y, staminaMain.transform.localScale.z);
    }

    public void DecreaseEnemy(float damage)
    {
        enemy = Mathf.Max(0, enemy - damage);
        enemyMain.transform.localScale = new Vector3(enemyLength * (enemy / enemyMax), enemyMain.transform.localScale.y, enemyMain.transform.localScale.z);
        if (enemyDropping != true)
        {
            enemyDropping = true;
            enemyDropDelay = maxDelay;
        }
        BarPump(enemyAll, initScaleEnemy, 1.1f, 0.1f);
    }

    public void IncreaseEnemy(float healing)
    {
        enemy = Mathf.Min(enemyMax, enemy + healing);
        enemyMain.transform.localScale = new Vector3(enemyLength * (enemy / enemyMax), enemyMain.transform.localScale.y, enemyMain.transform.localScale.z);
        if (enemyDrop.transform.localScale.x < enemyMain.transform.localScale.x)
            enemyDrop.transform.localScale = new Vector3(enemyLength * (enemy / enemyMax), enemyMain.transform.localScale.y, enemyMain.transform.localScale.z);
    }

    //Starts coroutine that makes the gameObject's scale pump
    public void BarPump(GameObject bar, Vector3 initScale, float scaleUpRatio, float dur)
    {
        StartCoroutine(Pump(bar, initScale, scaleUpRatio, dur));
    }

    //coroutine that makes the gameObject's scale pump. Call BarPump to use
    private IEnumerator Pump(GameObject obj, Vector3 initScale, float scaleUpRatio, float dur)
    {
        obj.transform.localScale = initScale;
        float maxDur = dur;
        Vector3 originalScale = obj.transform.localScale;
        for (float f = dur; dur > 0; dur -= Time.deltaTime)
        {
            obj.transform.localScale = originalScale * (1f + (scaleUpRatio - 1f) * (dur/maxDur));
            yield return new WaitForSeconds(Time.deltaTime);
        }
        obj.transform.localScale = originalScale;
        yield return null;
    }

    // Update is called once per frame
    void Update () {

        if (healthDropping)
        {
            if (healthDropDelay > 0)
            {
                healthDropDelay -= Time.deltaTime;
            }
            else if (healthDrop.transform.localScale.x > healthMain.transform.localScale.x)
            {
                healthDrop.transform.localScale = new Vector3(healthDrop.transform.localScale.x - Time.deltaTime, healthMain.transform.localScale.y, healthMain.transform.localScale.z);
            }
            else
            {
                healthDrop.transform.localScale = healthMain.transform.localScale;
                healthDropping = false;
            }
        }
        if (staminaDropping)
        {
            if (staminaDropDelay > 0)
            {
                staminaDropDelay -= Time.deltaTime;
            }
            else if (staminaDrop.transform.localScale.x > staminaMain.transform.localScale.x)
            {
                staminaDrop.transform.localScale = new Vector3(staminaDrop.transform.localScale.x - Time.deltaTime, staminaMain.transform.localScale.y, staminaMain.transform.localScale.z);
            }
            else
            {
                staminaDrop.transform.localScale = staminaMain.transform.localScale;
                staminaDropping = false;
            }
        }
        if (enemyDropping)
        {
            if (enemyDropDelay > 0)
            {
                enemyDropDelay -= Time.deltaTime;
            }
            else if (enemyDrop.transform.localScale.x > enemyMain.transform.localScale.x)
            {
                enemyDrop.transform.localScale = new Vector3(enemyDrop.transform.localScale.x - Time.deltaTime, enemyMain.transform.localScale.y, enemyMain.transform.localScale.z);
            }
            else
            {
                enemyDrop.transform.localScale = enemyMain.transform.localScale;
                enemyDropping = false;
            }
        }
    }

    //Size effect to juice up dodges
    public IEnumerator AlphaFlash(SpriteRenderer sprite)
    {
        for (float f = .5f; f >= 0; f -= 0.05f)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, f);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
        yield return null;
    }

}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUIHandler : MonoBehaviour {

    public static BattleUIHandler instance;

    //References to the UI in need of manipulation
    public GameObject healthMain, staminaMain, enemyMain, healthDrop, staminaDrop, enemyDrop,
        healthAll, staminaAll, enemyAll;
    //Values for attributes
    public float health, healthMax, stamina, staminaMax, enemy, enemyMax;
    //Max length of the actual bars in unity units
    private float healthLength, staminaLength, enemyLength;
    //Whether or not yellow drop bar is shrinking
    bool healthDropping, staminaDropping, enemyDropping;
    //The delay before the bar starts dropping
    float healthDropDelay, staminaDropDelay, enemyDropDelay, maxDelay = 0.5f;

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
        enemyMax = 100;
        //TEMP

        health = healthMax;
        stamina = staminaMax;
        enemy = enemyMax;

        healthLength = healthMain.transform.localScale.x;
        staminaLength = staminaMain.transform.localScale.x;
        enemyLength = enemyMain.transform.localScale.x;
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
        stamina = Mathf.Max(0, stamina - damage);
        staminaMain.transform.localScale = new Vector3(staminaLength * (stamina / staminaMax), staminaMain.transform.localScale.y, staminaMain.transform.localScale.z);
        if (staminaDropping != true)
        {
            staminaDropping = true;
            staminaDropDelay = maxDelay;
        }
    }

    public void IncreaseStamina(float healing)
    {
        stamina = Mathf.Min(staminaMax, stamina + healing);
        staminaMain.transform.localScale = new Vector3(staminaLength * (stamina / staminaMax), staminaMain.transform.localScale.y, staminaMain.transform.localScale.z);
        if (staminaDrop.transform.localScale.x < staminaMain.transform.localScale.x)
            staminaDrop.transform.localScale = new Vector3(staminaLength * (stamina / staminaMax), staminaMain.transform.localScale.y, staminaMain.transform.localScale.z);
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
    }

    public void IncreaseEnemy(float healing)
    {
        enemy = Mathf.Min(enemyMax, enemy + healing);
        enemyMain.transform.localScale = new Vector3(enemyLength * (enemy / enemyMax), enemyMain.transform.localScale.y, enemyMain.transform.localScale.z);
        if (enemyDrop.transform.localScale.x < enemyMain.transform.localScale.x)
            enemyDrop.transform.localScale = new Vector3(enemyLength * (enemy / enemyMax), enemyMain.transform.localScale.y, enemyMain.transform.localScale.z);
    }

    // Update is called once per frame
    void Update () {

        /*if (Input.GetKeyDown(KeyCode.Z)){
            DecreaseEnemy(10);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            IncreaseEnemy(10);
        }*/

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
}

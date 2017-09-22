using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    //Enemy max health and current health
    public float maxhealth, health;
    //Material associated with enemy's sprite renderer
    public Material mat;
    //Used to store reticules targeting this enemy
    //Likely going to be deprecated
    public List<Reticule> reticules;
    //Used to calculate the tick duration based on the bpm of the music
    public float bpm = 120f;

    //Tracks which tick the enemy is on, used to time attacks/movement. Will likely be reset upon entering a new state.
    public int tick = 0;

    //Misc components
    protected  Rigidbody2D rb;
    protected  SpriteRenderer sr;
    protected  Collider2D col;
    //Init col is default color, hitCol is the color the sprite renderer will be shaded to upon taking damage
    protected  Color initCol, hitCol;
    //Gives the enemy some iframes before taking another hit
    //Likely will be deprecated after limiting the player's offensive capability
    protected float hitTimer = 0, maxHitTimer = 0.1f;

    //Basic enemy state machine. Can be expanded upon in subclasses if necessary
    protected enum EnemyState { Idle, Move, Attack };
    protected EnemyState enemyState;

    //Delegate that is called upon an update tick
    protected delegate void UpdateTick();
    protected UpdateTick MyUpdateTick;
    //The interval in seconds that a tick occurs
    protected float tickDuration;

    void Awake()
    {
        tickDuration = 60f / bpm;
    }

    // Use this for initialization
    void Start () {
        //dir = new Vector3(1, -1, 0).normalized;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        sr.material = mat;
        initCol = sr.color;
        hitCol = Color.red;
        health = maxhealth;
        reticules = new List<Reticule>();
        enemyState = EnemyState.Idle;
    }

    //Checks for bullets that damage the enemy. Might be deprecated depending on where I go with the combat
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Bullet") && col.gameObject.GetComponent<Bullet>().friendly)
        {
            Bullet temp = col.gameObject.GetComponent<Bullet>();
            temp.Reset();
            TakeDamage(temp.damage);
        }
    }

    //Takes damages, handles events for reaching 0 health
    public void TakeDamage(float damage)
    {
        health -= damage;
        StartCoroutine(GetHit());
        if (health <= 0)
        {
            BattleUIHandler.instance.DecreaseEnemy(maxhealth);
            Destroy();
        }
    }

    //Juice effects associated with taking damage
    IEnumerator GetHit()
    {
        //sr.gameObject.transform.localPosition = Vector3.zero;
        Vector3 rot = new Vector3(0, 0, (Random.Range(0, 1) * 2 - 1)*2);
        //Vector3 tempDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized * Random.Range(0.04f, 0.06f);
        hitTimer = maxHitTimer;
        for (var f = maxHitTimer; f >= 0; f -= Time.deltaTime)
        {
            sr.color = Color.Lerp(initCol, hitCol, f / maxHitTimer);
            //sr.gameObject.transform.localPosition = Vector3.Lerp(Vector3.zero, tempDir, f / maxHitTimer);
            sr.gameObject.transform.eulerAngles = Vector3.Lerp(Vector3.zero, rot, f / maxHitTimer);
            sr.gameObject.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one*1.1f, f / maxHitTimer);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        sr.gameObject.transform.eulerAngles = Vector3.zero;
        sr.gameObject.transform.localScale = Vector3.one;
        sr.color = initCol;
        yield return null;
    }

    //Handle logic for enemy health reaching 0
    public void Destroy()
    {
        StopTick();
        /*
        //TODO make this NOT reset the enemy to their startPos
        foreach (Reticule ret in reticules)
        {
            ret.transform.parent = null;
        }
        reticules = new List<Reticule>();
        transform.position = initPos;
        health = maxhealth;
        Destroy(this.gameObject);
        */
    }

    //Calls the MyUpdateTick delegate once per beat based on the bpm;
    IEnumerator TickCounter()
    {
        while (true)
        {
            MyUpdateTick();
            yield return new WaitForSeconds(tickDuration);
        }
    }

    //Starts the TickCounter coroutine
    public void StartTick()
    {
        StartCoroutine(TickCounter());
    }

    //Stops the TickCounter coroutine from firing
    public void StopTick()
    {
        StopCoroutine(TickCounter());
    }

    //Virtual functions to be implemented in children enemy classes. Defines non-tick related updates during states.
    protected virtual void IdleUpdate(){}

    protected virtual void MoveUpdate(){}

    protected virtual void AttackUpdate(){}

}

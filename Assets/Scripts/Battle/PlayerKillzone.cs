using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillzone : MonoBehaviour {

    //Whether or not the killzone is active
    public bool active = false;
    SpriteRenderer sr;
    CircleCollider2D cirCol;

    //Cooldown for zap attack
    float cooldown, maxCooldown = 0.1f;
    float damage = 35f;

    public static PlayerKillzone instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
        cirCol = GetComponent<CircleCollider2D>();
        sr.enabled = false;
        cirCol.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (active)
        {
            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
            }
        }
	}

    //Used to enable/disable shield
    public void ToggleActive(bool active)
    {
        sr.enabled = active;
        cirCol.enabled = active;
        this.active = active;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (active && cooldown <= 0)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                //TODO attack enemy
                col.gameObject.GetComponent<Enemy>().TakeDamage(damage);
                cooldown = maxCooldown;
            }
        }
    }
}

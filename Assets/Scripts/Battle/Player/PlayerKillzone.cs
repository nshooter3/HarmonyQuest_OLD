using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillzone : MonoBehaviour {

    //Whether or not the killzone is active
    public bool active = false;
    SpriteRenderer sr;
    Vector4 col;
    float initAlpha;
    CircleCollider2D cirCol;

    List<Enemy> curCols = new List<Enemy>();

    //Cooldown for zap attack
    float cooldown, maxCooldown = 0.05f;
    float damage = 30f;

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
        col = sr.color;
        initAlpha = col.w;
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
            else
            {
                Debug.Log(curCols.Count);
                if (curCols.Count > 0)
                {
                    curCols[Random.Range(0, curCols.Count)].TakeDamage(damage);
                }
                cooldown = maxCooldown;
            }
            col.w = Mathf.Lerp(initAlpha/12f, initAlpha, (BattleUIHandler.instance.stamina/ BattleUIHandler.instance.staminaMax));
            sr.color = col;
        }
	}

    //Used to enable/disable shield
    public void ToggleActive(bool active)
    {
        sr.enabled = active;
        cirCol.enabled = active;
        this.active = active;
        if (!active)
        {
            curCols.Clear();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
            if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                curCols.Add(col.gameObject.GetComponent<Enemy>());
            }
    }

    void OnTriggerExit2D(Collider2D col)
    {
            if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                curCols.Remove(col.gameObject.GetComponent<Enemy>());
            }
    }
}

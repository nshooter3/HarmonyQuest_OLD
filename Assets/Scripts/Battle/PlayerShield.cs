using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour {

    SpriteRenderer sr;
    CircleCollider2D cirCol;

    //whether or not shield is active
    public bool active;
    //determines the scale of the shield as stamina decreases
    float maxSize = 2.5f, minSize = 1f, curSize;
    //Used for lerping shield size based on stamina
    float t;

    public static PlayerShield instance;

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
            AdjustShieldSize();
        }
	}

    //Update shield size based on stamina
    void AdjustShieldSize()
    {
        t = BattleUIHandler.instance.stamina / BattleUIHandler.instance.staminaMax;
        curSize = Mathf.Lerp(minSize, maxSize, t);
        transform.localScale = new Vector3(curSize, curSize, 1);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (PlayerHitbox.instance.curCooldown <= 0)
        {
            //Check for both bullet and enemy collisions
            if (col.gameObject.layer == LayerMask.NameToLayer("Bullet") && !col.gameObject.GetComponent<Bullet>().friendly)
            {
                Bullet temp = col.gameObject.GetComponent<Bullet>();
                PlayerHitbox.instance.TakeDamage(temp.damage, true);
                temp.Reset();
            }
            else if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                PlayerHitbox.instance.TakeDamage(col.gameObject.GetComponent<Enemy>().damage, true);
            }
        }
    }


    //Used to enable/disable shield
    public void ToggleActive(bool active)
    {
        if (active)
        {
            AdjustShieldSize();
        }
        sr.enabled = active;
        cirCol.enabled = active;
        this.active = active;
        if (PlayerHitbox.instance != null)
        {
            PlayerHitbox.instance.ToggleBoxCol(!active);
        }
    }
}

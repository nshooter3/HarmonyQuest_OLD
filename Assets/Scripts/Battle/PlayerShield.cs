using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour {

    SpriteRenderer sr;
    CircleCollider2D boxCol;
    //if greater than 0, is currently being collided with
    public int colCount;
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
        boxCol = GetComponent<CircleCollider2D>();
        sr.enabled = false;
        boxCol.enabled = false;
        colCount = 0;
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.gameObject.layer == LayerMask.NameToLayer("Bullet") && !col.gameObject.GetComponent<Bullet>().friendly) || col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            colCount++;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if ((col.gameObject.layer == LayerMask.NameToLayer("Bullet") && !col.gameObject.GetComponent<Bullet>().friendly) || col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            colCount--;
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
        boxCol.enabled = active;
        this.active = active;
    }
}

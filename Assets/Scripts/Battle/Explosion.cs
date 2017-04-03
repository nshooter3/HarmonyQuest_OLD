using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    List<Enemy> curCols;
    SpriteRenderer sr;
    Collider2D coll;
    Vector4 color, initColor;
    bool active = false;

    float damage = 100;

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        coll.enabled = false;
        initColor = sr.color;
        color = initColor;
        color.w = 0;
        sr.color = color;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartExplosion()
    {
        StartCoroutine(Explode());
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        curCols = new List<Enemy>();
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy") && !curCols.Contains(col.gameObject.GetComponent<Enemy>()))
        {
            col.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            curCols.Add(col.gameObject.GetComponent<Enemy>());
        }
    }

    IEnumerator Explode()
    {
        active = true;
        coll.enabled = true;
        float maxTime = 0.5f;
        for (float f = maxTime; f > 0; f -= Time.deltaTime)
        {
            sr.color = Color.Lerp(color, initColor, f / maxTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        sr.color = color;
        coll.enabled = false;
        active = false;
        Bomb.instance.boom = false;
        Bomb.instance.idle = true;
    }
}

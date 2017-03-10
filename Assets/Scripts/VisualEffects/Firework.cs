using UnityEngine;
using System.Collections;

public class Firework : MonoBehaviour {

    Rigidbody2D rb;

    public ParticleSystem trail, boom;

    float boomTimer;
    float force;
    Vector2 dir;

    bool hasBoom = false;

	// Use this for initialization
	void Start () {
        trail.GetComponent<CharacterChatter>().PlayChatter();
        rb = GetComponent<Rigidbody2D>();
        boom.gameObject.SetActive(false);
        boomTimer = Random.Range(1f, 1.5f);
        dir = new Vector2(Random.Range(-0.35f, 0.35f), 1);
        force = Random.Range(10f, 14f);
        rb.velocity = dir * force;
        boom.startColor = new Color(Random.value, Random.value, Random.value, 1.0f);
    }

    void Boom()
    {
        rb.isKinematic = true;
        boom.gameObject.SetActive(true);
        trail.Stop();
        Destroy(gameObject, 1.5f);
        hasBoom = true;
        boom.GetComponent<CharacterChatter>().PlayChatter();
    }
	
	// Update is called once per frame
	void Update () {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        if (!hasBoom && boomTimer > 0)
        {
            boomTimer -= Time.deltaTime;
        }
        else
        {
            if(!hasBoom)
                Boom();
        }
    }
}

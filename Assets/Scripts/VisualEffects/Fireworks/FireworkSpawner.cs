using UnityEngine;
using System.Collections;

public class FireworkSpawner : MonoBehaviour {

    public GameObject firework;
    public Camera cam;

    bool followCamera = true;
    float cooldown;
    float maxCool = 4f, minCool = 0.5f;

	// Use this for initialization
	void Start () {
        cam = GameObject.FindObjectOfType<Camera>();
        cooldown = Random.Range(maxCool, minCool);
    }
	
	// Update is called once per frame
	void Update () {
        if (cooldown <= 0)
        {
            GameObject.Instantiate(firework, new Vector3 (Random.Range(transform.position.x - 4f, transform.position.x + 4f), transform.position.y - 5,0), Quaternion.identity);
            cooldown = Random.Range(maxCool, minCool);
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
        if (followCamera)
        {
            transform.position = cam.transform.position;
        }
	}
}

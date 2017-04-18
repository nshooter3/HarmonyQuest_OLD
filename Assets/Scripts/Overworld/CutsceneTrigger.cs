using UnityEngine;
using System.Collections;

public class CutsceneTrigger : MonoBehaviour {

    private BoxCollider2D col;
    public bool isCol;

	// Use this for initialization
	void Start () {
        isCol = false;
        col = GetComponent<BoxCollider2D>();
        if (gameObject.GetComponent<SpriteRenderer>() != null)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        {
            //Debug.Log("Hit");
            isCol = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            isCol = false;
        }
    }
}

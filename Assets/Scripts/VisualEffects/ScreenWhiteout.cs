using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWhiteout : MonoBehaviour {

    public GameObject slashedObject;
    SpriteRenderer sr;

    public static ScreenWhiteout instance;

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
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InitSlashSequence();
        }
    }

    //Start post battle slash sequence
    public void InitSlashSequence()
    {
        sr.enabled = true;
        sr.material.SetColor("_MultColor", Color.black);
        SlashedObjectEffect.instance.InitSlashObject(slashedObject.GetComponent<SpriteRenderer>(), Color.white, 1);
        StartCoroutine(DelayedColorChange(Color.white, Color.black, 0.05f));
    }

    //Used to change colors after a brief delay
    IEnumerator DelayedColorChange(Color srCol, Color slashedCol, float delay)
    {
        yield return new WaitForSeconds(delay);
        sr.material.SetColor("_MultColor", srCol);
        SlashedObjectEffect.instance.ChangeSegmentColor(slashedCol);
    }
}

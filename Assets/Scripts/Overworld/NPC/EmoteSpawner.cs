using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteSpawner : MonoBehaviour {

    public EmoteAnimator emote;

	// Use this for initialization
	void Start () {
        emote = Instantiate(emote, transform.parent);
        emote.transform.position = transform.position;
        emote.transform.eulerAngles = transform.eulerAngles;
        emote.transform.localScale = transform.localScale;
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

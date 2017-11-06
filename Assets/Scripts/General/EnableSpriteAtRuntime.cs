using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSpriteAtRuntime : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

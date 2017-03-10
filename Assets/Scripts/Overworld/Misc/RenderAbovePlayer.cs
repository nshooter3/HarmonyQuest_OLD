using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderAbovePlayer : MonoBehaviour {

    SpriteRenderer ren;

	// Use this for initialization
	void Start () {
        ren = GetComponent<SpriteRenderer>();
        if (ren != null)
        {
            ren.sortingLayerName = "AbovePlayer";
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

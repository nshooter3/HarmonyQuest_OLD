using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

    public float xSpeed = 0, ySpeed = 0;
    Material mat;

	// Use this for initialization
	void Start () {
        mat = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
        mat.mainTextureOffset = new Vector2(mat.mainTextureOffset.x + xSpeed*Time.deltaTime, mat.mainTextureOffset.y + ySpeed * Time.deltaTime);
    }
}

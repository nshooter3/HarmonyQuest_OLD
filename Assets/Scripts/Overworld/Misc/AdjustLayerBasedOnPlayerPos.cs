using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustLayerBasedOnPlayerPos : MonoBehaviour {

    //Adjusts layer based on player position, allows player to go in front of/behind sprites

    string initLayer;
    public string adjustedLayer = "AbovePlayer";
    SpriteRenderer sr;
    PlayerMovementOverworld player;

    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        initLayer = sr.sortingLayerName;
        player = FindObjectOfType<PlayerMovementOverworld>();
    }
	
	// Update is called once per frame
	void Update () {
        if (player != null)
        {
            if (player.transform.position.y <= transform.position.y)
            {
                if (sr.sortingLayerName != initLayer)
                {
                    sr.sortingLayerName = initLayer;
                }
            }
            else if (player.transform.position.y > transform.position.y) {
                if (sr.sortingLayerName !=adjustedLayer)
                {
                    sr.sortingLayerName = adjustedLayer;
                }
            }
        }
	}
}

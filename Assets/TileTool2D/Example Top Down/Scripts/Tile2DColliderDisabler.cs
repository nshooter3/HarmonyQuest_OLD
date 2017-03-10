﻿using UnityEngine;
using System.Collections;

public class Tile2DColliderDisabler : MonoBehaviour {

	[Tooltip("Disables default box collider on Awake if a tile uses a specific tile.\nUseful for example on top down games where C tiles are ground without colliders.")]
	public string[] disableColliderOnTileTypes;

	void Awake () {
		DisableCollider();
	}
	
	void DisableCollider() {
		for (int i = 0; i < disableColliderOnTileTypes.Length; i++) {
			if (GetComponent<Tile2D>().tileSprite == disableColliderOnTileTypes[i])
				GetComponent<Collider2D>().enabled = false;
		}
	}
}

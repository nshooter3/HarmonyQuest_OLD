using UnityEngine;
using System.Collections;

public class RestrictMovement : MonoBehaviour {

    public BoxCollider2D bound;
    public Transform playerPos;

    private float xBound, yBound, playerRadius;

	// Use this for initialization
	void Start () {
        xBound = bound.size.x/2f;
        yBound = bound.size.y/2f;
        playerRadius = this.transform.localScale.x/2f;
        playerPos = gameObject.transform;
    }
	
	// Update is called once per frame
	void Update () {
        BoundPlayer();
    }

    private void BoundPlayer()
    {
        if (playerPos.position.x > xBound)
            playerPos.position = new Vector3(xBound, playerPos.position.y, playerPos.position.z);
        else if (playerPos.position.x < -xBound)
            playerPos.position = new Vector3(-xBound, playerPos.position.y, playerPos.position.z);

        if (playerPos.position.y > yBound)
            playerPos.position = new Vector3(playerPos.position.x, yBound, playerPos.position.z);
        else if (playerPos.position.y < -yBound)
            playerPos.position = new Vector3(playerPos.position.x, -yBound, playerPos.position.z);
    }
}

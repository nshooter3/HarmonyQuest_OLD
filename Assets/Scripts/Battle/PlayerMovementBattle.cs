using UnityEngine;
using System.Collections;

public class PlayerMovementBattle : MonoBehaviour {

    public BoxCollider2D bound;

    public float speed, actualSpeed;

    public Transform rBullet, lBullet;

    public GameObject[] bullets;

    private Transform playerPos;

    private float xBound, yBound, playerRadius, bulletCooldownMax, bulletCooldownCur;

    // Use this for initialization
    void Start () {
        speed = 6f;//Stored value
        actualSpeed = speed;//Modified value that is actually used
        xBound = bound.size.x / 2f;
        yBound = bound.size.y / 2f;
        playerRadius = this.transform.localScale.x / 13f;
        playerPos = gameObject.transform;
        bulletCooldownMax = 0.05f;
        bulletCooldownCur = 0;
    }
	
	// Update is called once per frame
	void Update () {
        CheckForKeyInput();
	}

    private void Move(Vector3 dir)
    {
        //Applies movement based on vector3 from CheckForMove
        playerPos.position = playerPos.position + dir*actualSpeed*Time.deltaTime;
        BoundPlayer();
    }

    private void CheckForKeyInput()
    {

        //Check for various directional keys/combinations for movement
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            Move(new Vector3(2, 2, 0));
        }
        else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            Move(new Vector3(2, -2, 0));
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            Move(new Vector3(-2, 2, 0));
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            Move(new Vector3(-2, -2, 0));
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Move(new Vector3(2, 0, 0));
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Move(new Vector3(-2, 0, 0));
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            Move(new Vector3(0, 2, 0));
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Move(new Vector3(0, -2, 0));
        }

        //Used to set speed depending on whether or not the player is dashing
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            actualSpeed = speed * 1.5f;
        }
        else
        {
            actualSpeed = speed;
        }

        //Used to determine firing patterns
        if (bulletCooldownCur <= 0)
        {
            if (Input.GetKey(KeyCode.A))
            {
                Instantiate(bullets[0], rBullet.transform.position, Quaternion.identity);
                Instantiate(bullets[0], lBullet.transform.position, Quaternion.identity);
                bulletCooldownCur = bulletCooldownMax;
            }
        }
        else
        {
            bulletCooldownCur -= Time.deltaTime;
        }
    }

    private void BoundPlayer()
    {
        //Prevents the player from exiting a specified x and y bound, keeps them in the arena
        if (playerPos.position.x + playerRadius > xBound)
            playerPos.position = new Vector3(xBound - playerRadius, playerPos.position.y, playerPos.position.z);
        else if (playerPos.position.x - playerRadius < -xBound)
            playerPos.position = new Vector3(-xBound + playerRadius, playerPos.position.y, playerPos.position.z);

        if (playerPos.position.y + playerRadius > yBound)
            playerPos.position = new Vector3(playerPos.position.x, yBound - playerRadius, playerPos.position.z);
        else if (playerPos.position.y - playerRadius < -yBound)
            playerPos.position = new Vector3(playerPos.position.x, -yBound + playerRadius, playerPos.position.z);
    }

    public void setBulletCooldown(float val)
    {
        bulletCooldownMax = val;
    }
}

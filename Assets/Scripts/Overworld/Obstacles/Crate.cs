using UnityEngine;
using System.Collections;

public class Crate : MonoBehaviour {

    public Transform center, up, down, left, right;
    RaycastHit2D result1, result2, result3, result4;
    public Rigidbody2D rb;
    bool freezeX, freezeY;
    string c1, c2, c3, c4;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        SetMovability();
    }

    //Freezes/unfreezes crate's x and y axes based on it's surroundings
    private void SetMovability()
    {
        result1 = GlobalFunctions.GF.RaycastY(up, Vector2.up, 0.01f, 0.1f);
        result2 = GlobalFunctions.GF.RaycastY(down, Vector2.down, 0.01f, 0.1f);
        result3 = GlobalFunctions.GF.RaycastX(right, Vector2.right, 0.01f, 0.1f);
        result4 = GlobalFunctions.GF.RaycastX(left, Vector2.left, 0.01f, 0.1f);
        c1 = "";
        c2 = "";
        c3 = "";
        c4 = "";
        freezeX = true;
        freezeY = true;

        if (result1.collider != null)
        {
            c1 = result1.collider.gameObject.tag;
        }
        if (result2.collider != null)
        {
            c2 = result2.collider.gameObject.tag;
        }
        if ((c1 == "Player" && c2 == "") || (c1 == "" && c2 == "Player"))
        {
            freezeY = false;
        }

        if (result3.collider != null)
        {
            c3 = result3.collider.gameObject.tag;
        }
        if (result4.collider != null)
        {
            c4 = result4.collider.gameObject.tag;
        }
        if ((c3 == "Player" && c4 == "") || (c3 == "" && c4 == "Player"))
        {
            freezeX = false;
        }

        //Debug.Log("c1: " + c1);
        //Debug.Log("c2: " + c2);
        //Debug.Log("c3: " + c3);
        //Debug.Log("c4: " + c4);

        if (freezeX && freezeY)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else if (freezeX)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
    }
}

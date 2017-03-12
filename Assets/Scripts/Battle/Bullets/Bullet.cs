using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    //Start direction for bullet
    protected Vector3 direction;
    //Init transform. Used for reseting bullet
    protected Transform startTrans;
    //How fast the bullet moves
    protected float speed;
    //How much damage the bullet will cause to its target
    protected float damage;

    Rigidbody2D rb;

    //Active determines whether or not bullet is updating, and is used by the bullet pool
    public bool active = false;

    // Use this for initialization
    void Start()
    {
        //Default values. Override in child
        rb = GetComponent<Rigidbody2D>();
        direction = new Vector3 (0,1,0);
        startTrans = transform;
        speed = 1f;
        damage = 1f;
        Reset();
    }

    // Update is called once per frame
    void Update () {
        //Override in child
        if (active)
        {
            rb.velocity = speed*direction;
        }
    }

    //Function called from bullet pool to "create" bullet
    public virtual void Init(Vector3 pos, Vector3 dir)
    {
        //Override in child
        active = true;
        this.direction = dir;
        transform.position = pos;
        ToggleCollider(true);
        ToggleRenderer(true);
    }

    //Function to disable bullet and make it available to the bullet pool
    public virtual void Reset()
    {
        rb.velocity = Vector3.zero;
        active = false;
        ToggleCollider(false);
        ToggleRenderer(false);
        ResetTransform();
        //Override in child
    }

    //Basic function for toggling the collider. May need to override for certain children.
    public void ToggleCollider(bool toggle)
    {
        GetComponent<Collider2D>().enabled = toggle;
    }

    //Basic function for toggling the renderer. May need to override for certain children.
    public void ToggleRenderer(bool toggle)
    {
        GetComponent<SpriteRenderer>().enabled = toggle;
    }

    //Util function for returning bullet to start transform
    public void ResetTransform()
    {
        transform.position = startTrans.position;
        transform.rotation = startTrans.rotation;
        transform.localScale = startTrans.localScale;
    }
}

using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    protected Vector3 direction;
    protected float speed;
    protected int damage;

    public BoxCollider2D bounds;

    // Use this for initialization
    void Start()
    {
        //Default values
        this.direction = new Vector3 (0,1,0);
        this.speed = 1;
        this.damage = 1;
    }

    void Awake()
    {
        //Find bounding box
        GameObject temp = GameObject.FindGameObjectWithTag("Boundary");
        if (temp != null)
        {
            bounds = temp.GetComponent<BoxCollider2D>();
        }
    }

    // Update is called once per frame
    void Update () {

        gameObject.transform.position += direction*speed*Time.deltaTime;

        if (bounds != null)
        {
            DestroyBoundaryBullets(bounds);
        }
    }


    private void DestroyBoundaryBullets(BoxCollider2D bounds)
    {

        float xBound = bounds.size.x / 2f;
        float yBound = bounds.size.y / 2f;

        if (gameObject.transform.position.x > xBound)
            Destroy(gameObject);
        else if (gameObject.transform.position.x < -xBound)
            Destroy(gameObject);

        if (gameObject.transform.position.y > yBound)
            Destroy(gameObject);
        else if (gameObject.transform.position.y < -yBound)
            Destroy(gameObject);
    }

}

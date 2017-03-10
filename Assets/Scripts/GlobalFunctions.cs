using UnityEngine;
using System.Collections;

public class GlobalFunctions : MonoBehaviour {

    public static GlobalFunctions GF;

    void Awake()
    {
        if (GF == null)
        {
            DontDestroyOnLoad(gameObject);
            GF = this;
        }
        else if (GF != this)
        {
            Destroy(gameObject);
        }

    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public RaycastHit2D RaycastX(Transform start, Vector2 dir, float dis, LayerMask mask, float height)
    {
        RaycastHit2D temp;
        Vector2 pos = start.transform.position;
        Vector2 posBelow = new Vector2(start.transform.position.x, start.transform.position.y - height);
        Vector2 posAbove = new Vector2(start.transform.position.x, start.transform.position.y + height);

        temp = Physics2D.Raycast(pos, dir, dis, mask);
        if (temp.collider == null)
        {
            temp = Physics2D.Raycast(posAbove, dir, dis, mask);
            if (temp.collider == null)
            {
                temp = Physics2D.Raycast(posBelow, dir, dis, mask);
            }
        }
        return temp;
    }

    public RaycastHit2D RaycastY(Transform start, Vector2 dir, float dis, LayerMask mask, float width)
    {
        RaycastHit2D temp;
        Vector2 pos = start.transform.position;
        Vector2 posLeft = new Vector2(start.transform.position.x - width, start.transform.position.y);
        Vector2 posRight = new Vector2(start.transform.position.x + width, start.transform.position.y);

        temp = Physics2D.Raycast(pos, dir, dis, mask);
        if (temp.collider == null)
        {
            temp = Physics2D.Raycast(posLeft, dir, dis, mask);
            if (temp.collider == null)
            {
                temp = Physics2D.Raycast(posRight, dir, dis, mask);
            }
        }
        return temp;
    }

    public RaycastHit2D RaycastX(Transform start, Vector2 dir, float dis, float height)
    {
        RaycastHit2D temp;
        Vector2 pos = start.transform.position;
        Vector2 posBelow = new Vector2(start.transform.position.x, start.transform.position.y - height);
        Vector2 posAbove = new Vector2(start.transform.position.x, start.transform.position.y + height);

        temp = Physics2D.Raycast(pos, dir, dis);
        if (temp.collider == null)
        {
            temp = Physics2D.Raycast(posAbove, dir, dis);
            if (temp.collider == null)
            {
                temp = Physics2D.Raycast(posBelow, dir, dis);
            }
        }
        return temp;
    }

    public RaycastHit2D RaycastY(Transform start, Vector2 dir, float dis, float width)
    {
        RaycastHit2D temp;
        Vector2 pos = start.transform.position;
        Vector2 posLeft = new Vector2(start.transform.position.x - width, start.transform.position.y);
        Vector2 posRight = new Vector2(start.transform.position.x + width, start.transform.position.y);

        temp = Physics2D.Raycast(pos, dir, dis);
        if (temp.collider == null)
        {
            temp = Physics2D.Raycast(posLeft, dir, dis);
            if (temp.collider == null)
            {
                temp = Physics2D.Raycast(posRight, dir, dis);
            }
        }
        return temp;
    }

}

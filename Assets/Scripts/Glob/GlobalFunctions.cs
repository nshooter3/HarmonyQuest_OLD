using UnityEngine;
using System.Collections;

public class GlobalFunctions : MonoBehaviour {

    public static GlobalFunctions instance;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
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

    public void AdjustRotationOverTime(Vector3 startRot, Vector3 endRot, float time, Transform obj)
    {
        StartCoroutine(AdjustRotationOverTimeCo(startRot, endRot, time, obj));
    }

    //Lerps a passed in transform between 2 rotations over time
    IEnumerator AdjustRotationOverTimeCo(Vector3 startRot, Vector3 endRot, float time, Transform obj)
    {
        for (float t = time; t >= 0; t -= Time.deltaTime)
        {
            obj.transform.eulerAngles = Vector3.Lerp(endRot, startRot, t/time);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        obj.transform.eulerAngles = endRot;
    }

    public void AdjustSizeOverTime(Vector3 startSize, Vector3 endSize, float time, Transform obj)
    {
        StartCoroutine(AdjustSizeOverTimeCo(startSize, endSize, time, obj));
    }

    //Lerps a passed in transform between 2 sizes over time
    IEnumerator AdjustSizeOverTimeCo(Vector3 startSize, Vector3 endSize, float time, Transform obj)
    {
        for (float t = time; t >= 0; t -= Time.deltaTime)
        {
            obj.transform.localScale = Vector3.Lerp(endSize, startSize, t / time);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        obj.transform.localScale = endSize;
    }

    public void AdjustPositionOverTime(Vector3 startPos, Vector3 endPos, float time, Transform obj)
    {
        StartCoroutine(AdjustPositionOverTimeCo(startPos, endPos, time, obj));
    }

    //Lerps a passed in transform between 2 positions over time
    IEnumerator AdjustPositionOverTimeCo(Vector3 startPos, Vector3 endPos, float time, Transform obj)
    {
        for (float t = time; t >= 0; t -= Time.deltaTime)
        {
            obj.transform.position = Vector3.Lerp(endPos, startPos, t / time);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        obj.transform.position = endPos;
    }

    public void AdjustColorOverTime(Color startCol, Color endCol, float time, SpriteRenderer obj)
    {
        StartCoroutine(AdjustColorOverTimeCo(startCol, endCol, time, obj));
    }

    //Lerps a passed in spriteRenderer between 2 colors over time
    IEnumerator AdjustColorOverTimeCo(Color startCol, Color endCol, float time, SpriteRenderer obj)
    {
        for (float t = time; t >= 0; t -= Time.deltaTime)
        {
            obj.color = Color.Lerp(endCol, startCol, t / time);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        obj.color = endCol;
    }

    public void AdjustColorOverTimeTextMesh(Color startCol, Color endCol, float time, TextMesh obj)
    {
        StartCoroutine(AdjustColorOverTimeTextMeshCo(startCol, endCol, time, obj));
    }

    //Lerps a passed in textMesh between 2 colors over time
    IEnumerator AdjustColorOverTimeTextMeshCo(Color startCol, Color endCol, float time, TextMesh obj)
    {
        for (float t = time; t >= 0; t -= Time.deltaTime)
        {
            obj.color = Color.Lerp(endCol, startCol, t / time);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        obj.color = endCol;
    }
}

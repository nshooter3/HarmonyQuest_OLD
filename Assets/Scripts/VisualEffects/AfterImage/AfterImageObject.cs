using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageObject : MonoBehaviour {

    //Whether or not the object is currently fading out
    public bool active = false;
    //Whether or not the object shrinks in around it's origin (used to telegraph attacks)
    public bool shrink = false;
    //Whether or not the object grows around it's origin
    public bool grow = false;
    //Used to reset parent after shrink command is complete
    public Transform originalParent;

    public Material defaultMat;

    Vector3 startScale, endScale;

    SpriteRenderer sr;

    //Used to reset layer after command is complete
    int originalLayer;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
        sr.enabled = false;
        originalLayer = gameObject.layer;
    }

	// Use this for initialization
	void Start () {
        
	}

    //Start fadeout
    public void ToggleOn(Transform trans, float fadeTime, bool shrink = false, bool grow = false, float xOff = 0, float yOff = 0, string layer = "")
    {
        active = true;
        sr.enabled = true;
        sr.sprite = trans.GetComponent<SpriteRenderer>().sprite;
        sr.sortingLayerName = trans.GetComponent<SpriteRenderer>().sortingLayerName;
        if (layer != "" && LayerMask.NameToLayer(layer) >= 0)
        {
            gameObject.layer = LayerMask.NameToLayer(layer);
        }
        sr.material = trans.GetComponent<SpriteRenderer>().material;
        GlobalFunctions.instance.CopyTranform(transform, trans);
        transform.position = new Vector3(transform.position.x + xOff, transform.position.y + yOff, transform.position.z);
        if (shrink)
        {
            transform.parent = trans;
            startScale = transform.localScale * 1.5f;
            endScale = transform.localScale;
            this.shrink = true;
            StartCoroutine(AfterImageObjectFade(new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f), new Color(sr.color.r, sr.color.g, sr.color.b, 0), fadeTime));
        }
        else if (grow)
        {
            transform.parent = trans;
            startScale = transform.localScale;
            endScale = transform.localScale * 3.0f;
            this.grow = true;
            StartCoroutine(AfterImageObjectFade(new Color(sr.color.r, sr.color.g, sr.color.b, 0.0f), new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f), fadeTime));
        }
        else
        {
            transform.localScale = trans.lossyScale;
            StartCoroutine(AfterImageObjectFade(new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f), new Color(sr.color.r, sr.color.g, sr.color.b, 0), fadeTime));
        }
    }

    public void ToggleOnDBZ(Transform trans, float fadeTime, Transform target)
    {
        active = true;
        sr.enabled = true;
        sr.sprite = trans.GetComponent<SpriteRenderer>().sprite;
        sr.sortingLayerName = trans.GetComponent<SpriteRenderer>().sortingLayerName;
        sr.material = trans.GetComponent<SpriteRenderer>().material;
        GlobalFunctions.instance.CopyTranform(transform, trans);
        transform.parent = trans;
        StartCoroutine(AfterImageDBZEffect(new Color(sr.color.r, sr.color.g, sr.color.b, 0.0f), new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f), fadeTime, target));
    }

    //Reset object after fadeout
    public void ToggleOff()
    {
        sr.sortingLayerName = "Default";
        sr.material = defaultMat;
        sr.enabled = false;
        active = false;
        shrink = false;
        if (originalParent != null)
        {
            transform.parent = originalParent;
        }
        gameObject.layer = originalLayer;
        GlobalFunctions.instance.ResetTranform(transform);
    }

    //Fadeout coroutine
    IEnumerator AfterImageObjectFade(Color startCol, Color endCol, float time)
    {
        for (float t = time; t >= 0; t -= Time.deltaTime)
        {
            if (shrink)
            {
                transform.localScale = Vector2.Lerp(endScale, startScale, t / time);
                sr.color = Color.Lerp(startCol, endCol, t / time);
            }
            else if (grow)
            {
                transform.localScale = Vector2.Lerp(endScale, startScale, t / time);
                sr.color = Color.Lerp(startCol, endCol, t / time);
            }
            else
            {
                sr.color = Color.Lerp(endCol, startCol, t / time);
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        sr.color = endCol;
        ToggleOff();
    }

    //DBZEffect coroutine, used to generate flickering after image perpendicular to the line between itself and it's target. For telegraphing major attacks
    IEnumerator AfterImageDBZEffect(Color startCol, Color endCol, float time, Transform target)
    {
        Vector2 lineOfSight, AfterImageDir;
        Vector2 startPos = transform.position;
        Vector2 startScale = transform.localScale;
        //Used to determine how far out the after image effect starts
        float distance = 0.75f;
        //Used to determine how often to flip the effect
        float dirFlipTimer = 0.05f;
        for (float t = time; t >= 0; t -= Time.deltaTime)
        {
            //Update to grab parent's current sprite
            if (transform.parent.GetComponent<SpriteRenderer>().sprite != null)
            {
                sr.sprite = transform.parent.GetComponent<SpriteRenderer>().sprite;
            }
            lineOfSight = (Vector2)target.position - startPos;
            lineOfSight.Normalize();
            //Gotta find a line perpendicular to the enemies' line of site to use for the effect
            AfterImageDir = new Vector2(lineOfSight.y, -lineOfSight.x);
            transform.position = Vector2.Lerp(startPos, startPos + AfterImageDir * distance, t/time);
            transform.localScale = Vector2.Lerp(startScale, startScale/2.0f, t / time);
            sr.color = Color.Lerp(endCol, startCol, t / time);
            dirFlipTimer -= Time.deltaTime;
            if (dirFlipTimer <= 0)
            {
                dirFlipTimer = 0.01f;
                distance *= -1;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        sr.color = endCol;
        ToggleOff();
    }
}

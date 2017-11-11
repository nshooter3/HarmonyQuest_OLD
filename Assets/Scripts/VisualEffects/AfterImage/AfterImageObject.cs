using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageObject : MonoBehaviour {

    //Whether or not the object is currently fading out
    public bool active = false;
    //Whether or not the object shrinks in around it's origin (used to telegraph attacks)
    public bool shrink = false;
    //Used to reset parent after shrink command is complete
    public Transform originalParent;

    Vector3 startScale, endScale;

    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
        sr.enabled = false;
    }

	// Use this for initialization
	void Start () {
        
	}

    //Start fadeout
    public void ToggleOn(Transform trans, float fadeTime, bool shrink = false)
    {
        active = true;
        sr.enabled = true;
        sr.sprite = trans.GetComponent<SpriteRenderer>().sprite;
        GlobalFunctions.instance.CopyTranform(transform, trans);
        if (shrink)
        {
            transform.parent = trans;
            startScale = transform.localScale*1.5f;
            endScale = transform.localScale;
            this.shrink = true;
        }
        StartCoroutine(AfterImageObjectFade(new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f), new Color(sr.color.r, sr.color.g, sr.color.b, 0), fadeTime));
    }

    //Reset object after fadeout
    public void ToggleOff()
    {
        sr.enabled = false;
        active = false;
        shrink = false;
        if (originalParent != null)
        {
            transform.parent = originalParent;
        }
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
            else
            {
                sr.color = Color.Lerp(endCol, startCol, t / time);
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        sr.color = endCol;
        ToggleOff();
    }
}

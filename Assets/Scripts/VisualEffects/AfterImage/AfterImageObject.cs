using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImageObject : MonoBehaviour {

    //Whether or not the object is currently fading out
    public bool active = false;

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
    public void ToggleOn(Transform trans, float fadeTime)
    {
        active = true;
        sr.enabled = true;
        sr.sprite = trans.GetComponent<SpriteRenderer>().sprite;
        GlobalFunctions.instance.CopyTranform(transform, trans);
        StartCoroutine(AfterImageObjectFade(new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f), new Color(sr.color.r, sr.color.g, sr.color.b, 0), fadeTime));
    }

    //Reset object after fadeout
    public void ToggleOff()
    {
        sr.enabled = false;
        active = false;
        GlobalFunctions.instance.ResetTranform(transform);
    }

    //Fadeout coroutine
    IEnumerator AfterImageObjectFade(Color startCol, Color endCol, float time)
    {
        for (float t = time; t >= 0; t -= Time.deltaTime)
        {
            sr.color = Color.Lerp(endCol, startCol, t / time);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        sr.color = endCol;
        ToggleOff();
    }
}

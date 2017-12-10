using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScreenDimmer : MonoBehaviour {

    SpriteRenderer sr;

    public static BattleScreenDimmer instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
	}

    public void SetAlpha(float a = 0.5f)
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, a);
    }

    public void FadeIn(float dur = 0.5f)
    {
        GlobalFunctions.instance.FadeIn(sr.color, dur, sr);
    }

    public void FadeOut(float dur = 0.5f)
    {
        GlobalFunctions.instance.FadeOut(sr.color, dur, sr);
    }
}

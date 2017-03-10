using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeInFadeOut : MonoBehaviour {

    private Image Fader;
    private float timer, maxTimer;
    private float dur = 1f;
    bool fadeIn, fadeOut;

	// Use this for initialization
	void Start () {
        Fader = GetComponent<Image>();
        Fader.enabled = true;
        FadeIn();
	}
	
	// Update is called once per frame
	void Update () {

        //Code for testing
        /*if (Input.GetKeyDown(KeyCode.I))
            FadeIn(dur);
        else if (Input.GetKeyDown(KeyCode.O))
            FadeOut(dur);*/

        if (timer > 0)
        {
            if (fadeIn)
            {
                Fader.color = new Color(Fader.color.r, Fader.color.g, Fader.color.b, timer / maxTimer);
            }
            else if (fadeOut)
            {
                Fader.color = new Color(Fader.color.r, Fader.color.g, Fader.color.b, 1f - timer / maxTimer);
            }
            timer -= Time.deltaTime;
        }
        else
        {
            fadeIn = false;
            fadeOut = false;
        }
	}

    public void FadeIn()
    {
        timer = dur;
        maxTimer = dur;
        fadeIn = true;
        fadeOut = false;
    }

    public void FadeOut()
    {
        timer = dur;
        maxTimer = dur;
        fadeOut = true;
        fadeIn = false;
    }
}

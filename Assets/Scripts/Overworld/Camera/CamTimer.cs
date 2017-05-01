using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CamTimer : MonoBehaviour {

    //Current time and maxTime for the timer
    public float curTime, maxTime;
    //Whether or not timer is currently counting down
    public bool timerActive;
    //UI text component
    Text text;

    public static CamTimer instance;

	void Awake () {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Turns on/off text component
    public void ToggleVisibility(bool toggle)
    {
        text.enabled = toggle;
    }

    //Sets Timer. If flash is true, makes timer flash for 3 seconds. Callback fires after timer is set, or after flashing if that is enabled
    public void SetTimer(float maxTime, bool flash = false, Action callback = null)
    {
        ToggleVisibility(true);
        timerActive = true;
        this.maxTime = maxTime;
        text.text = GlobalFunctions.instance.FormatTime(maxTime);
        if (flash)
        {
            StartCoroutine(TimerFlashCo(1.5f, 3, callback));
        }
        else
        {
            if (callback != null)
            {
                callback();
            }
        }
    }

    //Make timer flash on and off over a period of time a certain number of times. callback fires after dur is <= 0
    public void TimerFlash(float dur = 1.5f, int flashes = 3, Action callback = null)
    {
        StartCoroutine(TimerFlashCo(dur, flashes, callback));
    }

    IEnumerator TimerFlashCo(float dur, int flashes, Action callback)
    {
        ToggleVisibility(true);
        flashes = flashes*2 - 1;
        float flashTimer = dur / flashes;
        float maxFlashTimer = flashTimer;
        while (dur > 0)
        {
            dur -= Time.deltaTime;
            if (flashTimer > 0)
            {
                flashTimer -= Time.deltaTime;
            }
            else
            {
                flashTimer = maxFlashTimer;
                text.enabled = !text.enabled;
            }
            if (dur <= 0)
            {
                text.enabled = true;
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        if (callback != null)
        {
            callback();
        }
    }

    //Starts timer countdown. Callback fires when the timer runs out
    public void StartTimer(Action callback = null)
    {
        StartCoroutine(StartTimerCo(callback));
    }

    IEnumerator StartTimerCo(Action callback)
    {
        ToggleVisibility(true);
        curTime = maxTime;
        while (curTime > 0)
        {
            curTime = Mathf.Max(curTime - Time.deltaTime, 0);
            text.text = GlobalFunctions.instance.FormatTime(curTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        timerActive = false;
        if (callback != null)
        {
            callback();
        }
    }
}

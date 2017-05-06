using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CamTimer : MonoBehaviour
{

    //Current time and maxTime for the timer
    public float curTime, maxTime;
    //Whether or not timer is currently counting down
    public bool timerActive;
    //UI text component
    Text text;

    Vector3 startSize, startRot, startPos;
    Vector4 startCol;

    public static CamTimer instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        text = GetComponent<Text>();
        startSize = transform.localScale;
        startPos = transform.position;
        startRot = transform.eulerAngles;
        startCol = text.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Turns on/off text component
    public void ToggleVisibility(bool toggle)
    {
        text.enabled = toggle;
    }

    //Sets Timer. If flash is true, makes timer flash for 3 seconds. Callback fires after timer is set, or after flashing if that is enabled
    public void SetTimer(float maxTime, bool flash = false, Action callback = null)
    {
        transform.localScale = startSize;
        transform.position = startPos;
        transform.eulerAngles = startRot;
        text.color = startCol;
        ToggleVisibility(true);
        timerActive = true;
        this.maxTime = maxTime;
        if (maxTime >= 60)
            text.text = GlobalFunctions.instance.FormatTimeMinutes(maxTime);
        else
            text.text = GlobalFunctions.instance.FormatTimeSeconds(maxTime);
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
        flashes = flashes * 2 - 1;
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
    public void StartTimer(Action callback = null, bool shakeWhenLow = true)
    {
        StartCoroutine(StartTimerCo(callback, shakeWhenLow));
    }

    IEnumerator StartTimerCo(Action callback, bool shakeWhenLow)
    {
        ToggleVisibility(true);
        curTime = maxTime;
        while (curTime > 0)
        {
            curTime = Mathf.Max(curTime - Time.deltaTime, 0);
            if(maxTime >= 60)
                text.text = GlobalFunctions.instance.FormatTimeMinutes(curTime);
            else
                text.text = GlobalFunctions.instance.FormatTimeSeconds(curTime);
            yield return new WaitForSeconds(Time.deltaTime);
            //Visual effects for running out of time
            if (curTime < 10 && shakeWhenLow)
            {
                float val = 1 - curTime / 10;
                text.color = Color.Lerp(startCol, Color.red, val);
                text.transform.localScale = Vector3.Lerp(startSize, startSize * 1.3f, val);
                text.transform.position = startPos + new Vector3(UnityEngine.Random.Range(-val, val)*4f, UnityEngine.Random.Range(-val, val) * 4f, 0);
            }
        }
        timerActive = false;
        if (callback != null)
        {
            callback();
        }
    }
}

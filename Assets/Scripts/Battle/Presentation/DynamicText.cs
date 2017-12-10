using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicText : MonoBehaviour {

    public AudioSource sfx;
    public float duration, delay;
    public int quarterNotes;
    public bool isComplete = false, isTypeFade = true, isGo = false;
    private Text[] text;

    //Position/scale related vars
    public Vector3 startPos, endPos, startScale, endScale, startRot, endRot;

    public void Activate()
    {
        text = GetComponentsInChildren<Text>();
        if (quarterNotes > 0)
        {
            duration = BeatManager.instance.GetQuarterNote() * quarterNotes;
        }

        PlaySFX();

        if (startPos != endPos)
        {
            StartCoroutine(Move());
        }
        if (startScale != endScale)
        {
            StartCoroutine(Scale());
        }
        if (startRot != endPos)
        {
            StartCoroutine(Rot());
        }
        if (isTypeFade && !isGo)
        {
            StartCoroutine(Fade());
        }
        if (isGo)
        {
            StartCoroutine(GoEffect());
        }
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(delay);
        if (!isGo)
        {
            foreach (Text t in text)
            {
                GlobalFunctions.instance.AdjustPositionOverTimeSmoothText(startPos, endPos, duration, t);
            }
            yield return new WaitForSeconds(duration);
            isComplete = true;
        }
        else
        {
            foreach (Text t in text)
            {
                GlobalFunctions.instance.AdjustPositionOverTimeSmoothText(startPos, endPos, duration / 2.0f, t);
            }
        }
    }

    IEnumerator Scale()
    {
        yield return new WaitForSeconds(delay);
        if (!isGo)
        {
            foreach (Text t in text)
            {
                GlobalFunctions.instance.AdjustScaleOverTimeSmoothText(startScale, endScale, duration, t);
            }
            yield return new WaitForSeconds(duration);
            isComplete = true;
        }
        else
        {
            foreach (Text t in text)
            {
                GlobalFunctions.instance.AdjustScaleOverTimeSmoothText(startScale, endScale, duration / 2.0f, t);
            }
        }
    }

    IEnumerator Rot()
    {
        yield return new WaitForSeconds(delay);
        if (!isGo)
        {
            foreach (Text t in text)
            {
                GlobalFunctions.instance.AdjustRotOverTimeSmoothText(startRot, endRot, duration, t);
            }
            yield return new WaitForSeconds(duration);
            isComplete = true;
        }
        else
        {
            foreach (Text t in text)
            {
                GlobalFunctions.instance.AdjustRotOverTimeSmoothText(startRot, endRot, duration/2.0f, t);
            }
        }
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(delay);
        foreach (Text t in text)
        {
            GlobalFunctions.instance.FadeInText(t.color, duration * 0.25f, t);
            GlobalFunctions.instance.DelayedFunction(() => GlobalFunctions.instance.FadeOutText(t.color, duration * 0.25f, t), duration * 0.75f);
        }
        yield return new WaitForSeconds(duration);
        isComplete = true;
    }

    IEnumerator GoEffect()
    {
        yield return new WaitForSeconds(delay);
        foreach (Text t in text)
        {
            GlobalFunctions.instance.FadeInText(t.color, duration * 0.1f, t);
        }
        yield return new WaitForSeconds(duration * 0.1f);

        Text[] children = GetComponentsInChildren<Text>();
        int curChild = 0;
        float interval = 0;
        for (float d = duration * 0.7f; d > 0; d -= Time.deltaTime)
        {
            interval += Time.deltaTime;
            if (interval > 0.035f)
            {
                interval = 0;
                if (curChild == 0)
                {
                    children[0].color = new Color(children[0].color.r, children[0].color.g, children[0].color.b, 0);
                    children[1].color = new Color(children[1].color.r, children[1].color.g, children[1].color.b, 1);
                    curChild = 1;
                }
                else
                {
                    children[0].color = new Color(children[0].color.r, children[0].color.g, children[0].color.b, 1);
                    children[1].color = new Color(children[1].color.r, children[1].color.g, children[1].color.b, 0);
                    curChild = 0;
                }
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }

        foreach (Text t in text)
        {
            GlobalFunctions.instance.FadeOutText(t.color, duration * 0.2f, t);
        }
        yield return new WaitForSeconds(duration * 0.1f);

        isComplete = true;
    }

    public void PlaySFX()
    {
        if (sfx != null)
        {
            sfx.Play();
        }
    }
}

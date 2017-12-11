using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicText : Activatable {

    public bool isGo = false;
    private Text[] text;

    public Explodable explode;

    public override void Activate()
    {
        text = GetComponentsInChildren<Text>();
        if (quarterNotes > 0)
        {
            duration = BeatManager.instance.GetQuarterNote() * quarterNotes;
        }

        StartCoroutine(PlaySFX());
        StartCoroutine(PlayParticles());

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
                GlobalFunctions.instance.DelayedFunction(() => GlobalFunctions.instance.AdjustPositionOverTimeSmoothText(endPos, Vector3.Lerp(startPos, endPos, 0.75f), duration / 2.0f, t), duration / 2.0f);
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
                GlobalFunctions.instance.DelayedFunction(() => GlobalFunctions.instance.AdjustScaleOverTimeSmoothText(endScale, Vector3.Lerp(startScale, endScale, 0.75f), duration / 2.0f, t), duration / 2.0f);
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
                GlobalFunctions.instance.DelayedFunction(() => GlobalFunctions.instance.AdjustRotOverTimeSmoothText(endRot, Vector3.Lerp(startRot, endRot, 0.95f), duration / 4.0f, t), duration / 2.0f);
            }
        }
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(delay);
        foreach (Text t in text)
        {
            GlobalFunctions.instance.FadeInText(new Color(t.color.r, t.color.g, t.color.b, 1), duration * 0.25f, t);
            GlobalFunctions.instance.DelayedFunction(() => GlobalFunctions.instance.FadeOutText(new Color(t.color.r, t.color.g, t.color.b, 1), duration * 0.25f, t), duration * 0.75f);
        }
        yield return new WaitForSeconds(duration);
        isComplete = true;
    }

    IEnumerator GoEffect()
    {
        yield return new WaitForSeconds(delay);
        ScreenFlash.instance.Flash(new Color(1, 1, 1, 0.5f), 0.25f);
        GlobalFunctions.instance.DelayedFunction(() => BattleScreenDimmer.instance.FadeOut(0.5f), duration/1.5f);
        if (explode != null)
        {
            explode.explode();
        }
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
}

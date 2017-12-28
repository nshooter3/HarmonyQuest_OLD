using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicTransform : Activatable {

    public override void Activate()
    {
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
        if (isTypeFade)
        {
            StartCoroutine(Fade());
        }
    }

    IEnumerator Move()
    {
        yield return new WaitForSeconds(delay);
        GlobalFunctions.instance.AdjustPositionOverTimeSmooth(startPos, endPos, duration, transform);
        yield return new WaitForSeconds(duration);
        isComplete = true;
    }

    IEnumerator Scale()
    {
        yield return new WaitForSeconds(delay);
        GlobalFunctions.instance.AdjustScaleOverTimeSmooth(startScale, endScale, duration, transform);
        yield return new WaitForSeconds(duration);
        isComplete = true;
    }

    IEnumerator Rot()
    {
        yield return new WaitForSeconds(delay);
        GlobalFunctions.instance.AdjustRotOverTimeSmooth(startRot, endRot, duration, transform);
        yield return new WaitForSeconds(duration);
        isComplete = true;
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(delay);
        foreach (SpriteRenderer t in GetComponentsInChildren<SpriteRenderer>())
        {
            GlobalFunctions.instance.FadeIn(new Color(t.color.r, t.color.g, t.color.b, 1), duration * 0.25f, t);
            if (isTypeFadeOut)
            {
                GlobalFunctions.instance.DelayedFunction(() => GlobalFunctions.instance.FadeOut(new Color(t.color.r, t.color.g, t.color.b, 1), duration * 0.25f, t), duration * 0.75f);
            }
        }
        yield return new WaitForSeconds(duration);
        isComplete = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIcon : MonoBehaviour {

    public SpriteRenderer background, fill, weaponLight, weaponDark, whiteOut, redOut;

    private float maxDur, curDur, whiteTimer = 0, redTimer = 0, whiteTimerMax = 0.25f, redTimerMax = 0.25f;

    public void Activate(float duration)
    {
        maxDur = duration;
        curDur = duration;
        weaponDark.enabled = true;
        weaponLight.enabled = false;
        whiteTimer = whiteTimerMax;
        fill.transform.localScale = Vector3.zero;
    }

    public void FailedActivate()
    {
        redTimer = redTimerMax;
    }

    public void Deactivate()
    {
        maxDur = 0;
        curDur = 0;
        weaponDark.enabled = false;
        weaponLight.enabled = true;
        fill.transform.localScale = Vector3.one;
    }

    void Update()
    {
        if (curDur > 0)
        {
            curDur -= Time.deltaTime;
            fill.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, curDur/maxDur);
            if (curDur <= 0)
            {
                Deactivate();
            }
        }
        if (redTimer > 0)
        {
            redTimer -= Time.deltaTime;
            redOut.color = new Color(redOut.color.r, redOut.color.g, redOut.color.b, redTimer/redTimerMax);
        }
        if (whiteTimer > 0)
        {
            whiteTimer -= Time.deltaTime;
            whiteOut.color = new Color(whiteOut.color.r, whiteOut.color.g, whiteOut.color.b, whiteTimer / whiteTimerMax);
        }
    }
}

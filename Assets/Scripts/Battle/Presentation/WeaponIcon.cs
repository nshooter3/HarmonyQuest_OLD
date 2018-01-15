using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponIcon : MonoBehaviour
{

    public SpriteRenderer background, fill, weaponLight, weaponDark, whiteOut, redOut;

    private float maxDur, curDur, whiteTimer = 0, whiteTimerMax = 0.25f, sizeTimer = 0, sizeTimerMax = 0.1f;

    private Vector3 initSize;

    public string iconName;

    //Toggle juicy size effect upon this weapon's loadout getting activated
    public void LoadoutSizeLerp()
    {
        sizeTimer = sizeTimerMax;
        initSize = transform.localScale;
    }

    public void Activate(float duration, bool playFill)
    {
        if (playFill)
        {
            maxDur = duration;
            curDur = duration;
            weaponDark.enabled = true;
            weaponLight.enabled = false;
            fill.transform.localScale = Vector3.zero;
        }
        LoadoutSizeLerp();
        whiteTimer = whiteTimerMax;
    }

    public void Deactivate()
    {
        maxDur = 0;
        curDur = 0;
        weaponDark.enabled = false;
        weaponLight.enabled = true;
        fill.transform.localScale = Vector3.one;
    }

    public void Darken()
    {
        if (fill.GetComponent<SpriteRenderer>().enabled == true)
        {
            weaponDark.enabled = true;
            weaponLight.enabled = false;
            fill.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void UnDarken()
    {
        if (fill.GetComponent<SpriteRenderer>().enabled == false)
        {
            weaponDark.enabled = false;
            weaponLight.enabled = true;
            fill.GetComponent<SpriteRenderer>().enabled = true;
            whiteTimer = whiteTimerMax;
        }
    }

    void Update()
    {
        if (curDur > 0)
        {
            curDur -= Time.deltaTime;
            fill.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, curDur / maxDur);
            if (curDur <= 0)
            {
                Deactivate();
            }
        }
        if (whiteTimer > 0)
        {
            whiteTimer -= Time.deltaTime;
            whiteOut.color = new Color(whiteOut.color.r, whiteOut.color.g, whiteOut.color.b, (whiteTimer / whiteTimerMax) / 2.0f);
        }
        if (sizeTimer > 0)
        {
            sizeTimer -= Time.deltaTime;
            transform.localScale = Vector3.Lerp(initSize, initSize * 1.25f, sizeTimer / sizeTimerMax);
        }
    }
}
